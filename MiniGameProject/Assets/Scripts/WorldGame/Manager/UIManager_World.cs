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
                StartGameText.text = "장애물 달리기 장소로 이동하시겠습니까?";
                StartGameButton.onClick.AddListener(OnStartGame1);
                break;
            case "Building2":
                StartGameText.text = "벽돌 쌓기 장소로 이동하시겠습니까?";
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
        gameName1.text = "장애물 달리기";
        gameName2.text = "벽돌 쌓기";
        if (playerData.PlayerGameScore == null)
        {
            gameScore1.text = " 기록없음 ";
            gameScore2.text = " 기록없음 ";
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
            checkSave.text = "저장 파일이 이미 존재합니다. Load 버튼으로 불러오거나 New 버튼으로 새로 시작합니다.";
        }
        else
        {
            checkSave.text = "저장 파일이 없습니다. New 버튼으로 새로 시작합니다.";
        }

        Debug.Log($"저장 경로 확인: {savePath}");
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
