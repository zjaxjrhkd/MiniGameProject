using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    public PlayerState playerState = PlayerState.Idle;
    public PlayerGameMode playerGameMode = PlayerGameMode.None;

    private float _moveSpeed = 5f;

    private Vector2 _movementDirection;
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
    public void OnMove()
    {
        switch (playerGameMode)
        {
            case PlayerGameMode.StackBlock:
                // Implement stack block movement logic here
                Debug.Log("Player is in StackBlock mode");
                break;
            case PlayerGameMode.Run:
                // Implement run mode movement logic here
                Debug.Log("Player is in Run mode");
                break;
            default:
                // Default movement logic
                MapMove();
                break;
        }
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
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        transform.position += new Vector3(movementDirection.x, movementDirection.y, 0) * MoveSpeed * Time.deltaTime;
    }
    void OnClothesChange()
    {
        // Implement clothes change logic here
        Debug.Log("Player's clothes have changed");
    }
}
