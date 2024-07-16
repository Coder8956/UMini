﻿using System.Collections;
using System.Collections.Generic;
using UMiniFramework.Scripts.Pool.GameObjectPool;
using UMiniFramework.Scripts.Utils;
using UnityEngine;

namespace UMiniFramework.Scripts.Modules.AudioModule
{
    public class EffectAudio : BaseAudio
    {
        private GameObjectPool m_soundPool;
        private Dictionary<string, AudioClip> m_cachedAudioClipDic;

        public override void Init()
        {
            // 初始化音效对象池
            GameObject poolObjectTemplet = UMUtils.Common.CreateGameObject<AudioSource>("Sound", gameObject).gameObject;
            GameObjectPool.PoolConfig poolConfig = new GameObjectPool.PoolConfig
            ("SoundPool",
                gameObject,
                poolObjectTemplet,
                5,
                null,
                null
            );
            m_soundPool = GameObjectPool.CreatePool(poolConfig);
            m_cachedAudioClipDic = new Dictionary<string, AudioClip>();
        }

        public void Play(string audioPath, float volume = 1)
        {
            AudioClip effectAC = null;
            if (m_cachedAudioClipDic.ContainsKey(audioPath))
            {
                effectAC = m_cachedAudioClipDic[audioPath];
                PlayEffect(effectAC, volume);
            }
            else
            {
                LoadAudioClip(audioPath, (clip) =>
                {
                    effectAC = clip;
                    m_cachedAudioClipDic.Add(audioPath, effectAC);
                    PlayEffect(effectAC, volume);
                });
            }
        }

        private void PlayEffect(AudioClip ac, float volume)
        {
            GameObject audioEffect = m_soundPool.Get();
            audioEffect.transform.SetParent(transform, false);
            AudioSource effectAS = audioEffect.GetComponent<AudioSource>();
            effectAS.clip = ac;
            effectAS.volume = volume;
            effectAS.Play();
            StartCoroutine(WaitEffectPlayOver(effectAS));
        }

        private IEnumerator WaitEffectPlayOver(AudioSource audioSource)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            m_soundPool.Back(audioSource.gameObject);
        }
    }
}