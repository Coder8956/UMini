﻿using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.PersistentData;
using Newtonsoft.Json;
using UMiniFramework.Scripts.Modules.ConfigModule;
using UMiniFramework.Scripts.Utils;
using UnityEngine;
using Object = System.Object;

namespace UMiniFramework.Scripts.Modules.PersistentDataModule
{
    public class UMPersistentDataModule : UMModule
    {
        private bool m_isPersiDataToConsole = true;
        private Dictionary<string, UMPersistentData> m_persistentDataDic;
        private const string m_persiDataFileExtend = ".json";
        private string m_persiDataRootPath;

        public override IEnumerator Init(UMini.UMiniConfig config)
        {
            m_persiDataRootPath = UMPersistentDataRootDir.GetRootDir();
            m_persistentDataDic = new Dictionary<string, UMPersistentData>();
            m_isPersiDataToConsole = config.IsPersiDataToConsole;

            UMUtils.IO.CreateDir(m_persiDataRootPath);

            if (config.PersistentData != null)
            {
                for (var i = 0; i < config.PersistentData.Count; i++)
                {
                    UMPersistentData data = config.PersistentData[i];
                    string key = GetDataKey(data);
                    m_persistentDataDic.Add(key, InitPersistentData(key, data));
                }
            }

            yield return null;
        }

        private UMPersistentData InitPersistentData(string key, UMPersistentData defaultData)
        {
            UMPersistentData PersiData = null;
            string filePath = GetDataFilePath(key);
            if (UMUtils.IO.IsExistsFile(filePath))
            {
                string jsonStr = UMUtils.IO.FileReadAllText(filePath);
                PersiData = JsonConvert.DeserializeObject(jsonStr, defaultData.GetType()) as UMPersistentData;
            }
            else
            {
                string jsonStr = defaultData.ToJson();
                UMUtils.IO.FileWriteAllText(filePath, jsonStr);
                PersiData = defaultData;
            }

            return PersiData;
        }

        private string GetDataFilePath(string fileName)
        {
            return string.Concat(m_persiDataRootPath, $"/{fileName}", m_persiDataFileExtend);
        }

        private string GetDataKey(UMPersistentData data)
        {
            return data.GetType().Name;
        }

        private string GetDataKey<T>() where T : UMPersistentData, new()
        {
            return typeof(T).Name;
        }

        private bool IsPrintLog()
        {
            return (m_isPersiDataToConsole);
        }

        public T Write<T>() where T : UMPersistentData, new()
        {
            string key = GetDataKey<T>();
            if (m_persistentDataDic.ContainsKey(key))
            {
                return m_persistentDataDic[key] as T;
            }
            else
            {
                UMUtils.Debug.Warning($"UMPersistentDataDic The key [{key}] does not exist.");
                return null;
            }
        }

        public void Save<T>() where T : UMPersistentData, new()
        {
            string key = GetDataKey<T>();
            if (m_persistentDataDic.ContainsKey(key))
            {
                string filePath = GetDataFilePath(key);
                string jsonStr = m_persistentDataDic[key].ToJson();
                if (IsPrintLog())
                {
                    string log = $"[Save] key:{key};\n{jsonStr}";
                    UMUtils.Debug.Log(log);
                }

                UMUtils.IO.FileWriteAllText(filePath, jsonStr);
            }
            else
            {
                UMUtils.Debug.Warning($"UMPersistentDataDic The key [{key}] does not exist.");
            }
        }

        public void ResetDefault<T>() where T : UMPersistentData, new()
        {
            string key = GetDataKey<T>();
            if (m_persistentDataDic.ContainsKey(key))
            {
                m_persistentDataDic[key] = new T();
            }
            else
            {
                UMUtils.Debug.Warning($"UMPersistentDataDic The key [{key}] does not exist.");
            }
        }

        public void SaveAllData()
        {
            foreach (var kv in m_persistentDataDic)
            {
                string filePath = GetDataFilePath(kv.Key);
                string jsonStr = kv.Value.ToJson();
                if (IsPrintLog())
                {
                    string log = $"[SaveAllData] key:{kv.Key};\n{jsonStr}";
                    UMUtils.Debug.Log(log);
                }

                UMUtils.IO.FileWriteAllText(filePath, jsonStr);
            }
        }
    }
}