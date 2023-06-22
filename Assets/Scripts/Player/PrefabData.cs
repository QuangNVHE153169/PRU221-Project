using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[System.Serializable]

public class PrefabData
{
    public string filePath;

    public PrefabData(GameObject gameObject)
    {
        if (gameObject == null)
            return;
        this.filePath = AssetDatabase.GetAssetPath(gameObject);
    }

    public GameObject ToGameObject()
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(this.filePath);
        return obj;
    }
}
