using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSence : MonoBehaviour
{
    public GameObject loadingSence;
    public Image loadingFill;

    void Start()
    {
        LoadSence(1);
    }
    public void LoadSence(int senceId)
    {
        StartCoroutine(loadSceneAsyc(senceId));
    }
    IEnumerator loadSceneAsyc(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
