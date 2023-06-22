using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SaveData
{
    [SerializeField]
    public class BulletEnemiesData
    {
        public string explosivePrefab;
        public BulletType typeBullet;
        public float damage;
        private Rigidbody2DData rb2D;

        public BulletEnemiesData(BulletEnemies bulletEnemies)
        {   
            if (bulletEnemies == null){
                return;
            }
            if(bulletEnemies.explosivePrefab != null)
            this.explosivePrefab = AssetDatabase.GetAssetPath(bulletEnemies.explosivePrefab);
            this.typeBullet = bulletEnemies.typeBullet;
            this.damage = bulletEnemies.damage;
            this.rb2D = new Rigidbody2DData(bulletEnemies.rb2D);
        }

        public BulletEnemies BulletEnemies()
        {
            GameObject gameObject = new GameObject("BulletEnemies");
            BulletEnemies bulletEnemies = gameObject.AddComponent<BulletEnemies>();
            if (explosivePrefab != null)
                bulletEnemies.explosivePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(explosivePrefab);
            bulletEnemies.typeBullet = typeBullet;
            bulletEnemies.damage = damage;
            if (rb2D != null)
            {
                bulletEnemies.rb2D = rb2D.Rigidbody2D();
            }
            else
            {
                bulletEnemies.rb2D = gameObject.AddComponent<Rigidbody2D>();
            }

            return bulletEnemies;
           
        }
    }
}
