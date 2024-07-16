﻿using UMiniFramework.Scripts.Pool.GameObjectPool;
using UnityEditor;
using UnityEngine;

namespace UMiniFramework.Editor.UMInspectorEditor.GameObjectPoolInspector
{
    [CustomEditor(typeof(GameObjectPool))]
    [CanEditMultipleObjects]
    public class GameObjectPoolEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();

            GameObjectPool GoPool = (GameObjectPool) target;

            // 绘制默认的 Inspector GUI（包括其他字段）
            DrawDefaultInspector();
            
            GUI.enabled = false;
            // 使用反射访问和编辑属性
            EditorGUILayout.LabelField("ObjectCount", GoPool.ObjectCount.ToString());
            GUI.enabled = true;

            // 保存更改
            if (GUI.changed)
            {
                EditorUtility.SetDirty(GoPool);
            }
        }
    }
}