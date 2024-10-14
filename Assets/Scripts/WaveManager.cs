using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float timeBetweenWaves = 5f; 
    public int baseEnemiesPerWave; 
    public Transform[] spawnPoints;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            UpdateMaxWave();

            if (currentWave > 0) 
            {
                uiManager.OpenShop();

                yield return new WaitUntil(() => !uiManager.shopPanel.activeSelf);
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            SpawnWave();
            currentWave++;
        }
    }

    void SpawnWave()
    {
        int enemiesToSpawn = baseEnemiesPerWave + currentWave; 
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(enemy);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.health += 5 * currentWave; 
            enemyScript.moveSpeed += .2f * currentWave; 
        }
    }

    public void EnemyDefeated(GameObject enemy)
    {
        activeEnemies.Remove(enemy); 
    }

    private void UpdateMaxWave()
    {
        if (uiManager != null)
        {
            uiManager.SetMaxWave(currentWave); 
        }
    }

}
