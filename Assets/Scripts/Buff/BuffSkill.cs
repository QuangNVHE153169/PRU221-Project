using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffSkillStyle
    {
        dashSkill, immortalSkill, boomSkill
    }
public class BuffSkill : MonoBehaviour
{

    public float cdBuff;
    public BuffSkillStyle buffSkillStyle;
    public bool buffReady;
    public Sprite intro;
    public Sprite avatar;

    public float timeEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {
            if (GameSave.instance.isIntro)
            {
            if (GameManager.instance.isDashInfo && buffSkillStyle == BuffSkillStyle.dashSkill)
            {
                GameManager.instance.introControl.SetIntro(intro);
                GameManager.instance.isDashInfo = false;
            }
            else if (GameManager.instance.isBoomInfo && buffSkillStyle == BuffSkillStyle.boomSkill)
            {
                GameManager.instance.introControl.SetIntro(intro);

                GameManager.instance.isBoomInfo = false;
            }
            else if (GameManager.instance.isImmortalInfo && buffSkillStyle == BuffSkillStyle.immortalSkill)
            {
                GameManager.instance.introControl.SetIntro(intro);

                GameManager.instance.isImmortalInfo = false;
            }
            }
           
           
            p.ChangeBuffSkill(this);
            GameManager.instance.skillButton.ChangeAvatar(avatar);
            Destroy(this.gameObject);

        }
    }
}
