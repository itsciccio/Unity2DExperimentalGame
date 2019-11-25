using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;
    public float timer;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
      
    }

    void Update()
    {
        timer += 1.0f * Time.deltaTime;

        if(timer>= 5)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        if (col.gameObject.tag == "enemy")
        {
            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

    }

}
