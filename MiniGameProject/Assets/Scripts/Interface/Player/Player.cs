using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player;

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
        None,
        StackBlock,
        Run
    }
    private bool isGrounded = true;
    public PlayerState playerState = PlayerState.Idle;
    public PlayerGameMode playerGameMode = PlayerGameMode.None;

    private float _moveSpeed = 5f;
    public float jumpPower = 10f;
    private Vector2 _movementDirection;
    private Rigidbody2D rb;       

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
        float horizontal = Input.GetAxis("Horizontal");
        movementDirection = new Vector2(horizontal, 0).normalized;
        transform.position += new Vector3(movementDirection.x, 0, 0) * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch(playerGameMode)
            {
            case PlayerGameMode.StackBlock:
                // Implement stack block collision logic here
                Debug.Log("Player collided in StackBlock mode");
                break;
            case PlayerGameMode.Run:
                if (collision.gameObject.CompareTag("Ground"))
                {
                    isGrounded = true;
                }
                break;
            default:
                // Default collision logic
                break;
        }


    }
    void OnClothesChange()
    {
        // Implement clothes change logic here
        Debug.Log("Player's clothes have changed");
    }
}
