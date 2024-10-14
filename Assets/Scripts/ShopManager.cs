using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerController player; 
    private UIManager uiManager; 

    public int healthRegenCost = 10;
    public int maxHealthIncreaseCost = 15;
    public int maxShieldIncreaseCost = 15;
    public int reloadMissilesCost = 5;
    public int missileCapacityIncreaseCost = 10;
    public int shieldRegenerationCost = 20;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); 
    }

    // Regenerar salud total
    public void RegenerateHealth(int cost)
    {
        if (uiManager.GetPlayerCoins() >= healthRegenCost)
        {
            uiManager.SpendCoins(healthRegenCost);
            player.currentHealth = player.maxHealth; 
            Debug.Log("Salud completamente regenerada. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    // Aumentar la salud máxima
    public void IncreaseMaxHealth(int cost)
    {
        if (uiManager.GetPlayerCoins() >= maxHealthIncreaseCost)
        {
            uiManager.SpendCoins(maxHealthIncreaseCost);
            player.maxHealth += 5; 
            player.currentHealth = player.maxHealth; 
            Debug.Log("Vida máxima aumentada. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    // Aumentar el escudo máximo
    public void IncreaseMaxShield(int cost)
    {
        if (uiManager.GetPlayerCoins() >= maxShieldIncreaseCost)
        {
            uiManager.SpendCoins(maxShieldIncreaseCost);
            player.maxShield += 5; 
            player.currentShield = player.maxShield; 
            Debug.Log("Escudo máximo aumentado. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    // Recargar misiles
    public void ReloadMissiles(int cost)
    {
        if (uiManager.GetPlayerCoins() >= reloadMissilesCost)
        {
            uiManager.SpendCoins(reloadMissilesCost);
            player.ReloadMissiles();
            uiManager.UpdateMissileUI(player.currentMissiles, player.maxMissiles);
            Debug.Log("Misiles recargados. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    // Aumentar la capacidad de misiles
    public void IncreaseMissileCapacity(int cost)
    {
        if (uiManager.GetPlayerCoins() >= missileCapacityIncreaseCost)
        {
            uiManager.SpendCoins(missileCapacityIncreaseCost);
            player.maxMissiles += 1;
            Debug.Log("Capacidad de misiles aumentada. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    // Regeneración de escudo
    public void IncreaseShieldRegeneration(int cost)
    {
        if (uiManager.GetPlayerCoins() >= shieldRegenerationCost)
        {
            uiManager.SpendCoins(shieldRegenerationCost);
            player.shieldRegenerationRate += 1; 
            Debug.Log("Regeneración de escudo aumentada. Monedas restantes: " + uiManager.GetPlayerCoins());
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }
}
