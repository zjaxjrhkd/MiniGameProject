using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RunGame : MonoBehaviour
{
    public enum GameState
    {
        Stop,
        Ready,
        Playing,
        End
    }
    GameState gameState = GameState.Stop;

    private BgCreate bgCreate;
    public GameObject bg_Object;
    private Player player;
    public GameObject player_Object;
    private ItemCreate itemCreate;
    private int stageNum = 0;
    private int TargetGold = 300; 

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Stop:
                SetGame();
                break;
            case GameState.Ready:
                StartGame();
                break;
            case GameState.Playing:
                PlayLoop();
                break;
            case GameState.End:
                break;
        }
    }
    public void InitGame()
    {
        gameState = GameState.Ready;
        bgCreate = bg_Object.GetComponent<BgCreate>();
        player = player_Object.GetComponent<Player>();
        itemCreate = GetComponent<ItemCreate>();
    }
    public void SetGame()
    {
        UIManager_Run.Instance.ResetTimer();
        itemCreate.ClearPattern();
        gameState = GameState.Ready;
    }
    
    public void StartGame()
    {
        player.OnRunGame();
        UIManager_Run.Instance.stageUI.SetActive(true);
        UIManager_Run.Instance.resultUI.SetActive(false);
        UIManager_Run.Instance.inGameUI.SetActive(false);
    }

    public void PlayLoop()
    {
        bgCreate.MapLoop();
        player.OnMove();
        UIManager_Run.Instance.UpdateTimer();
        itemCreate.CheckAndResetJumpPrefabs();
        CheckResult();
    }
    public void CheckResult()
    {
        if (!UIManager_Run.Instance.hearts[2].activeSelf)
        {
            UIManager_Run.Instance.ShowfailText();
            itemCreate.ResetPatternJumpPositions();
            gameState = GameState.End;
            itemCreate.PatternOffJump();
        }
        else if (UIManager_Run.Instance.time<=0)
        {
            if(UIManager_Run.Instance.coinCount >= TargetGold)
            {
                UIManager_Run.Instance.ShowClearText();
            }
            else
            {
                UIManager_Run.Instance.ShowfailText();
            }
            itemCreate.ResetPatternJumpPositions();
            gameState = GameState.End;
            itemCreate.PatternOffJump();
        }
    }

    public void OnBUttonStage1()
    {
        stageNum = 1;
        gameState = GameState.Playing;
        itemCreate.SetPattern(stageNum);
        UIManager_Run.Instance.stageUI.SetActive(false);
        bgCreate.ChangeAllLayerSprites(stageNum);
        UIManager_Run.Instance.inGameUI.SetActive(true);

    }
    public void OnBUttonStage2()
    {
        stageNum = 2;
        gameState = GameState.Playing;
        itemCreate.SetPattern(stageNum);
        UIManager_Run.Instance.stageUI.SetActive(false);
        bgCreate.ChangeAllLayerSprites(stageNum);
        UIManager_Run.Instance.inGameUI.SetActive(true);
    }
    public void OnBUttonStage3()
    {
        stageNum = 3;
        gameState = GameState.Playing;
        itemCreate.SetPattern(stageNum);
        UIManager_Run.Instance.stageUI.SetActive(false);
        bgCreate.ChangeAllLayerSprites(stageNum);
        UIManager_Run.Instance.inGameUI.SetActive(true);

    }
    public void OnBUttonStage4()
    {
        stageNum = 4;
        gameState = GameState.Playing;
        itemCreate.SetPattern(stageNum);
        UIManager_Run.Instance.stageUI.SetActive(false);
        bgCreate.ChangeAllLayerSprites(stageNum);
        UIManager_Run.Instance.inGameUI.SetActive(true);

    }
    public void OnBUttonStage5()
    {
        stageNum = 5;
        gameState = GameState.Playing;
        itemCreate.SetPattern(stageNum);
        UIManager_Run.Instance.stageUI.SetActive(false);
        bgCreate.ChangeAllLayerSprites(stageNum);
        UIManager_Run.Instance.inGameUI.SetActive(true);

    }

    public void OnBackButton()
    {
        player.playerGameMode = Player.PlayerGameMode.Map;
        SceneManager.LoadScene("1.MainScene");
    }
    public void OnStageSelectButton()
    {
        UIManager_Run.Instance.stageUI.SetActive(true);
        UIManager_Run.Instance.inGameUI.SetActive(false);
        gameState = GameState.Stop;
    }

}
