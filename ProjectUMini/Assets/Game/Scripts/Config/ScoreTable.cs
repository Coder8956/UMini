﻿// UMiniFramework config automatically generated, please do not modify it
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UMiniFramework.Scripts;
using UMiniFramework.Scripts.Utils;
using UMiniFramework.Scripts.Modules.ConfigModule;
using UnityEngine;

public class ScoreTable : UMConfigTable
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    public const string ConfigAssetPath = "Game/Resources/Config/score";
    public const string ConfigLoadPath = "Config/score";

    /// <summary>
    /// 包含在配置表中的数据
    /// </summary>
    public List<ScoreData> TableData { get; private set; }

    private Dictionary<string, ScoreData> m_dataDicById;

    /// <summary>
    /// 通过 Id 属性查询数据
    /// </summary>
    public ScoreData GetDataById(string id)
    {
        if (m_dataDicById.ContainsKey(id))
            return m_dataDicById[id];
        else
            UMUtils.Debug.Warning($"ScoreTable id does not exist {id}");
        return null;
    }

    public override IEnumerator Init()
    {
        m_dataDicById = new Dictionary<string, ScoreData>();
        string jsonCofig = string.Empty;
        UMini.Asset.LoadAsync<TextAsset>(ConfigLoadPath, (configData) =>{
        if (configData != null)
        {
            jsonCofig = configData.Resource.text;
            TableData = JsonConvert.DeserializeObject<List<ScoreData>>(jsonCofig);
            foreach (var data in TableData){
                m_dataDicById.Add(data.id, data);
            }
        UMUtils.Debug.Log($"Init Config: {GetType().FullName} Succeed.");
        }
        else
        {
            UMUtils.Debug.Warning($"config load failed. path: {ConfigLoadPath}");
        }});
        yield return new WaitUntil(() => { return TableData != null; });
    }
}