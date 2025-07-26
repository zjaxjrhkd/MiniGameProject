using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Block : MonoBehaviour
{
    public GameObject stageUI;
    public GameObject resultUI;
    public GameObject inGameUI;
    public Text ResultText;
    public Text GetCoinText;
    public Text timer;
    public GameObject comboUI;
    public Text comboTimer;
    public Text comboText;
    public float time = 30f;
    public float comboTime = 3f;
    public int comboCount = 0;
    public GameObject[] hearts;
    public int coinCount = 0;
    public Text coinText;
    public bool isCombo= false;
    public GameObject helpUI;
    public Text GetCoinTextResult;
    public bool isWin = false;

    public static UIManager_Block Instance { get; private set; }

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

    public void AddCoin()
    {
        coinCount += comboCount * 10;
        coinText.text = coinCount.ToString();
    }
    public void TakeHeart()
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
    public void UpdateTimer()
    {
        time -= Time.deltaTime;
        timer.text = time.ToString("F2");
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
        GetCoinText.text = "Get Half Coin: " + (coinCount / 2).ToString();
    }
    public void StartComboTimer()
    { 
        if(comboTime <= 0f)
        {
            comboTime = 5f;
            comboCount = 1;
            comboText.text = $"{comboCount} Combo!";
            comboTimer.text = comboTime.ToString("F2");
            comboUI.SetActive(true);
            isCombo = true;
        }
        else
        {
            comboTime = 5f;
            comboCount++;
            comboText.text = $"{comboCount} Combo!";
            comboTimer.text = comboTime.ToString("F2");
            comboUI.SetActive(true);
            isCombo = true;
        }
        AddCoin();
    }
    public void ClearCombo()
    {
        isCombo = false;
        comboCount = 0;
        comboText.text = $" ";
        comboTimer.text = "0.00";
        comboUI.SetActive(false);
    }
    public void ComboTimer()
    {
        if (isCombo)
        {
            comboTime -= Time.deltaTime;
            comboTimer.text = comboTime.ToString("F2");
            if (comboTime <= 0f)
            {
                isCombo = false;
                comboCount = 0;
                comboText.text = $" ";
                comboTimer.text = "0.00";
                comboUI.SetActive(false);
            }
        }
    }
    public void UpdateHelp()
    {
        helpUI.SetActive(true);
    }

    public void ShowClearText_Block()
    {
        resultUI.SetActive(true);
        ResultText.text = "Stage Clear!";
        GetCoinTextResult.text = "Get Coin: " + coinCount.ToString();
    }

    public void ShowfailText_Block()
    {
        resultUI.SetActive(true);
        ResultText.text = "Stage fail!";
        GetCoinTextResult.text = "Get Half Coin: " + (coinCount).ToString();
    }
    public void DeadCost()
    {
        coinCount /= 2;
    }
}
