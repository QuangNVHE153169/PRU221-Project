using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Scripts.SaveData
{
    [System.Serializable]
    public class EnemyData 
    {
        public string prefabName;
        public SerializableVector3 position;
        public SerializableQuaternion rotation;

        public string introSpriteName;
        public string explosivePrefabs;
        public BulletEnemiesData rangedBulletPrefabs;
        public EnemyType enemyType;
        public float currentHealth;
        public float maxHealth;
        public int damage;
        public float movementSpeed;
        public float attackSpeed;
        public bool isAlive;
        public bool isHunt;
        public SerializableVector3 endPoint;
        public float popular;
        public EnemyData(Enemies enemies)
        {
            if (enemies == null)
                return;
            prefabName = @$"Assets\Prefabs\Enemys\{enemies.gameObject.tag}.prefab";
            position = new SerializableVector3(enemies.transform.position);
            rotation = new SerializableQuaternion(enemies.transform.rotation);
            if (enemies.intro != null)
                introSpriteName = AssetDatabase.GetAssetPath(enemies.intro);
            if (enemies.explosivePrefabs != null)
                explosivePrefabs = AssetDatabase.GetAssetPath(enemies.explosivePrefabs);
            if (enemies.rangedBulletPrefabs != null)
                rangedBulletPrefabs = new BulletEnemiesData(enemies.rangedBulletPrefabs);
            enemyType = enemies.enemyType;
            currentHealth = enemies.currentHealth;
            maxHealth = enemies.maxHealth;
            damage = enemies.damage;
            movementSpeed = enemies.movementSpeed;
            attackSpeed = enemies.attackSpeed;
            isAlive = enemies.isAlive;
            isHunt = enemies.isHunt;
            if (enemies.endPoint != null)
                endPoint = new SerializableVector3(enemies.endPoint);
            popular = enemies.popular;

        }

      


        public static List<EnemyData> ListEnemiesData(List<Enemies> enemies)
        {
            List<EnemyData> enemiesData = new List<EnemyData>();
            foreach (var item in enemies)
            {
                enemiesData.Add(new EnemyData(item));
            }
            return enemiesData;
        }

        public static List<Enemies> ListEnemies(List<EnemyData> enemiesData)
        {
            List<Enemies> enemies = new List<Enemies>();
            foreach (var item in enemiesData)
            {
                Enemies e = Enemies.ToEnemies(item);
                Debug.Log("vi tri : " + e.transform.position.x);
                enemies.Add(e);
            }
            return enemies;
        }

    }

}
