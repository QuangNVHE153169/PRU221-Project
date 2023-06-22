using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBarControl : MonoBehaviour
{
    public Image imageCount;
    public float cooldown;
    // Start is called before the first frame update

    private void Awake()
    {
        cooldown = SoundController.instance.GameStart.length;
       
    }
    // Update is called once per frame
    void Update()
    {

        if (ButtonControl.instance.isGameStart)
        {
           
            imageCount.fillAmount -= 1 / cooldown * Time.unscaledDeltaTime;

        }
    }
}
