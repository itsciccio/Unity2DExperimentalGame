using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb2d;
    public Text coinText;
    public Text healthText;
    public Animator animator;
    public FirePointController firePoint;
    public SpriteRenderer spriteRenderer;
    public GameObject deathPrefab;
    public PlayerShoot playerShoot;
    public Slider slider;

    public float speed;
    public float jumpHeight;
    public int maxJumps = 2;
    public bool facingRight;
    public float health;
    public float maxHealth = 100;
    public int healthRegenRate = 2;
    public float spikeDamage = 15;
    public float regenTimer = 0f;

    bool RegeningHealth = false;
    bool damageOverTime = false;
    bool deathCompleted = false;
    bool disableGun = false;
    int jumps;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = maxHealth;
        slider.value = healthPercent();
        facingRight = true;
    }

    void FixedUpdate()
    {
        slider.value = healthPercent();

        healthText.text = "HP: " + health;

        if(health!=maxHealth && !RegeningHealth)
        {
            StartCoroutine(healthRegen());
        }

        if (health <= 0)
        {
            healthText.text = "HP: " + 0;
            playerShoot.disableGun = true;
            StartCoroutine(Die());
        }

        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        Flip(moveHorizontal);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumps = maxJumps;
        }

        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coinText.text = "Coin collected!";
        }

        if (collision.gameObject.tag == "enemy")
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            damageOverTime = true;
            StartCoroutine(Spiked(enemy.damageToPlayer));
            //RegenTimer();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            damageOverTime = false;
        }
    }

    private void Jump()
    {
        if (jumps > 0)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight) * Time.deltaTime, ForceMode2D.Impulse);
            jumps--;
        }
        if (jumps == 0) {
            return;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;
            transform.localScale = scale;

            firePoint.Flip();

        }
    }

    IEnumerator Spiked(float damage)
    {
        while (damageOverTime == true && deathCompleted == false)
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-350f, 0f));
            }
            else
            {
                rb2d.AddForce(new Vector2(350f, 0f));
            }
            health -= damage;
            spriteRenderer.color = new Color(164f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    float healthPercent()
    {
        return health / maxHealth;
    }

    void RegenTimer()
    {
        regenTimer = 0f;
        while(regenTimer != 4.0f)
        {
            regenTimer += 1.0f * Time.deltaTime;
        }
        Debug.Log(regenTimer);
        regenTimer = 0f;
        RegeningHealth = true;
        return;
    }

    IEnumerator healthRegen()
    {
        while(health!=maxHealth)
        {
            RegeningHealth = true;
            health += 2.5f;
            if (health >= maxHealth)
            {
                health = maxHealth;
                RegeningHealth = false;
                yield return null;
            }
            yield return new WaitForSeconds(healthRegenRate);
        }
 
    }

    IEnumerator Die()
    {
        spriteRenderer.enabled = false;
        if (deathCompleted == false)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
            deathCompleted = true;
        }
        else
        {
            
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        //game over scene

    }

}