using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;
    private UIManager uiManager; 

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        uiManager.AddCoins(coinValue); 
        Destroy(gameObject); 
    }
}
