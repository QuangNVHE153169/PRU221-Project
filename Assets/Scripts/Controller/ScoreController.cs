using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject prefab;
    public static ScoreController instance;
    public int score = 0;
    public const string ScorePreFix = "Score: ";
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = ScorePreFix + score.ToString();
        
    }

    public void Addpoint(int points)
    {
        score += points;
        scoreText.text = ScorePreFix + score.ToString();
        print($"Cộng {points} điểm");
    }
}
