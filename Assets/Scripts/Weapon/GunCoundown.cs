using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCoundown : MonoBehaviour
{
    public Image imageCount;
    public float cooldown;
    // Start is called before the first frame update
    void Awake()
    {
        cooldown = GameManager.instance.player.firstWeapon.norCd;

        Player.OnWeaponChanged += UpdateCountdown;

    }
    public void UpdateCountdown()
    {
        // Get the current weapon

        cooldown = GameManager.instance.player.curWeapon.norCd;
        imageCount.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.player.curWeapon.norReady)
        {
            float a = cooldown;
            imageCount.fillAmount += Time.deltaTime / a;
            if (imageCount.fillAmount >=1)
            {
                GameManager.instance.player.curWeapon.norReady = true;
                imageCount.fillAmount = 0;
            }
        }
    }
}
