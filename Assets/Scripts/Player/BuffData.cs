using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[System.Serializable]

public class BuffData
{
    public BuffStyle style;
    public float quantity;
    public string introName;
    public string name;
    public string tag;
    public BuffData(Buff buff)
    {
        if (buff == null)
            return;
        style = buff.style;
        quantity = buff.quantity;
        introName = AssetDatabase.GetAssetPath(buff.intro);
        name = buff.name;
        tag = buff.tag;
    }
    public void Buff(Buff buff)
    {
        if (buff == null || (buff.style == 0 && buff.quantity == 0 && string.IsNullOrEmpty(buff.name) && string.IsNullOrEmpty(buff.tag)))
            return;
        buff.style = this.style;
        buff.quantity = this.quantity;
        buff.intro = AssetDatabase.LoadAssetAtPath<Sprite>(this.introName);
        buff.name = this.name;
        buff.tag = this.tag;
    }
}
