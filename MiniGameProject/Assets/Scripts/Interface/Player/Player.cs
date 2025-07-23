using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IMove, IClothes
{
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
