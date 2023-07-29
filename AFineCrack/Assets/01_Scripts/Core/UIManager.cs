using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    private float bestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetFloat("saveScore");
        _bestScoreText.text = bestScore.ToString() + "M";
    }

    public void UpdateScore(float score)
    {
        score = (int)score;
        _scoreText.text = score.ToString() + "M";
    }

    public void GameOverScore(float score)
    {
        score = (int)score;
        _gameOverScoreText.text = score.ToString() + "M";
    }

    public void UpdateBestScore(float score)
    {
        score = (int)score;

        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("saveScore", bestScore);
            _bestScoreText.text = bestScore.ToString() + "M";
        }
    }
}
