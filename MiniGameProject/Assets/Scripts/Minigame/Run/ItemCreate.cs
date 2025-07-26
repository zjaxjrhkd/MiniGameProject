using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    public GameObject coin;
    public GameObject fire;
    public GameObject food;

    public List<GameObject> jumpPrefabs;

    public void ClearPattern()
    {         
        foreach (var obj in jumpPrefabs)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        jumpPrefabs.Clear();
    }

    public void SetPattern(int stageInfo)
    {
        switch(stageInfo)
        {
            case 1:
                CreatePatternJump();
                break;
            case 2:
                CreatePatternJump();
                break;
            case 3:
                CreatePatternJump();
                break;
            case 4:
                CreatePatternJump();
                break;
            case 5:
                CreatePatternJump();
                break;
            default:
                Debug.LogWarning("Unknown stage info: " + stageInfo);
                break;
        }
    }

    public void PatternOff(List<GameObject> objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }
    public void PatternOffJump()=> PatternOff(jumpPrefabs);

    public void PatternOn(List<GameObject> objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }
    public void CreatePatternJump()
    {
        float startX = 10f;
        float startY = -4f;
        float endY = 1.5f;
        float minY = -4f;
        float step = 0.5f;
        float x = startX;
        float y = startY;
        bool goingUp = true;

        while (true)
        {
            GameObject obj = Instantiate(coin, new Vector3(x, y, 0), Quaternion.identity);
            obj.SetActive(false);
            jumpPrefabs.Add(obj);
            x += step;

            if (goingUp)
            {
                y += step;
                if (y >= endY)
                {
                    y = endY;
                    goingUp = false;
                }
            }
            else
            {
                y -= step;
                if (y <= minY)
                {
                    y = minY;
                    break;
                }
            }
        }
        GameObject obj2 = Instantiate(fire, new Vector3(15, -4, 0), Quaternion.identity);
        obj2.SetActive(false);
        jumpPrefabs.Add(obj2);
        

    }
    public void ResetPatternJumpPositions()
    {
        float startX = 10f;
        float startY = -4f;
        float endY = 1.5f;
        float minY = -4f;
        float step = 0.5f;
        float x = startX;
        float y = startY;
        bool goingUp = true;

        int count = jumpPrefabs.Count;

        // 코인 패턴 위치 초기화 (fire 제외)
        for (int i = 0; i < count - 1; i++)
        {
            if (jumpPrefabs[i] != null)
                jumpPrefabs[i].transform.position = new Vector3(x, y, 0);

            x += step;

            if (goingUp)
            {
                y += step;
                if (y >= endY)
                {
                    y = endY;
                    goingUp = false;
                }
            }
            else
            {
                y -= step;
                if (y <= minY)
                {
                    y = minY;
                }
            }
        }
        jumpPrefabs[count - 1].transform.position = new Vector3(15, -4, 0);
    }
    public void CheckAndResetJumpPrefabs()
    {
        bool allInactive = true;
        foreach (var obj in jumpPrefabs)
        {
            if (obj != null && obj.activeSelf)
            {
                allInactive = false;
                break;
            }
        }
        if (allInactive)
        {
            ResetPatternJumpPositions();
            PatternOn(jumpPrefabs);
        }
    }
}
