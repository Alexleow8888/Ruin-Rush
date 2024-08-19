using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Public can be seen in the Inspector
    // Character speed and jump speed
    public float speed = 5f;
    public float jumpspeed = 8f;
    // The direction the character is moving (+ for ->) (- for <-)
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Vector3 respawnPoint;
    // The starting position of the character
    public GameObject fallDetector;
    // Allows the fall detector to be attatched to an object

    private Animator playerAnimation;

    public Text scoreText;

    public Animator transition;

    // Start is called before the first frame update.
    void Start()
    {
        // Ensures that only the RigidBody2D component is looked at
        player = GetComponent<Rigidbody2D>();

        playerAnimation = GetComponent<Animator>();

        respawnPoint = transform.position;

        scoreText.text = "Score : " + Score.totalScore;

    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        // Makes a circle to detect if a player is touching the ground
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        direction = Input.GetAxis("Horizontal");
        if (direction > 0f)
        {
            // Makes player go right if the direction variable is positive
            player.velocity = new Vector2(direction * speed, player.velocity.y);

            transform.localScale = new Vector2(2f, 2f);
        }
        else if (direction < 0f)
        {
            // Makes player go left if the direction variable is negative
            player.velocity = new Vector2(direction * speed, player.velocity.y);

            transform.localScale = new Vector2(-2f, 2f);
        }
        else
        {
            // Player not moving
            player.velocity = new Vector2(0, player.velocity.y);
        }
        // Spacebar is "Jump" 
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            // Jump
            player.velocity = new Vector2(player.velocity.x, jumpspeed);
        }
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        print(isTouchingGround);

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Main Menu");
            Score.totalScore = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }

        else if (collision.tag == "NextLevel")
        {
            //print("hhhhhhhhhhhhhh");
            //transition.SetTrigger("Start");
            //yield return new WaitForSeconds(1);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //respawnPoint = transform.position;
            LoadNextLevel();
        }
        else if (collision.tag == "MenuReturn")
        {
            MenuReturn();
        }

    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Spike")
        {
            transform.position = respawnPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
            
        }
        //else if (collision.gameObject.tag == "Coin")
        //{
            //Score.totalScore += 1;
            //Debug.Log(Score.totalScore);
            //collision.gameObject.SetActive(false);
            //scoreText.text = "Score : " + Score.totalScore;
        //}
        else if (collision.gameObject.tag == "Gem")
        {
            Score.totalScore += 50;
            Debug.Log(Score.totalScore);
            collision.gameObject.SetActive(false);
            scoreText.text = "Score : " + Score.totalScore;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingGround = false;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void MenuReturn()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 4));
        Score.totalScore = 0;
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        Debug.Log("Triggered");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelIndex);
    }
 
}
