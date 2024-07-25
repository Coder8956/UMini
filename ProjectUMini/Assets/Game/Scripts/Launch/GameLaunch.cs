using System.Collections.Generic;
using Game.Scripts.Const;
using Game.Scripts.PersistentData;
using UMiniFramework.Scripts;
using UMiniFramework.Scripts.Modules.AssetModule.AssetLoaders;
using UMiniFramework.Scripts.Modules.ConfigModule;
using UMiniFramework.Scripts.Modules.PersistentDataModule;
using UMiniFramework.Scripts.UMEntrance;
using UMiniFramework.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Launch
{
    public class GameLaunch : MonoBehaviour
    {
        [SerializeField] private Button m_btnEnterGame;

        private void Awake()
        {
            m_btnEnterGame.gameObject.SetActive(false);
            m_btnEnterGame.onClick.AddListener(() => { UMini.Scene.Load(SceneConst.LOGIN); });
        }

        // Start is called before the first frame update
        void Start()
        {
            UMiniConfig umConfig = new UMiniConfig();
            umConfig.OnLaunchFinished = () => { m_btnEnterGame.gameObject.SetActive(true); };
            umConfig.LaunchProgress = (tag, val) =>
            {
                UMUtils.Debug.Log($"UMini LaunchProgress: {tag}, prograssVal: {val}.");
            };
            umConfig.AssetLoader = new UMResourcesLoader();
            umConfig.IsPersiDataToConsole = true;
            umConfig.IsLog = true;
            umConfig.ConfigTableList = new List<UMConfigTable>()
            {
                new LevelTable(),
                new GameAudioTable(),
                new CannonTable(),
                new BulletTable(),
                new MonsterTable(),
            };
            umConfig.PersistentDataList = new List<UMPersistentData>()
            {
                new UserData(),
                new GameData(),
                new UIData(),
            };
            umConfig.EventTypeList = new List<string>()
            {
                GameEventConst.Launch,
                GameEventConst.Login
            };
            UMini.Launch(umConfig);
        }
    }
}