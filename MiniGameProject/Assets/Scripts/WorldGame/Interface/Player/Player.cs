using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IMove, IClothes
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping
    }
    public enum PlayerGameMode
    {
        Map,
        StackBlock,
        Run,
        None
    }
    private bool isGrounded = true;
    public PlayerState playerState = PlayerState.Idle;
    public PlayerGameMode playerGameMode = PlayerGameMode.None;

    private float _moveSpeed = 5f;
    public float jumpPower = 30f;
    private Vector2 _movementDirection;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Vector2 movementDirection
    {
        get { return _movementDirection; }
        private set { _movementDirection = value; }
    }
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        private set { _moveSpeed = value; }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void OnMove()
    {
        switch (playerGameMode)
        {
            case PlayerGameMode.StackBlock:

                break;
            case PlayerGameMode.Run:
                RunMove();
                break;
            default:
                MapMove();
                break;
        }
    }
    public void OnRunGame()
    {   
        playerGameMode = PlayerGameMode.Run;
    }

    public void MapMove()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        transform.position += new Vector3(movementDirection.x, movementDirection.y, 0) * MoveSpeed * Time.deltaTime;
    }



    public void RunMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        movementDirection = new Vector2(horizontal, 0).normalized;
        rb.velocity = new Vector2(movementDirection.x * _moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.X) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        switch(playerGameMode)
            {
            case PlayerGameMode.StackBlock:
                break;
            case PlayerGameMode.Run:
                if (other.gameObject.CompareTag("Ground"))
                {
                    isGrounded = true;
                }
                break;
            default:
                break;
        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (playerGameMode)
        {
            case PlayerGameMode.StackBlock:

            case PlayerGameMode.Run:
                if (other.gameObject.CompareTag("Obstacle"))
                {
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    if (spriteRenderer != null)
                        StartCoroutine(BlinkSprite(1f, 0.1f));
                }
                break;
            default:
                break;
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Building1") && (Input.GetKeyDown(KeyCode.X)))
        {
            Debug.Log("Building1 Triggered");
            SceneManager.LoadScene("2.RunScene");
        }
    }


    IEnumerator BlinkSprite(float duration, float blinkInterval)
    {
        float timer = 0f;
        bool visible = true;
        while (timer < duration)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true; 
    }

    void OnClothesChange()
    {
        // Implement clothes change logic here
        Debug.Log("Player's clothes have changed");
    }
}
