﻿using System.Collections;
using UMiniFramework.Scripts.UMEntrance;
using UMiniFramework.Scripts.Utils;

namespace UMiniFramework.Scripts.Modules.AudioModule
{
    public class UMAudioModule : UMModule
    {
        public BGMAudio BGM { get; private set; }
        public EffectAudio Effect { get; private set; }

        public override IEnumerator Init(UMiniConfig config)
        {
            BGM = UMUtils.Common.CreateGameObject<BGMAudio>(nameof(BGMAudio), gameObject);
            BGM.Init();

            Effect = UMUtils.Common.CreateGameObject<EffectAudio>(nameof(EffectAudio), gameObject);
            Effect.Init();
            yield return null;
        }
    }
}