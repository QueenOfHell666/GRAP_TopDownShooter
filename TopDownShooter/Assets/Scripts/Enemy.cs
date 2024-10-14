using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float attackRange;
    public GameObject projectilePrefab; 
    public float attackCooldown;    
    public float fireForce;
    public GameObject coinPrefab; 
    public float dropChance; 

    private Transform player;
    private float lastAttackTime;
    private WaveManager waveManager; 
    private UIManager uiManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waveManager = FindObjectOfType<WaveManager>(); 
        uiManager = FindObjectOfType<UIManager>(); 
    }

    void Update()
    {
        if (player != null)
        {
            SeekPlayer();
        }
    }

    void SeekPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > attackRange)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DropCoin(); 
            waveManager.EnemyDefeated(gameObject); 
            uiManager.EnemyDefeated(gameObject); 
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= dropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
