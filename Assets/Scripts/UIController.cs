using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text waveText;
    [SerializeField] private Text livesText;

    private void OnEnable()
    {
        Spawner.OnWaveChanged += UpdateWaveText;
        GameManager.OnLivesChanged += UpdateLivesText;
    }

    private void OnDisable()
    {
        Spawner.OnWaveChanged -= UpdateWaveText;
        GameManager.OnLivesChanged -= UpdateLivesText;
    }

    private void UpdateWaveText(int currentWave)
    {
        waveText.text = $"Wave: {currentWave+1}";
    }

    private void UpdateLivesText(int currentLives)
    {
        livesText.text = $"Lives: {currentLives}";
    }
}
