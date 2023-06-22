using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCircle : MonoBehaviour
{
    public float duration = 45f;
    public Vector3 startScale = new Vector3(1f, 1f, 1f);
    public float speedMultiplier = 3f;

    private float scaleSpeed;
    private Vector3 initialScale;
    private bool isScaling = false;
    private Spawncircle spawner;
    private void Start()
    {
        spawner = FindObjectOfType<Spawncircle>();
        initialScale = startScale;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ButtonControl.instance.isBuffCircle = true;
            spawner.isPlayerInCircle = true;
        }
        if (collision.CompareTag("Player") && !isScaling)
        {
            Debug.Log("ða vao");
            CountdownTime();
            isScaling = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.isPlayerInCircle = false;
            ButtonControl.instance.isBuffCircle = false;
        }
    }
    private void CountdownTime()
    {
        scaleSpeed = startScale.x / duration * speedMultiplier;
        transform.localScale = initialScale;

        StartCoroutine(DecreaseSize());
    }

    private System.Collections.IEnumerator DecreaseSize()
    {
        while (transform.localScale.x > 0f)
        {
            float scaleFactor = scaleSpeed * Time.deltaTime;
            transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0f);

            yield return null;
        }
        Destroy(gameObject);
    }

}
