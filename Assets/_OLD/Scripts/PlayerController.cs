using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Animator transitionAnimator;

    Rigidbody2D rb;
    public float leftRightspeed;
    public float jumpspeed;
    Animator animator;
    BoxCollider2D col;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    public AudioClip jumpClip;
    public AudioClip shootClip;
    public AudioClip gameOverClip;
    public AudioClip backgroundClip;
    public AudioClip collectableClip;

    AudioSource audioSource;

    int point;

    int playerFacingDirection = 1; // player is facing right direction.. 

    public Text pointText;
    public Text totalPointText;

    public GameObject gameOverPanel;

    bool isLeftButtonPressed;
    bool isRightButtonPressed;
    bool isJumpButtonPressed;
    bool isShootButtonPressed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        col = GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = backgroundClip;
        audioSource.Play();
    }
    private void Update()
    {
        if(isRightButtonPressed == true || Input.GetKey(KeyCode.D)) // right
        {
            playerFacingDirection = 1;

            rb.velocity = new Vector2(leftRightspeed, rb.velocity.y);

            transform.localScale = new Vector2(1, 1);

            PlayAnimation("walk");
        }

        else if (isLeftButtonPressed == true || Input.GetKey(KeyCode.A)) //left
        {
            playerFacingDirection = -1;

            rb.velocity = new Vector2(-leftRightspeed, rb.velocity.y);

            transform.localScale = new Vector2(-1, 1);

            PlayAnimation("walk");
        }

        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            PlayAnimation("idle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (col.IsTouchingLayers(groundLayer))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed);

                audioSource.PlayOneShot(jumpClip, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(playerFacingDirection * bulletSpeed, 0));

            audioSource.PlayOneShot(shootClip, 1f);
        }

    }

    private void PlayAnimation(string name)
    {
        if (col.IsTouchingLayers(groundLayer))
        {
            animator.Play(name);
        }
        else
        {
            animator.Play("nothing");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            audioSource.PlayOneShot(collectableClip, 1f);

            Destroy(collision.gameObject);

            point = point + 1;

            pointText.text = "Point: " + point;

        }

        if (collision.tag == "Enemy")
        {
            // Debug.Log("Game over");

            gameOverPanel.SetActive(true);

            totalPointText.text = "Total point: " + point;

            audioSource.PlayOneShot(gameOverClip, 1f);

        }

        if (collision.tag == "Level 1 End")
        {
            transitionAnimator.SetTrigger("Transition");

            Invoke("LoadScene2", 0.7f);

        }
        if (collision.tag == "Level 2 End")
        {

            Invoke("LoadScene0", 0.7f);
        }
    }

    void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void LeftButtonPressed()
    {
        isLeftButtonPressed = true;
    }

    public void LeftButtonReleased()
    {
        isLeftButtonPressed = false;
    }

    public void RightButtonPressed()
    {
        isRightButtonPressed = true;
    }

    public void RightButtonReleased()
    {
        isRightButtonPressed = false;
    }

    public void JumpButtonPressed()
    {
        if (col.IsTouchingLayers(groundLayer))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);

            audioSource.PlayOneShot(jumpClip, 1f);
        }
    }

    public void ShootButtonPressed()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(playerFacingDirection * bulletSpeed, 0));

        audioSource.PlayOneShot(shootClip, 1f);
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
