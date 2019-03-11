using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    public Text countText;
    public Text winText;
    public Text loseText;
    public Text livesText;
    public Animator animator;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    private bool facingRight;
    
    void Start()
    {
        facingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();
        lives = 3;
        livesText.text = "";
        SetLivesText();
    }

    // Update is called once per frame
    void Update ()
    {
        animator.SetFloat("speed", Mathf.Abs(speed));
    }

    void FixedUpdate()
    {

        if (Input.GetKey("escape"))
            Application.Quit();

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
            if (0 >= lives)
            {
                Destroy(gameObject);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            winText.text = "You win!";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives >= 0)
        {
            loseText.text = "You lose!";
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
        }
    }
}
