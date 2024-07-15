using System;
using System.Collections.Generic;
using Game.Scripts.Const;
using UMiniFramework.Scripts;
using UMiniFramework.Scripts.Modules.AssetModule.AssetLoaders;
using UMiniFramework.Scripts.Modules.ConfigModule;
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
            UMini.UMiniConfig umConfig = new UMini.UMiniConfig();
            umConfig.OnLaunchFinished = () => { m_btnEnterGame.gameObject.SetActive(true); };
            umConfig.ResourcesLoader = new ResourcesLoader();
            umConfig.ConfigTableList = new List<UMConfigTable>()
            {
                new BulletTable(),
                new MonsterTable(),
                new ScoreTable()
            };
            UMini.Launch(umConfig);
        }
    }
}