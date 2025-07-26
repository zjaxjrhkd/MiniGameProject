using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;
using static BlockCreater;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        set,
        ready,
        stay,
    }
    private float speed = 10f;
    private float xMin = -9f;
    private float xMax = 9f;
    private float y = 6f;
    bool isMoving = true;
    public BlockType blockType = BlockType.ready;
    public Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveBlock();
        }
        if (Input.GetKeyDown(KeyCode.X)&& isMoving)
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
        col.isTrigger = false;
        blockType = BlockType.set;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block")&& (blockType==BlockType.set))
        {
            blockType = BlockType.stay;
            BlockCreater.Instance.BlockDownCheck();
            UIManager_Block.Instance.StartComboTimer();
        }
        else if (collision.gameObject.CompareTag("BackWall"))
        {
            blockType = BlockType.stay;
            UIManager_Block.Instance.ClearCombo();
            UIManager_Block.Instance.TakeHeart();
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block") && blockType == BlockType.ready)
        {
            UIManager_Block.Instance.isWin = true;
        }
    }


}
