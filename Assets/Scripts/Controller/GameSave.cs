using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSave : Singleton<GameSave>
{
    public bool isIntro;
    public bool isQues;
   
    // Start is called before the first frame update
    void Awake()
    {
        isQues = true;
        isIntro = true;
        DontDestroyOnLoad(this);
       
    }

}
