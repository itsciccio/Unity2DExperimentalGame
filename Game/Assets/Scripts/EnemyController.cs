using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public GameObject deathPrefab;
    public GameObject healthUI;
    public Slider slider;

    void Start()
    {
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
}
