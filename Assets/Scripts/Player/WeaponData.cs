using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[System.Serializable]

public class WeaponData
{
    public string prefabName;

    public WeaponStyle style;
    public int quantity;
    public GunBulletData normalBulletData;
    public GunBulletData ultiBulletData;
    public string introSpriteName;
    public string ultiSoundClipName;
    public string norSoundClipName;

    public float bulletForce;
    public float norCd;
    public float ultCd;
    public bool norReady;
    public bool ultReady;
    public TransformData hittf;
    public PrefabData explosivePrefab;
    public float damage;

    public WeaponData(Weapon weapon)
    {
        if (weapon == null)
            return;
        if (weapon.name.Contains("(Clone)"))
        {
            weapon.name = weapon.name.Replace("(Clone)", "");
        }
        prefabName = @$"Assets/Prefabs/Weapons/{weapon.name}.prefab";
        this.style = weapon.style;
        this.quantity = weapon.quantity;
        this.normalBulletData = new GunBulletData(weapon.normalBullet);
        this.ultiBulletData = new GunBulletData(weapon.ultiBullet);
        this.introSpriteName = AssetDatabase.GetAssetPath(weapon.intro);
        this.ultiSoundClipName = AssetDatabase.GetAssetPath(weapon.ultiSound);
        this.norSoundClipName = AssetDatabase.GetAssetPath(weapon.norSound);
        this.bulletForce = weapon.bulletForce;
        this.norCd = weapon.norCd;
        this.ultCd = weapon.ultCd;
        this.norReady = weapon.norReady;
        this.ultReady = weapon.ultReady;
        this.damage = weapon.damage;
        this.hittf = new TransformData(weapon.hittf);
        this.explosivePrefab = new PrefabData(weapon.explosivePrefabs);
    }
}
