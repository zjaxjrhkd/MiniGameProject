using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStackGame : MonoBehaviour
{
    private BlockCreater blockCreater;

    void Start()
    {
        blockCreater = gameObject.GetComponent<BlockCreater>();
    }

    // Update is called once per frame
    void Update()
    {
        GameLoop();
    }

    public void GameLoop()
    {
        blockCreater.CreateBlock();
    }
}
