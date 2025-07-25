using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager_Run : MonoBehaviour
{
    public GameObject stageUI;
    public GameObject resultUI;
    public GameObject inGameUI;
    public Text ResultText;
    public Text GetCoinText;
    public Text timer;
    public float time = 5f;
    public GameObject[] hearts;
    public int coinCount = 0;
    public Text coinText;


    public static UIManager_Run Instance { get; private set; }

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


    public void UpdateTimer()
    {
        time -= Time.deltaTime;
        timer.text = time.ToString("F2");
    }

    public void TakeFire()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].activeSelf)
            {
                hearts[i].SetActive(false);
                break;
            }
        }
    }
    public void TakeFood()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (!hearts[i].activeSelf)
            {
                hearts[i].SetActive(true);
                break;
            }
        }
    }
    public void TakeCoin()
    {
        coinCount++;
        coinText.text = coinCount.ToString();
    }

    public void ShowClearText()
    {   
        resultUI.SetActive(true);
        ResultText.text = "Stage Clear!";
        GetCoinText.text = "Get Coin: " + coinCount.ToString();
    }

    public void ShowfailText()
    {
        resultUI.SetActive(true);
        ResultText.text = "Stage fail!";
        GetCoinText.text = "Get Half Coin: " + (coinCount/2).ToString();
    }
}
