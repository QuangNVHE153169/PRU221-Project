using UnityEngine;
using System.Collections;

public enum BulletStyle
{
    bulletPistol,
    bulletFast,
    bulletStrong,
    Bom,
}

public class GunBullet : MonoBehaviour
{

    [SerializeField] public GameObject explosivePrefabs;

    public BulletStyle style;

    public float bulletLifeTime = 2f;




    public void Fire(Vector3 direction, float bulletForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }


    void Start()
    {
        DestroyBullet(bulletLifeTime);
    }

    void DestroyBullet(float Time)
    {
        switch (style)
        {
            case BulletStyle.bulletPistol:
                Destroy(gameObject, Time);
                break;
            case BulletStyle.bulletFast:
                Destroy(gameObject, Time);
                break;
            case BulletStyle.bulletStrong:
                StrongUltimateBullet(Time);
                break;
            case BulletStyle.Bom:
                BoomBullet(Time);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies e = collision.gameObject.GetComponent<Enemies>();
        if (e != null)
        {
            e.TakeDamage(GameManager.instance.player.curWeapon.damage);
            DestroyBullet(0f);
        }
    }

    public void StrongUltimateBullet(float time)
    {

        StartCoroutine(FireBulletsStrong(time));

    }

    private IEnumerator FireBulletsStrong(float time)
    {
        yield return new WaitForSeconds(time);
        GunBullet extraBullet = GameManager.instance.player.curWeapon.normalBullet;
        float halfConeAngle = (20 - 1) * 20f / 2f;
        //Vector2 direction = transform.right;

        GetComponent<Rigidbody2D>().velocity = transform.up * 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                float angle = j * 18f - halfConeAngle;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 rotatedDirection = rotation * transform.up;
                GunBullet bullet = Instantiate(extraBullet, transform.position, Quaternion.identity);
                bullet.Fire(rotatedDirection, 10f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);

    }

    public void BoomBullet(float time)
    {
        StartCoroutine(Boom(time));
    }
    private IEnumerator Boom(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody2D>().velocity = transform.up * 0;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(gameObject.transform.position, gameObject.transform.position + new Vector3(5, 5, 0));
        foreach (Collider2D collider in colliders)
        {
            Enemies e = collider.gameObject.GetComponent<Enemies>();
            if (e != null)
            {
                e.TakeDamage(GameManager.instance.player.curWeapon.damage);
                //Destroy(e.gameObject);
            }
        }
        Instantiate<GameObject>(explosivePrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}