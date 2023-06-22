using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 10.0f;
    [SerializeField] public float rotationSpeed = 600.0f;
    [SerializeField] public float maxHealth = 200;
    [SerializeField] public float curHealth;
    [SerializeField] public Transform gunSpawnPos;
    [SerializeField] public FixedJoystick joystick;
    [SerializeField] public float bonusdame = 0;
    [SerializeField] public Slider healthBar;

    public Weapon firstWeapon;
    public Buff firstBuff;
    public BuffSkill firstBuffSkill;

    public bool isVisible;
    public bool isUndead;
    //abc
    public Rigidbody2D rb2d;
    public Camera mainCamera;
    [SerializeField] public Weapon curWeapon;
    [SerializeField] public BuffSkill curBuffSkill;


    public Vector2 movementInput;
    public Vector2 movementInputSmooth;
    public Vector2 velocityInputSmooth;
    public float unDeahHeath;
    public delegate void WeaponChangedHandler();
    public static event WeaponChangedHandler OnWeaponChanged;

    public delegate void BuffSkillChangedHandler();
    public static event BuffSkillChangedHandler OnBuffSkillChanged;

    private void Awake()
    {
        isVisible = false;
        isUndead = false;
        GameManager.instance.player = this;
        healthBar.maxValue = 1;
        mainCamera = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        curWeapon = Instantiate(firstWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
        curWeapon.transform.SetParent(this.transform);
    }
    public void FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Enemies closestEnemy = null;
        Enemies[] allEnemies = GameObject.FindObjectsOfType<Enemies>();

        foreach (Enemies currentEnemy in allEnemies)
        {
            // Tính toán khoảng cách từ người chơi đến enemy hiện tại
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            // So sánh khoảng cách hiện tại với khoảng cách gần nhất đã tìm thấy trước đó
            if (distanceToEnemy < distance)
            {
                // Nếu khoảng cách mới nhỏ hơn khoảng cách gần nhất, cập nhật khoảng cách và lưu trữ enemy gần nhất
                distance = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        // Kiểm tra xem có enemy gần nhất không
        if (closestEnemy != null)
        {
            // Tính toán hướng bắn từ người chơi đến enemy gần nhất
            Vector2 direction = closestEnemy.transform.position - this.transform.position;
            direction.Normalize();

            // Gọi hàm Shoot() của weapon hiện tại để bắn theo hướng đã tính toán
            curWeapon.Shoot(direction);
            SetRotationInFindClosestEnemy(direction);
        }
    }
    public void Shoot()
    {
        curWeapon.Shoot(transform.up);
    }

    private void Update()
    {
        healthBar.value = curHealth / maxHealth;

        if (curWeapon.quantity <= 0)
        {
            ChangeWeapon(firstWeapon);
        }

        if (isUndead)
        {
            if (curHealth <= unDeahHeath) curHealth = unDeahHeath;
        }

        if (curHealth > maxHealth) curHealth = maxHealth;
    }

    private void LateUpdate()
    {
        //set camera follow
        if (this != null)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.x = transform.position.x;
            cameraPos.y = transform.position.y;
            mainCamera.transform.position = cameraPos;
        }
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb2d;
    }
    public Vector2 GetMovementInputSmooth()
    {
        return movementInputSmooth;
    }

    public BuffSkill GetCurBuffSkill()
    {
        return curBuffSkill;
    }
    public float GetCurHealth()
    {
        return curHealth;
    }
    public void SetCurHealth(float healthUndead)
    {
        curHealth = healthUndead;
    }

    private void FixedUpdate()
    {
        SetVelocityOfInput();
        SetRotationInDirectinOfInput();
        Dead();
    }


    private void SetVelocityOfInput()
    {
        movementInput = new Vector2(joystick.Horizontal, joystick.Vertical)
            + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movementInputSmooth = Vector2.SmoothDamp(movementInputSmooth,
            movementInput, ref velocityInputSmooth, 0.1f);

        rb2d.velocity = movementInputSmooth * speed;
    }



    private void SetRotationInDirectinOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotaion =
                Quaternion.LookRotation(transform.forward, movementInputSmooth);

            Quaternion rotation = Quaternion.RotateTowards
                (transform.rotation, targetRotaion, rotationSpeed * Time.deltaTime);

            rb2d.MoveRotation(rotation);
        }

    }
    private void SetRotationInFindClosestEnemy(Vector2 direction)
    {
        if (movementInput == Vector2.zero)
        {
            Quaternion targetRotaion =
               Quaternion.LookRotation(transform.forward, direction);

            Quaternion rotation = Quaternion.RotateTowards
                (transform.rotation, targetRotaion, rotationSpeed * Time.deltaTime);

            rb2d.MoveRotation(rotation);
        }
    }

    //public void Shoot()
    //{
    //    curWeapon.Shoot(transform.up);
    //}

    public void UltiShoot()
    {
        curWeapon.UltiShoot(transform.up);
    }

    public void BuffSkill()
    {
        if (curBuffSkill.buffReady)
        {
            switch (curBuffSkill.buffSkillStyle)
            {
                case BuffSkillStyle.boomSkill:
                    Invisible();
                    break;
                case BuffSkillStyle.dashSkill:
                    Dash();
                    break;
                case BuffSkillStyle.immortalSkill:
                    Undead();
                    break;
            }
            curBuffSkill.buffReady = false;
            StartCoroutine(CountDownBuff(curBuffSkill.cdBuff));
        }

    }

    IEnumerator CountDownBuff(float time)
    {
        yield return new WaitForSeconds(time);
        curBuffSkill.buffReady = true;
    }
    public void BuffUpdate(Buff b)
    {

        switch (b.style)
        {
            case BuffStyle.health:
                curHealth += b.quantity;
                break;
            case BuffStyle.speed:
                speed += b.quantity;
                break;
            case BuffStyle.strong:
                maxHealth += b.quantity;
                curHealth += b.quantity;
                break;
        }

    }

    private IEnumerator IEInvisible(float timeDuration)
    {

        isVisible = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(timeDuration);
        isVisible = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    private void Invisible()
    {
        StartCoroutine(IEInvisible(curBuffSkill.timeEffect));
    }

    private void Dash()
    {
        Vector2 force = 10000 * transform.up;
        rb2d.AddForce(force);
    }



    public void ChangeWeapon(Weapon newWeapon)
    {
        Destroy(curWeapon.gameObject);
        curWeapon = Instantiate(newWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
        curWeapon.transform.SetParent(this.transform);
        OnWeaponChanged?.Invoke();

    }

    public void ChangeBuffSkill(BuffSkill newBuff)
    {
        foreach (BuffSkill buff in GameManager.instance.BuffSkill)
        {
            if (buff.buffSkillStyle == newBuff.buffSkillStyle)
            {
                curBuffSkill = buff;
                break;
            }
        }
        OnBuffSkillChanged?.Invoke();

    }



    public void TakeDamge(float damage)
    {
        curHealth -= damage;
    }

    public void Dead()
    {
        if (curHealth <= 0)
        {
            ES3.DeleteFile("SaveFile.es3");
            SoundController.instance.PlayGameOver();
            ButtonControl.instance.GameOver();
        }
    }

    private IEnumerator IEUndead(float timeDuration)
    {
        isUndead = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.black;
        unDeahHeath = curHealth;
        yield return new WaitForSeconds(timeDuration);
        isUndead = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    private void Undead()
    {

        StartCoroutine(IEUndead(curBuffSkill.timeEffect));
    }

    public void SetPlayerWeaponWhenResume()
    {
        GameObject removeComponent = null;
        GameObject player = GameManager.instance.player.gameObject;
        // Lấy danh sách các game object con của playerNew

        List<GameObject> childObjects = new List<GameObject>();
        for (int i = 0; i < player.transform.childCount; i++)
        {
            childObjects.Add(player.transform.GetChild(i).gameObject);
        }

        // Kiểm tra từng game object con nếu chứa script Weapon thì xóa đi
        foreach (GameObject childObject in childObjects)
        {
            Weapon weapon = childObject.GetComponent<Weapon>();
            if (weapon != null)
            {
                removeComponent = childObject;
                Destroy(childObject);
                break;
            }
        }
        ES3AutoSaveMgr.Current.Load();
        GameObject playerNew = GameManager.instance.player.gameObject;
        // Lấy danh sách các game object con của playerNew

        List<GameObject> childObjectsNew = new List<GameObject>();
        for (int i = 0; i < playerNew.transform.childCount; i++)
        {
            childObjectsNew.Add(playerNew.transform.GetChild(i).gameObject);
        }
        Debug.Log("count after: " + childObjectsNew.Count);
        //Kiểm tra từng game object con nếu chứa script Weapon thì xóa đi
        foreach (GameObject childObject in childObjectsNew)
        {
            Debug.Log("childObject : " + childObject.name);
            Weapon weapon = childObject.GetComponent<Weapon>();
            if (weapon != null && removeComponent != childObject)
            {
                GameManager.instance.player.curWeapon = weapon;
                GameManager.instance.player.curWeapon.norReady = true;
            }
        }
    }
}
