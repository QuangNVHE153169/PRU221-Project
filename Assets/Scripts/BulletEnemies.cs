using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum BulletType
{
    Ranged, 
    Boss
}

public class BulletEnemies : MonoBehaviour
{
    [SerializeField]
    public GameObject explosivePrefab;
    public BulletType typeBullet;
    public float damage;
    public Rigidbody2D rb2D;

    private void Awake()
    {
        SetUp();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void SetUp()
    {
        switch (typeBullet)
        {
            case BulletType.Ranged:
                damage = 15f;
                break;
        }
    }

    public void Update()
    {
        if (GameSave.instance.isIntro)
        {
            damage = 1;
        }
        else
        {
            SetUp();
        }
    }

    public void Project(Vector3 direction)
    { 
        rb2D.velocity = direction * 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.player.TakeDamge(damage);
        }
    }
}
