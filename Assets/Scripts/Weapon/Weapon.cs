using UnityEngine;
using System.Collections;
using Assets.Scripts.SaveData;
using UnityEditor;

public enum WeaponStyle
{
    Pistol,
    FastGun,
    StrongGun,
    Bom,
}
public class Weapon : MonoBehaviour
{
    public WeaponStyle style;
    public int quantity;
    public GunBullet normalBullet;
    public GunBullet ultiBullet;
    public Sprite intro;
    public AudioClip ultiSound;
    public AudioClip norSound;

    public float bulletForce;

    public float norCd;
    public float ultCd;

    public bool norReady;
    public bool ultReady;

    public Transform hittf;
    public GameObject explosivePrefabs;    

    public float damage;
  
    private void Awake()
    {
        SetUp();
    }

    public void SetUp()
    {
        switch (style)
        {
            case WeaponStyle.Pistol:
                quantity = 1000;
                damage = 10;
                norCd = 0.5f;
                ultCd = 5f;
                bulletForce = 15;
                break;
            case WeaponStyle.FastGun:
                quantity = 10;
                damage = 15;
                norCd = 0.2f;
                ultCd = 8f;
                bulletForce = 30;
                break;
            case WeaponStyle.StrongGun:
                quantity = 5;
                damage = 50;
                norCd = 0.8f;
                ultCd = 8f;
                bulletForce = 15;
                break;
            case WeaponStyle.Bom:
                quantity = 1;
                damage = 100;
                norCd = 1f;
                ultCd = 10f;
                bulletForce = 10;
                break;
        }
    }




    public void Shoot(Vector2 direction)
    {
        if (norReady)
        {
            SoundController.instance.PlaySound(norSound);
            Instantiate<GameObject>(explosivePrefabs, hittf.position, Quaternion.identity);
            GunBullet bullet = Instantiate(normalBullet, hittf.position, Quaternion.identity);
            bullet.Fire(direction, bulletForce);
            norReady = false;
            StartCoroutine(CountDownShoot(norCd));
            
        }
    }

    public void UltiShoot(Vector2 direction)
    {
        if (ultReady)
        {
            switch (style)
            {
                case WeaponStyle.Pistol:
                    BoomShotUlti(direction);
                    break;
                case WeaponStyle.FastGun:
                    FastGunUlti();
                    break;
                case WeaponStyle.StrongGun:
                    StronngShotUlti(direction);
                    break;
                case WeaponStyle.Bom:
                    UltimateSkillBom();
                    break;
            }
            SoundController.instance.PlaySound(ultiSound);
            quantity -= 1;
            ultReady = false;
            StartCoroutine(CountDownUtil(ultCd));
        }

    }

    IEnumerator CountDownShoot(float time)
    {
        yield return new WaitForSeconds(time);
        norReady = true;
    }

    IEnumerator CountDownUtil(float time)
    {
        yield return new WaitForSeconds(time);
        ultReady = true;
    }

    public void StronngShotUlti(Vector2 direction)
    {
        GunBullet bullet = Instantiate(ultiBullet, hittf.position, Quaternion.identity);
        bullet.Fire(direction, bulletForce);
    }
    public void FastGunUlti()
    {
        StartCoroutine(FireBulletsInCone());
    }
    public void BoomShotUlti(Vector2 direction)
    {
        GunBullet bullet = Instantiate(ultiBullet, hittf.position, Quaternion.identity);
        bullet.Fire(direction, bulletForce);
    }



    public void UltimateSkillBom()
    {
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("1");
            Enemies e = collider.gameObject.GetComponent<Enemies>();
            if (e != null)
            {
                if (e.enemyType != EnemyType.Boss)
                {
                    e.TakeDamage(10000);
                }
                else
                {
                    e.TakeDamage(20);
                }

            }
        }
        GameManager.instance.player.TakeDamge(20);
        quantity = 0;


    }

    private IEnumerator FireBulletsInCone()
    {
        float halfConeAngle = (6 - 1) * 6f / 2f;
        Vector2 direction = transform.right;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float angle = j * 6f - halfConeAngle;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 rotatedDirection = rotation * direction;
                GunBullet bullet = Instantiate(normalBullet, hittf.position, Quaternion.identity);
                bullet.Fire(rotatedDirection, bulletForce);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {


            if (GameManager.instance.isFastGun && style == WeaponStyle.FastGun)

            {
                GameManager.instance.introControl.SetIntro(intro);
                GameManager.instance.isFastGun = false;
            }
            else if (GameManager.instance.isStrongGun && style == WeaponStyle.StrongGun)
            {
                GameManager.instance.introControl.SetIntro(intro);
                GameManager.instance.isStrongGun = false;
            }
            else if (GameManager.instance.isBoomGun && style == WeaponStyle.Bom)
            {
                GameManager.instance.introControl.SetIntro(intro);
                GameManager.instance.isBoomGun = false;
            }
            p.ChangeWeapon(this);
            Destroy(gameObject);
        }
            
        }
    public static Weapon ToWeapon(WeaponData weaponData)
    {

        GameObject weaponPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(weaponData.prefabName);
        GameObject weaponObject = null ;
        if (weaponPrefab != null)
        {
            weaponObject = Instantiate(weaponPrefab);
        }
       
        Weapon weapon = weaponObject.GetComponent<Weapon>();

        weapon.style = weaponData.style;
        weapon.quantity = weaponData.quantity;
        if (weaponData.normalBulletData != null)
        {
            weaponData.normalBulletData.GunBullet(weapon.normalBullet);
        }
        if (weaponData.ultiBulletData != null)
        {
            weaponData.ultiBulletData.GunBullet(weapon.ultiBullet);
        }
        weapon.intro = AssetDatabase.LoadAssetAtPath<Sprite>(weaponData.introSpriteName);
        weapon.ultiSound = AssetDatabase.LoadAssetAtPath<AudioClip>(weaponData.ultiSoundClipName);
        weapon.norSound = AssetDatabase.LoadAssetAtPath<AudioClip>(weaponData.norSoundClipName);
        weapon.bulletForce = weaponData.bulletForce;
        weapon.norCd = weaponData.norCd;
        weapon.ultCd = weaponData.ultCd;
        weapon.norReady = weaponData.norReady;
        weapon.ultReady = weaponData.ultReady;
        weapon.damage = weaponData.damage;
        if (weaponData.hittf != null)
        {
            weaponData.hittf.Transform(weapon.hittf);
        }
        if (weaponData.explosivePrefab != null)
        {
            weapon.explosivePrefabs = weaponData.explosivePrefab.ToGameObject();
        }
        return weapon;
    }
}
