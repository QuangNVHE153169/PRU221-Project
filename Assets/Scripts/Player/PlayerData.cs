using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public float speed;
    public float rotationSpeed;
    public float maxHealth;
    public float curHealth;
    public TransformData gunSpawnPos;
    public FixedJoystickData joystick;
    public float bonusdame;
    public SliderData healthBar;

    public WeaponData firstWeapon;
    public BuffData firstBuff;
    public BuffSkillData firstBuffSkill;

    public bool isVisible;
    public bool isUndead;
    public Rigidbody2DData rb2d;
    public CameraData mainCamera;
    public WeaponData curWeapon;
    public BuffSkillData curBuffSkill;

    public SerializableVector2 movementInput;
    public SerializableVector2 movementInputSmooth;
    public SerializableVector2 velocityInputSmooth;
    public float unDeahHeath;

    public PlayerData(Player player)
    {
        if (player == null)
            return;
        position = new SerializableVector3(player.transform.position);
        rotation = new SerializableQuaternion(player.transform.rotation);
        speed = player.speed;
        rotationSpeed = player.rotationSpeed;
        maxHealth = player.maxHealth;
        curHealth = player.curHealth;
        gunSpawnPos = new TransformData(player.gunSpawnPos);
        joystick = new FixedJoystickData(player.joystick);
        bonusdame = player.bonusdame;
        healthBar = new SliderData(player.healthBar);
        firstWeapon = new WeaponData(player.firstWeapon);
        firstBuff = new BuffData(player.firstBuff);
        firstBuffSkill = new BuffSkillData(player.firstBuffSkill);
        isVisible = player.isVisible;
        isUndead = player.isUndead;
        rb2d = new Rigidbody2DData(player.rb2d);
        mainCamera = new CameraData(player.mainCamera);
        curWeapon = new WeaponData(player.curWeapon);
        curBuffSkill = new BuffSkillData(player.curBuffSkill);
        movementInput = new SerializableVector2(player.movementInput);
        movementInputSmooth = new SerializableVector2(player.movementInputSmooth);
        velocityInputSmooth = new SerializableVector2(player.velocityInputSmooth);
        unDeahHeath = player.unDeahHeath;
    }
    public void Player(Player player)
    {
        player.transform.position = this.position.Vector3();
        player.transform.rotation = this.rotation.Quaternion();
        player.speed = this.speed;
        player.rotationSpeed = this.rotationSpeed;
        player.maxHealth = this.maxHealth;
        player.curHealth = this.curHealth;
        this.gunSpawnPos.Transform(player.gunSpawnPos);
        this.joystick.FixedJoystick(player.joystick);
        player.bonusdame = this.bonusdame;
        this.healthBar.Slider(player.healthBar);
        player.firstWeapon= Weapon.ToWeapon(this.firstWeapon);
        this.firstBuff.Buff(player.firstBuff);
        player.firstBuffSkill = this.firstBuffSkill.BuffSkill();
        player.isVisible = this.isVisible;
        player.isUndead = this.isUndead;
        this.rb2d.Rigidbody2D(player);
        player.mainCamera = this.mainCamera.Camera();
        player.curWeapon = Weapon.ToWeapon(this.curWeapon);
        player.curWeapon.transform.SetParent(player.transform);
        player.curBuffSkill = this.curBuffSkill.BuffSkill();
        player.movementInput = this.movementInput.Vector2();
        player.movementInputSmooth = this.movementInputSmooth.Vector2();
        player.velocityInputSmooth = this.velocityInputSmooth.Vector2();
        player.unDeahHeath = this.unDeahHeath;
    }

}







