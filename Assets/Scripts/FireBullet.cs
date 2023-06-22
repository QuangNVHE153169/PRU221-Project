using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    GameObject bossBulletPrefab;

    [SerializeField]
    private int bulletsAmount = 10;
    private float angle = 0f;
    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;
    public float involkTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BossShot", 0f, 3f);
    }

    public void BossShot()
    {
        int a = Random.Range(1, 4);
        if (a == 1 )
        {
            Fire();  
        }

        if (a == 2 )
        {
            DoubleSpiral();
        }   

        if (a == 3 )
        {
            Spiral();
        }
    }

    public void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject a = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
            a.GetComponent<BulletForBoss>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }


    public void DoubleSpiral()
    {
        StartCoroutine(DoubleSpiralShoot());
    }

    private IEnumerator DoubleSpiralShoot()
    {
        for (int j = 0; j < 20; j++)
        {
            for (int i = 0; i <= 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject a = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                a.GetComponent<BulletForBoss>().SetMoveDirection(bulDir);

            }
            angle += 10f;
            if (angle >= 360f)
            {
                angle = 0f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void Spiral()
    {
        StartCoroutine(SpiralShoot());
    }

    private IEnumerator SpiralShoot()
    {
        for (int i = 0; i < 20; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject a = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
            a.GetComponent<BulletForBoss>().SetMoveDirection(bulDir);

            angle += 20f;
            yield return new WaitForSeconds(0.1f);
        }  
    }
}
