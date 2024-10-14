using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject gameOverPanel; 
    public GameObject shopPanel; 


    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI waveText; 
    public TextMeshProUGUI coinText; 
    public TextMeshProUGUI shopCoinText;
    public TextMeshProUGUI gameOverScoreText; 
    public TextMeshProUGUI gameOverWaveText;
    public TextMeshProUGUI missileText; // Para mostrar los misiles en el juego
    public TextMeshProUGUI shopMissileText; // Para mostrar los misiles en la tienda

    private int score; 
    private int maxWave;
    private int totalCoins; 

    void Start()
    {
        gameOverPanel.SetActive(false);
        shopPanel.SetActive(false);
        score = 0;
        maxWave = 0;
        totalCoins = 0;
        UpdateScoreUI();
        UpdateWaveUI();
        UpdateCoinUI();
    }

    public void PlayerDied()
    {
        gameOverPanel.SetActive(true);
        UpdateGameOverUI();
    }

    private void UpdateGameOverUI()
    {
        gameOverScoreText.text = "Final Score: " + score.ToString();
        gameOverWaveText.text = "Max Wave: " + maxWave.ToString();
    }

    public void EnemyDefeated(GameObject enemy)
    {
        score += 10;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void SetMaxWave(int wave)
    {
        maxWave = Mathf.Max(maxWave, wave); 
        UpdateWaveUI(); 
    }

    private void UpdateWaveUI()
    {
        waveText.text = "Wave: " + maxWave.ToString();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinUI(); 
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + totalCoins.ToString();

        if (shopPanel.activeSelf)
        {
            shopCoinText.text = "Coins: " + totalCoins.ToString();
        }
    }

    public int GetPlayerCoins()
    {
        return totalCoins; 
    }

    public void SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount; 
            UpdateCoinUI(); 
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        shopCoinText.text = "Coins: " + totalCoins.ToString();
        UpdateMissileUI(player.currentMissiles, player.maxMissiles);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OnCloseShopButtonClicked()
    {
        CloseShop();
    }
    public void UpdateMissileUI(int currentMissiles, int maxMissiles)
    {
        missileText.text = "Missiles: " + currentMissiles.ToString() + "/" + maxMissiles.ToString();

        // Actualizar la tienda si está abierta
        if (shopPanel.activeSelf)
        {
            shopMissileText.text = "Missiles: " + currentMissiles.ToString() + "/" + maxMissiles.ToString();
        }
    }
  
}
