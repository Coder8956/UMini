﻿using Game.Scripts.UI.Login;
using UMiniFramework.Scripts;
using UMiniFramework.Scripts.Modules.UIModule;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Debug
{
    [UMUIPanelInfo("UI/Debug/DebugPanel", UMUILayer.TOP)]
    public class DebugPanel : UMUIPanel
    {
        [SerializeField] private Button m_btnShowDebugFunc;
        [SerializeField] private Button m_btnOpenLogin;
        [SerializeField] private RectTransform m_debugFuncRoot;

        #region AUDIO_DEBUG

        [SerializeField] private Button m_btnPlayBGM_1;
        [SerializeField] private Button m_btnPlayBGM_2;
        [SerializeField] private Button m_btnStopBGM;
        [SerializeField] private Button m_btnBGMMute;

        #endregion

        public override void OnLoaded()
        {
            m_debugFuncRoot.gameObject.SetActive(false);
            m_btnShowDebugFunc.onClick.AddListener(() =>
            {
                m_debugFuncRoot.gameObject.SetActive(!m_debugFuncRoot.gameObject.activeSelf);
            });

            m_btnOpenLogin.onClick.AddListener(OpenLoginPanel);

            m_btnPlayBGM_1.onClick.AddListener(PlayBGM_1);
            m_btnPlayBGM_2.onClick.AddListener(PlayBGM_2);
            m_btnStopBGM.onClick.AddListener(StopBGM);
            m_btnBGMMute.onClick.AddListener(BGMMute);
        }

        private void PlayBGM_1()
        {
            UMini.Audio.BGM.Play("Audio/BGM/BGM_001");
        }

        private void PlayBGM_2()
        {
            UMini.Audio.BGM.Play("Audio/BGM/BGM_002", 0.2f, false);
        }

        private void StopBGM()
        {
            UMini.Audio.BGM.Stop();
        }

        private void BGMMute()
        {
            UMini.Audio.BGM.SetMute(!UMini.Audio.BGM.GetMute());
        }

        private void OpenLoginPanel()
        {
            UMini.UI.Open<LoginPanel>();
        }

        public override void OnOpen()
        {
        }

        public override void OnClose()
        {
        }
    }
}