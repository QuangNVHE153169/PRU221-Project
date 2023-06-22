//using System;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum EnemyType
{
    Ant,
    Ranged,
    Bee,
    Boss
}
public class Enemies : MonoBehaviour
{
    //New Commit
    public Sprite intro;
    //New Commit
    //Attribute and Property
    [SerializeField] public GameObject explosivePrefabs;
    public BulletEnemies rangedBulletPrefabs;
    public EnemyType enemyType;
    public float currentHealth;
    public float maxHealth;
    public int damage;
    public float movementSpeed;
    public float attackSpeed = 0;
    public bool isAlive;
    public bool isHunt;
    public Vector3 endPoint;
    public float popular;

    Timer timer;
    Timer timer1;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer1 = gameObject.AddComponent<Timer>();
        SetUp();
        currentHealth = maxHealth;
        isAlive = true;
        isHunt = false;
        endPoint = Gennerate();
        timer.Duarion = 2;
        timer.Run();
        timer1.Duarion = 3;
        timer1.Run();
    }

    public void SetUp()
    {

        switch (enemyType)
        {
            case EnemyType.Ant:
                popular = 0.7f;
                maxHealth = 50;
                damage = 2;
                movementSpeed = GameManager.instance.player.speed * 0.1f;
                break;
            case EnemyType.Ranged:
                popular = 0.2f;
                maxHealth = 40;
                damage = 10;
                movementSpeed = 5;
                break;
            case EnemyType.Bee:
                popular = 0.1f;
                maxHealth = 20;
                damage = 20;
                movementSpeed = 25f;
                attackSpeed = 50;
                break;
            case EnemyType.Boss:
                maxHealth = 1000;
                damage = 15;
                movementSpeed = 2;
                break;
        }
    }
    void Update()
    {
        if (GameSave.instance.isIntro)
        {
            damage = 1;
        }
        else
        {
            SetUp();
        }

        if (enemyType == EnemyType.Ant)
        {
            Hunt(GameManager.instance.player.transform.position, movementSpeed);
        }

        if (enemyType == EnemyType.Ranged)
        {
            Patrol();
        }

        if (enemyType == EnemyType.Bee)
        {
            Patrol();
        }

        if (enemyType == EnemyType.Boss)
        {
            Hunt(GameManager.instance.player.transform.position, movementSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;

            if (enemyType == EnemyType.Ant)
            {
                GameManager.instance.isAntAliveIntro = false;

                if (GameSave.instance.isIntro != true)
                {
                    ScoreController.instance.Addpoint(1);
                }
            }
            if (enemyType == EnemyType.Ranged)
            {
                GameManager.instance.isRangedAliveIntro = false;

                if (GameSave.instance.isIntro != true)
                {
                    ScoreController.instance.Addpoint(2);
                }
            }
            if (enemyType == EnemyType.Bee)
            {
                GameManager.instance.isBeeAliveIntro = false;
            }
            if (enemyType == EnemyType.Boss)
            {
                GameManager.instance.isBossAlive = false;

                Instantiate<GameObject>(explosivePrefabs, transform.position, Quaternion.identity);
                ScoreController.instance.Addpoint(4);
                if (!GameManager.instance.isUpgrade)
                {
                    GameManager.instance.UpgradeAttribute();
                    GameManager.instance.isUpgrade = true;
                }
                if (GameSave.instance.isIntro != true)
                {
                    ScoreController.instance.Addpoint(4);
                }
            }
            DestroyEnemies();
        }
    }

    public void Hunt(Vector3 player, float MovementSpeed)
    {
        if (enemyType == EnemyType.Ant)
        {
            if (GameManager.instance.player.isVisible == false)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                        player, MovementSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint, MovementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }

        }

        Vector3 po = player;
        if (enemyType == EnemyType.Bee)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                       po, MovementSpeed * Time.deltaTime);
        }

        Vector3 po1 = transform.position;

        if (enemyType == EnemyType.Boss)
        {
            if (GameManager.instance.player.isVisible == false)
            {
                if (Vector3.Distance(po1, player) < 10f)
                {
                    transform.position = po1;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(po1,
                                player, MovementSpeed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint, MovementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }

        }
    }


    public void AttackPlayer()
    {
        switch (enemyType)
        {
            case EnemyType.Ant:
                GameManager.instance.player.TakeDamge(damage);
                break;
            case EnemyType.Ranged:
                BulletEnemies bur = Instantiate(rangedBulletPrefabs, transform.position, Quaternion.identity);
                Vector3 dir = GameManager.instance.player.transform.position - transform.position;
                Debug.Log(dir);
                bur.Project(dir);
                break;
            case EnemyType.Bee:
                GameManager.instance.player.TakeDamge(damage);
                break;
        }
    }
    public void Patrol()
    {
        Vector3 po = transform.position;
        if (enemyType == EnemyType.Bee)
        {
            Vector3 pl = GameManager.instance.player.transform.position;
            if (Vector3.Distance(po, pl) < 10f && GameManager.instance.player.isVisible == false)
            {
                if (timer.Finished)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                       pl, movementSpeed);
                    Debug.Log("Bee Attack");
                    timer1.Duarion = 3;
                    timer1.Run();

                }
            }
            else
            {
                //Vector2 dir = endPoint - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //transform.rotation = rotation;

                transform.position = Vector3.MoveTowards(transform.position, endPoint, 10 * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }
        }

        if (enemyType == EnemyType.Ranged)
        {
            if (Vector3.Distance(po, GameManager.instance.player.transform.position) < 10f && GameManager.instance.player.isVisible == false)
            {
                transform.position = po;
                //Vector2 dir = endPoint - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //transform.rotation = rotation;
                if (timer.Finished)
                {
                    AttackPlayer();
                    timer.Duarion = 2;
                    timer.Run();
                }

            }
            else
            {
                //Vector2 dir = endPoint - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //transform.rotation = rotation;

                transform.position = Vector3.MoveTowards(transform.position, endPoint, 10 * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }
        }

    }

    public Vector3 Gennerate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        float screenLeft = lowerLeftCornerWorld.x;
        float screenRight = upperRightCornerWorld.x;
        float screenTop = upperRightCornerWorld.y;
        float screenBottom = lowerLeftCornerWorld.y;
        return new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop), -1);
    }

    //Check event takeDamage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            if (enemyType == EnemyType.Bee)
            {
                DestroyEnemies();
                if (GameSave.instance.isIntro != true)
                {
                    ScoreController.instance.Addpoint(3);
                }

                GameManager.instance.isBeeAliveIntro = false;
                AttackPlayer();

            }

            if (enemyType == EnemyType.Ant)
            {
                AttackPlayer();
            }
        }

    }

    public void DestroyEnemies()
    {
        SoundController.instance.PlayEnemyDead();
        SpawnManager.instance.BuffSpawn(this.transform);
        SpawnManager.instance.SpawnWeapon(this.transform);
        GameManager.instance.CurEnemies.Remove(this);
        Destroy(this.gameObject);
    }

    public static Enemies ToEnemies(EnemyData enemyData)
    {
        if (enemyData.prefabName == null)
            return null;
        GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(enemyData.prefabName);
        GameObject enemyObject = Instantiate(enemyPrefab);
        Enemies enemies = enemyObject.GetComponent<Enemies>();
        enemies.transform.position = enemyData.position.Vector3();
        enemies.transform.rotation = enemyData.rotation.Quaternion();
        enemies.intro = AssetDatabase.LoadAssetAtPath<Sprite>(enemyData.introSpriteName);
        if (enemyData.explosivePrefabs != null)
            enemies.explosivePrefabs = AssetDatabase.LoadAssetAtPath<GameObject>(enemyData.explosivePrefabs);
        if (enemyData.rangedBulletPrefabs != null)
            enemies.rangedBulletPrefabs = enemyData.rangedBulletPrefabs.BulletEnemies();
        enemies.enemyType = enemyData.enemyType;
        enemies.currentHealth = enemyData.currentHealth;
        enemies.maxHealth = enemyData.maxHealth;
        enemies.damage = enemyData.damage;
        enemies.movementSpeed = enemyData.movementSpeed;
        enemies.attackSpeed = enemyData.attackSpeed;
        enemies.isAlive = enemyData.isAlive;
        enemies.isHunt = enemyData.isHunt;
        enemies.endPoint = enemyData.endPoint.Vector3();
        enemies.popular = enemyData.popular;
        return enemies;
    }


}
