using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public float damageToPlayer = 60;

    public SpriteRenderer spriteRenderer;
    public GameObject deathPrefab;
    public GameObject healthUI;
    public Slider slider;
    public Color originalColor; 

    void Start()
    {
        originalColor = spriteRenderer.color;
        health = maxHealth;
        slider.value = healthPercent();
        healthUI.SetActive(false);
    }

    void Update()
    {        
        slider.value = healthPercent();

        if (health < maxHealth)
        {
            healthUI.SetActive(true);
        }
    }

    float healthPercent()
    {
        return health / maxHealth;
    }

    public void TakeDamage(int damage)
    {        
        StartCoroutine(FlashDamage());
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);        
    }

    IEnumerator FlashDamage()
    {
        spriteRenderer.color = new Color(164f, 0f, 0f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        yield return null;
    }
}
