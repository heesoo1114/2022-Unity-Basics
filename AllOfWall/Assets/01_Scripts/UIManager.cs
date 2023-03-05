using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;

    private void Update()
    {
        string score = GameManager.instance.Score.ToString();
        scoreText.text = score;

        float a = GameManager.instance.SetTime;
        string timer = a.ToString("0.00");
        timerText.text = timer;
    }
}
