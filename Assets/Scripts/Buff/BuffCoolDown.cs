using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCoolDown : MonoBehaviour
{
    public Image imageCount;
    public float cooldown = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.instance.player.GetCurBuffSkill() != null)
        {
        cooldown = GameManager.instance.player.GetCurBuffSkill().cdBuff;
        
        
        }
        
       Player.OnBuffSkillChanged += UpdateCountdown;

    }
    public void UpdateCountdown()
    {
        // Get the current weapon
        if (GameManager.instance.player.GetCurBuffSkill() != null)
        {
            cooldown = GameManager.instance.player.GetCurBuffSkill().cdBuff;
        }
        else
        {
            cooldown= 0;
        }
        
        imageCount.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.GetCurBuffSkill() != null) { 
        if (!GameManager.instance.player.GetCurBuffSkill().buffReady)
        {
            float a = cooldown;
            imageCount.fillAmount += Time.deltaTime / a;
            if (imageCount.fillAmount >= 1)
            {
                GameManager.instance.player.GetCurBuffSkill().buffReady = true;
                imageCount.fillAmount = 0;
            }
        }
        }
        
    }
}
