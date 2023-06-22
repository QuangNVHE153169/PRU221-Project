using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[System.Serializable]

public class BuffSkillData
{
    public float cdBuff;
    public BuffSkillStyle buffSkillStyle;
    public bool buffReady;
    public string introSpriteName;
    public string avatarSpriteName;

    public BuffSkillData(BuffSkill buffSkill)
    {
        if (buffSkill == null)
            return;
        cdBuff = buffSkill.cdBuff;
        buffSkillStyle = buffSkill.buffSkillStyle;
        buffReady = buffSkill.buffReady;
        introSpriteName = AssetDatabase.GetAssetPath(buffSkill.intro);
        avatarSpriteName = AssetDatabase.GetAssetPath(buffSkill.avatar);
    }

    public BuffSkill BuffSkill()
    {
        GameObject gameObject = new GameObject("BuffSkill");
        BuffSkill buffSkill = gameObject.AddComponent<BuffSkill>();
        buffSkill.cdBuff = this.cdBuff;
        buffSkill.buffSkillStyle = this.buffSkillStyle;
        buffSkill.buffReady = this.buffReady;
        buffSkill.intro = AssetDatabase.LoadAssetAtPath<Sprite>(this.introSpriteName);
        buffSkill.avatar = AssetDatabase.LoadAssetAtPath<Sprite>(this.avatarSpriteName);
        return buffSkill;
    }
}