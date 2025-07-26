using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_World : MonoBehaviour
{
    public GameObject StartGameUI;
    public Text StartGameText;
    public Button StartGameButton;


    public GameObject readerBoardUI;
    public Text gameName1;
    public Text gameName2;
    public Text gameScore1;
    public Text gameScore2;

    PlayerData playerData;

    public GameObject startUI;
    public Text checkSave;
    public Button LoadButton;
    public Button SaveButton;

    public GameObject ShopUI;



    public static UIManager_World Instance { get; private set; }
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
    void Start()
    {
        StartGameUI.SetActive(false);
        readerBoardUI.SetActive(false);
        playerData = GameManager.Instance.player_Object.GetComponent<PlayerData>();
        SetBoard();
    }

    public void OnStartGameUI(string buildingName)
    {
        StartGameUI.SetActive(!StartGameUI.activeSelf);
        switch (buildingName)
        {
            case "Building1":
                StartGameText.text = "��ֹ� �޸��� ��ҷ� �̵��Ͻðڽ��ϱ�?";
                StartGameButton.onClick.AddListener(OnStartGame1);
                break;
            case "Building2":
                StartGameText.text = "���� �ױ� ��ҷ� �̵��Ͻðڽ��ϱ�?";
                StartGameButton.onClick.AddListener(OnStartGame2);
                break;
        }
    }
    public void OnStartGame1()
    {
        SceneManager.LoadScene("2.RunScene");
    }
    public void OnStartGame2()
    {
        SceneManager.LoadScene("3.StackScene");
    }

    public void SetBoard()
    {
        gameName1.text = "��ֹ� �޸���";
        gameName2.text = "���� �ױ�";
        if (playerData.PlayerGameScore == null)
        {
            gameScore1.text = " ��Ͼ��� ";
            gameScore2.text = " ��Ͼ��� ";
        }
        else
        {
            gameScore1.text = playerData.PlayerGameScore["Run"].ToString();
            gameScore2.text = playerData.PlayerGameScore["BlockStack"].ToString();
        }
    }

    public void OnReaderBoard()
    {
        readerBoardUI.SetActive(!readerBoardUI.activeSelf);
    }

    public void OnOpenShop()
    {
        ShopUI.SetActive(!ShopUI.activeSelf);
    }

    public void GameDataCheck()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        if (File.Exists(savePath))
        {
            checkSave.text = "���� ������ �̹� �����մϴ�. Load ��ư���� �ҷ����ų� New ��ư���� ���� �����մϴ�.";
        }
        else
        {
            checkSave.text = "���� ������ �����ϴ�. New ��ư���� ���� �����մϴ�.";
        }

        Debug.Log($"���� ��� Ȯ��: {savePath}");
    }

    public void OnSaveGame()
    {
        playerData.SavePlayerData();
    }

    public void OnLoadGame()
    {
        playerData.LoadPlayerData();
        startUI.SetActive(false);
    }
    public void OnCloseStartUI()
    {
        startUI.SetActive(false);
    }
}
