﻿using System.Collections;
using UMiniFramework.Scripts.UMEntrance;
using UMiniFramework.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UMiniFramework.Scripts.Modules.SceneModule
{
    public class UMSceneModule : UMModule
    {
        public override IEnumerator Init(UMiniConfig config)
        {
            yield return null;
            m_initFinished = true;
            UMUtilCommon.PrintModuleInitFinishedLog(GetType().Name, m_initFinished);
        }

        public void Load(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public AsyncOperation LoadSceneAsync(string scene)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
            return ao;
        }
    }
}