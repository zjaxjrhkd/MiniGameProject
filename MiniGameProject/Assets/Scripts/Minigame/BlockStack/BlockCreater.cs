using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreater : MonoBehaviour
{
    public List<GameObject> blocks = new List<GameObject>();
    private Vector3 blockPosition = new Vector3(0, 0, 7);
    public int blockStackCount = 0;
    bool isBlockDown = false;
    public static BlockCreater Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateBlock()
    {
        if (!isBlockDown)
        {
            isBlockDown = true;
            if (blockStackCount > 25)
            {
                Instantiate(blocks[6], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 20)
            {
                Instantiate(blocks[5], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 15)
            {
                Instantiate(blocks[4], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 10)
            {
                Instantiate(blocks[3], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 5)
            {
                Instantiate(blocks[2], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 0)
            {
                Instantiate(blocks[1], blockPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(blocks[0], blockPosition, Quaternion.identity);
            }
            blockStackCount++;
        }
    }
    public void BlockDownCheck()
    {
        isBlockDown = false;
    }
}
