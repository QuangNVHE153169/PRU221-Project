using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[System.Serializable]

public class GunBulletData
{
    public string explosivePrefabs;
    public BulletStyle style;
    public float bulletLifeTime;
    public GunBulletData(GunBullet bullet)
    {
        if (bullet == null)
            return;
        this.explosivePrefabs = AssetDatabase.GetAssetPath(bullet.explosivePrefabs);
        this.style = bullet.style;
        this.bulletLifeTime = bullet.bulletLifeTime;
    }

    public void GunBullet(GunBullet gunBullet)
    {
        gunBullet.explosivePrefabs = AssetDatabase.LoadAssetAtPath<GameObject>(this.explosivePrefabs);
        gunBullet.style = this.style;
        gunBullet.bulletLifeTime = this.bulletLifeTime;

    }
}