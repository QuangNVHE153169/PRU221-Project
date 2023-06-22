using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroControl : MonoBehaviour
{

    public Image introImage;

    private void Start()
    {
        GameManager.instance.introControl = this;
        gameObject.SetActive(false);


    }
    public void SetIntro(Sprite image)
    {
        ButtonControl.instance.isShowIntro = true;
        this.gameObject.SetActive(true);
        introImage.sprite = image;
        Time.timeScale = 0;
    }
   

    public void SkipIntro()
    {
        if (!ButtonControl.instance.isGamePause)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            ButtonControl.instance.isShowIntro = false;
        }
    }


   

}
