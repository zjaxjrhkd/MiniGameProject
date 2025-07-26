using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreater : MonoBehaviour
{
    public enum CreateType
    {
        Create,
        DownCheck
    }
    public CreateType type = CreateType.Create;
    public List<GameObject> blocks = new List<GameObject>();
    private Vector3 blockPosition = new Vector3(0, 6f, 0);
    public int blockStackCount = 0;
    public float createTimer = 5.0f;
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
    private void Update()
    {
        CreateCheck();
    }
    public void CreateCheck()
    {
        createTimer += Time.deltaTime;
        if (createTimer >= 10.0f)
        {
            UIManager_Block.Instance.UpdateHelp();
            createTimer = 0.0f;
        }
    }

    public void CreateBlock()
    {
        if (type==CreateType.Create)
        {
            if (blockStackCount > 25)
            {
                Instantiate(blocks[5], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 20)
            {
                Instantiate(blocks[4], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 15)
            {
                Instantiate(blocks[3], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 10)
            {
                Instantiate(blocks[2], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount > 5)
            {
                Instantiate(blocks[1], blockPosition, Quaternion.identity);
            }
            else if (blockStackCount >= 0)
            {
                Debug.Log("Creating Block: " + blockStackCount);
                Instantiate(blocks[0], blockPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(blocks[6], blockPosition, Quaternion.identity);
            }
            blockStackCount++;
            type = CreateType.DownCheck;
            createTimer = 0.0f;
        }
    }
    public void BlockDownCheck()
    {
        type = CreateType.Create;
    }
}
