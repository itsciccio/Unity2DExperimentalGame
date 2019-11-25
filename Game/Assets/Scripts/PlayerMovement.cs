using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb2d;
    public Text coinText;
    public Animator animator;
    public FirePointController firePoint;

    public float speed;
    public float jumpHeight;
    public int maxJumps = 2;
    public bool facingRight;
    int jumps;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    void FixedUpdate()
    {
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
    }

    private void Jump()
    {
        if (jumps > 0)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight) * Time.deltaTime, ForceMode2D.Impulse);
            //isGrounded = false;
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

}