using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : Singleton<SpawnManager>
{

    // Start is called before the first frame update
    public void BuffSpawn(Transform tf)
    {
        print("spawn buff");
        Buff f = null;
        BuffSkill bf = null;
        int r = Random.Range(0, 10);
        if (r < 2)
        {
            f = Instantiate(GameManager.instance.Buffs[Random.Range(0, 4)], tf.position, Quaternion.identity);
        }
        else if (r < 3)
        {
            bf = Instantiate(GameManager.instance.BuffSkill[Random.Range(0, 3)], tf.position, Quaternion.identity);
        }
        if (f != null)
            Destroy(f.gameObject, 10f);
        if (bf != null)
            Destroy(bf.gameObject, 10f);
    }

    public void StartSpawn()
    {
        if (GameSave.instance.isIntro)
        {
            IntroGame();
        }
        InvokeRepeating("SpawnEnemies", 0f, 10f);
        StartCoroutine(SpawnBosses());

    }


    public float AmountEnemy(Enemies enemyType)
    {
        return enemyType.popular * GameManager.instance.totalEnemies;
    }

    public void SpawnEnemies()
    {
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType != EnemyType.Boss && GameSave.instance.isIntro == false)
            {
                if (GameManager.instance.totalEnemies < 7)
                {
                    if (item.enemyType != EnemyType.Ranged && item.enemyType != EnemyType.Bee)
                    {
                        SpawnEachEnemy(item, 5);
                    }
                }
                else if (GameManager.instance.totalEnemies < 9)
                {
                    if (item.enemyType != EnemyType.Bee)
                    {
                        SpawnEachEnemy(item, AmountEnemy(item));
                    }
                }
                else if (GameManager.instance.totalEnemies >= 10)
                {
                    SpawnEachEnemy(item, AmountEnemy(item));
                }
            }
        }
    }

    public void SpawnEachEnemy(Enemies enemyType, float amount)
    {
        if (GameManager.instance.isBossAlive == false)
        {
            for (int i = 0; i < amount; i++)
            {
                Enemies enemies = Instantiate(enemyType, Gennerate(), Quaternion.identity);
                print($"spawn enemy {enemyType}");
                GameManager.instance.CurEnemies.Add(enemies);
            }
        }
    }
  

    public void SpawnBoss()
    {
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Boss && GameManager.instance.isBossAlive == false && GameSave.instance.isIntro == false)
            {
                GameManager.instance.isBossAlive = true;
                GameManager.instance.isUpgrade = false;
                Instantiate(item.gameObject, Gennerate(), Quaternion.identity);
            }
        }
    }

    private IEnumerator SpawnBosses()
    {
        while (true)
        {
            // Wait until the boss is destroyed
            yield return new WaitUntil(() => GameSave.instance.isIntro == false);
            yield return new WaitUntil(() => GameManager.instance.isBossAlive == false);

            // Wait for the spawn delay before spawning another boss
            yield return new WaitForSeconds(50);
            SpawnBoss();
        }
    }

    private void IntroGame()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.introControl.SetIntro(GameManager.instance.player.firstWeapon.intro);
        yield return new WaitForSeconds(1f);
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Ant)
            {
                SpawnEachEnemy(item, 1);
                GameManager.instance.introControl.SetIntro(item.intro);
            }
        }
        yield return new WaitUntil(() => GameManager.instance.isAntAliveIntro == false);
        Debug.Log("Ant Done");
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Bee)
            {
                SpawnEachEnemy(item, 1);
                GameManager.instance.introControl.SetIntro(item.intro);
            }
        }
        yield return new WaitUntil(() => GameManager.instance.isBeeAliveIntro == false);
        Debug.Log("Bee Done");

        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Ranged)
            {
                SpawnEachEnemy(item, 1);
                GameManager.instance.introControl.SetIntro(item.intro);

            }
        }
        yield return new WaitUntil(() => GameManager.instance.isRangedAliveIntro == false);
        Debug.Log("Ranged Done");
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Boss)
            {
                SpawnEachEnemy(item, 1);
                GameManager.instance.isBossAlive = true;
                GameManager.instance.introControl.SetIntro(item.intro);

            }
        }
        yield return new WaitUntil(() => GameManager.instance.isBossAlive == false);

        ButtonControl.instance.toggle.GetComponent<Toggle>().isOn = false;
        GameManager.instance.player.SetCurHealth(GameManager.instance.player.maxHealth);
    }

    public void SpawnWeapon(Transform tf)
    {
        Weapon w = null;
        int r = Random.Range(0, 20);
        if (r <= 4)
        {
            w = Instantiate(GameManager.instance.Weapons[Random.Range(1, 3)], tf.position, Quaternion.identity);
        }
        else if (r <= 5)
        {
            Instantiate(GameManager.instance.Weapons[3], tf.position, Quaternion.identity);
        }
        if (w != null)

            Destroy(w.gameObject, 10f);

    }


    public Vector3 Gennerate()
    {
        Vector3 position;
        do
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            // save screen edges in world coordinates
            float screenZ = -Camera.main.transform.position.z;
            Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
            Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
            Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
            Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
            float screenLeft = lowerLeftCornerWorld.x;
            float screenRight = upperRightCornerWorld.x;
            float screenTop = upperRightCornerWorld.y;
            float screenBottom = lowerLeftCornerWorld.y;
            position = new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop), -1);
        } while (Vector3.Distance(position, GameManager.instance.player.transform.position) < 10f);

        return position;
    }
}