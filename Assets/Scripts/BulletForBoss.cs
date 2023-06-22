using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForBoss : MonoBehaviour
{
    public float damage;
    private float moveSpeed;
    private Vector2 moveDirection;
    public float bulletLifeTime = 3f;

    private void OnEnable()
    {
        
        Invoke("Destroy", 10f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        DestroyBullet();
        
    }

    public void SetUp()
    {
        damage = 15f;
        moveSpeed = 5f;
    }

    // Update is called once per frame
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
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);   
    }

    public void DestroyBullet()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            GameManager.instance.player.TakeDamge(damage);
        }
    }
}
