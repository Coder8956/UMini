using System;
using System.Collections;
using System.Collections.Generic;
using UMiniFramework.Scripts.Modules;
using UMiniFramework.Scripts.Modules.AudioModule;
using UMiniFramework.Scripts.Modules.ConfigModule;
using UMiniFramework.Scripts.Modules.AssetModule;
using UMiniFramework.Scripts.Modules.AssetModule.AssetLoaders;
using UMiniFramework.Scripts.Modules.PersistentDataModule;
using UMiniFramework.Scripts.Modules.SceneModule;
using UMiniFramework.Scripts.Modules.UIModule;
using UMiniFramework.Scripts.Utils;
using UnityEngine;

namespace UMiniFramework.Scripts
{
    public class UMini : MonoBehaviour
    {
        private static UMini GlobalInstance = null;

        /// <summary>
        /// 音频模块
        /// </summary>
        public static UMAudioModule Audio { get; private set; }

        [SerializeField] private UMAudioModule m_audioModule = null;

        /// <summary>
        /// 配置模块
        /// </summary>
        public static UMConfigModule Config { get; private set; }

        [SerializeField] private UMConfigModule m_configModule = null;

        /// <summary>
        /// 数据持久化模块
        /// </summary>
        public static UMPersistentDataModule PersiData { get; private set; }

        [SerializeField] private UMPersistentDataModule m_persiData = null;

        /// <summary>
        /// 场景模块
        /// </summary>
        public static UMSceneModule Scene { get; private set; }

        [SerializeField] private UMSceneModule m_sceneModule = null;

        /// <summary>
        /// 资源模块
        /// </summary>
        public static UMAssetModule Asset { get; private set; }

        [SerializeField] private UMAssetModule m_assetModule = null;

        /// <summary>
        /// UI模块
        /// </summary>
        public static UMUIModule UI { get; private set; }

        [SerializeField] private UMUIModule m_UIModule = null;

        private List<UMModule> m_moduleList = null;

        private UMiniConfig m_config = null;

        private void Awake()
        {
            if (GlobalInstance == null)
            {
                GlobalInstance = this;
                DontDestroyOnLoad(GlobalInstance);
            }
            else
            {
                Destroy(gameObject);
                UMUtils.Debug.Warning("Destroy duplicate UMEntity instances on awake");
            }
        }

        /// <summary>
        /// 启动UMini框架 不能在MonoBehaviour Awake函数中调用 建议在Start函数中调用
        /// </summary>
        public static void Launch(UMiniConfig config = null)
        {
            GlobalInstance.InitFramework(config);
        }

        private void InitFramework(UMiniConfig config)
        {
            UMUtils.Debug.Log(">>> UMini Launch");
            m_config = config;
            m_moduleList = new List<UMModule>();

            Audio = m_audioModule;
            Audio.InitPriority = 0;
            m_moduleList.Add(Audio);

            Scene = m_sceneModule;
            Scene.InitPriority = 0;
            m_moduleList.Add(Scene);

            Config = m_configModule;
            Config.InitPriority = 0;
            m_moduleList.Add(Config);

            PersiData = m_persiData;
            PersiData.InitPriority = 0;
            m_moduleList.Add(PersiData);

            Asset = m_assetModule;
            Asset.InitPriority = -1;
            m_moduleList.Add(Asset);

            UI = m_UIModule;
            UI.InitPriority = 0;
            m_moduleList.Add(UI);

            m_moduleList.Sort((x, y) => { return x.InitPriority > y.InitPriority ? 1 : -1; });

            // Debug.Log(m_moduleList);

            StartCoroutine(InitModules());
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <returns></returns>
        private IEnumerator InitModules()
        {
            foreach (var module in m_moduleList)
            {
                UMUtils.Debug.Log($"{module.GetType().Name} Start Init.");
                yield return module.Init(m_config);
            }

            UMUtils.Debug.Log(">>> UMini Launch Finished.");
            m_config?.OnLaunchFinished?.Invoke();
        }

        public class UMiniConfig
        {
            /// <summary>
            /// 启动成功的回调
            /// </summary>
            public Action OnLaunchFinished { get; set; }

            /// <summary>
            /// 资源加载器
            /// </summary>
            public IAssetLoader AssetLoader { get; set; }

            /// <summary>
            /// 配置表
            /// </summary>
            public List<UMConfigTable> ConfigTableList { get; set; }

            /// <summary>
            /// 持久化数据转换
            /// </summary>
            public IDataConverter DataConverter { get; set; }

            /// <summary>
            /// 数据持久化
            /// </summary>
            public IDataPersistenceHandler DataPers { get; set; }

            /// <summary>
            /// 是否在控制台显示持久化数据
            /// </summary>
            public bool IsPersiDataToConsole { get; set; }
        }
    }
}