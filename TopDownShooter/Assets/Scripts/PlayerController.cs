using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private UIManager uiManager;


    // Armas
    public Weapon primaryWeapon; 
    public Weapon missileWeapon; 

    // Misiles
    public int maxMissiles;   
    public int currentMissiles;  

    // Salud y escudo
    public float maxShield;
    public float currentShield;
    public float maxHealth;
    public float currentHealth;

    public Slider shieldSlider;
    public Slider healthSlider;

    // Regeneración de escudo
    public float shieldRegenerationRate;
    public float shieldRegenerationDelay;
    private float timeSinceLastDamage;
    private bool isRegenerating = false;

    // Movimiento
    Vector2 moveDirection;
    Vector2 mousePosition;
    public float minX, maxX, minY, maxY;

    void Start()
    {
        currentShield = maxShield;
        currentHealth = maxHealth;
        currentMissiles = maxMissiles;

        uiManager = FindObjectOfType<UIManager>(); // Obtener referencia al UIManager

        shieldSlider.maxValue = maxShield;
        healthSlider.maxValue = maxHealth;
        UpdateHUD();

        uiManager.UpdateMissileUI(currentMissiles, maxMissiles); // Actualizar la UI de misiles al iniciar
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleShieldRegeneration();
        UpdateHUD();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb.MovePosition(newPosition);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            primaryWeapon.Fire();
        }

        if (Input.GetMouseButtonDown(1  ) && currentMissiles > 0)
        {
            missileWeapon.Fire();
            currentMissiles--;
            uiManager.UpdateMissileUI(currentMissiles, maxMissiles);
        }
    }

    public void TakeDamage(float amount)
    {
        if (currentShield > 0)
        {
            currentShield -= amount;

            if (currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0;
            }

            timeSinceLastDamage = 0;
            isRegenerating = false;
        }
        else
        {
            currentHealth -= amount;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HandleShieldRegeneration()
    {
        if (currentShield < maxShield)
        {
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= shieldRegenerationDelay)
            {
                isRegenerating = true;
            }

            if (isRegenerating)
            {
                currentShield += shieldRegenerationRate * Time.deltaTime;

                if (currentShield > maxShield)
                {
                    currentShield = maxShield;
                }
            }
        }
    }

    private void UpdateHUD()
    {
        shieldSlider.value = currentShield;
        healthSlider.value = currentHealth;
    }

    void Die()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.PlayerDied();
    }

    public void ReloadMissiles()
    {
        currentMissiles = maxMissiles;
        uiManager.UpdateMissileUI(currentMissiles, maxMissiles);
    }
}
