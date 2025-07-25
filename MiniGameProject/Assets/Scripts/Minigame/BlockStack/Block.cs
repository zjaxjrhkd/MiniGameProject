using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 3f;
    public float xMin = -9f;
    public float xMax = 9f;
    public float y = 7f;
    bool isMoving = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveBlock();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StopMoving();
        }
    }


    public void MoveBlock()
    {
        float center = (xMin + xMax) / 2f;
        float halfDistance = (xMax - xMin) / 2f;
        float x = center + Mathf.PingPong(Time.time * speed, (xMax - xMin)) - halfDistance;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            BlockCreater.Instance.BlockDownCheck();
        }
    }
}
