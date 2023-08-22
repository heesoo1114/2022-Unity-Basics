using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave "+enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        float waveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (waveSpawnTimer <= 0)
        {
            SetWaveMessageText("");
        }
        else
        {
            SetWaveMessageText("Next Wave in "+waveSpawnTimer.ToString("F1")+"s");
        }
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }

    private void SetWaveMessageText(string text)
    {
        waveMessageText.SetText(text);
    }
}
