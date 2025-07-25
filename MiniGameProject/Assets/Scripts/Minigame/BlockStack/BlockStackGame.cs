using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockStackGame : MonoBehaviour
{
    private Player player;
    public GameObject player_Object;
    public GameObject baseBlock;
    private float speed = 6f;
    private float xMin = -9f;
    private float xMax = 9f;
    private float y = -10f;
    public int stageNum = 1;
    public enum GameState
    {
        Stop,
        Ready,
        Playing,
        End
    }
    GameState gameState = GameState.Stop;

    private BlockCreater blockCreater;

    void Start()
    {
        SetGame();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Stop:
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
    public void SetGame()
    {
        blockCreater = gameObject.GetComponent<BlockCreater>();
        player = player_Object.GetComponent<Player>();
        gameState = GameState.Ready;
    }
    public void StartGame()
    {

        player.OnBlockStackGame();
        UIManager_Block.Instance.stageUI.SetActive(true);

    }
    public void OnHelpButton()
    {
        BlockCreater.Instance.BlockDownCheck();
        UIManager_Block.Instance.helpUI.SetActive(false);
    }
    public void MoveBaseBlock()
    {
        float center = (xMin + xMax) / 2f;
        float halfDistance = (xMax - xMin) / 2f;
        float x = center + Mathf.PingPong(Time.time * speed, (xMax - xMin)) - halfDistance;
        baseBlock.transform.position = new Vector3(x, y, transform.position.z);
    }
    
    public void PlayLoop()
    {
        blockCreater.CreateBlock();
        UIManager_Block.Instance.UpdateTimer();
        UIManager_Block.Instance.ComboTimer();
        CheckDead();
    }
    public void CheckDead()
    {
        if (UIManager_Block.Instance.time <= 0)
        {
            gameState = GameState.End;
            UIManager_Block.Instance.ShowfailText_Block();

        }
        else if (!UIManager_Block.Instance.hearts[2].activeSelf)
        {
            gameState = GameState.End;
            UIManager_Block.Instance.ShowfailText_Block();

        }
        else if(UIManager_Block.Instance.isWin)
        {
            gameState = GameState.End;
            UIManager_Block.Instance.ShowClearText_Block();
        }
    }

    public void OnBUttonStage1()
    {
        stageNum = 1;
        gameState = GameState.Playing;
        UIManager_Block.Instance.stageUI.SetActive(false);
        UIManager_Block.Instance.inGameUI.SetActive(true);
        baseBlock.transform.localScale = new Vector3(10f, 1f, 1f);

    }
    public void OnBUttonStage2()
    {
        stageNum = 2;
        gameState = GameState.Playing;
        UIManager_Block.Instance.stageUI.SetActive(false);
        UIManager_Block.Instance.inGameUI.SetActive(true);
        baseBlock.transform.localScale = new Vector3(8f, 1f, 1f);
    }
    public void OnBUttonStage3()
    {
        stageNum = 3;
        gameState = GameState.Playing;
        UIManager_Block.Instance.stageUI.SetActive(false);
        UIManager_Block.Instance.inGameUI.SetActive(true);
        baseBlock.transform.localScale = new Vector3(6f, 1f, 1f);

    }
    public void OnBUttonStage4()
    {
        stageNum = 4;
        gameState = GameState.Playing;
        UIManager_Block.Instance.stageUI.SetActive(false);
        UIManager_Block.Instance.inGameUI.SetActive(true);
        baseBlock.transform.localScale = new Vector3(4f, 1f, 1f);

    }
    public void OnBUttonStage5()
    {
        stageNum = 5;
        gameState = GameState.Playing;
        UIManager_Block.Instance.stageUI.SetActive(false);
        UIManager_Block.Instance.inGameUI.SetActive(true);
        baseBlock.transform.localScale = new Vector3(2f, 1f, 1f);

    }

    public void OnBackButton()
    {
        player.playerGameMode = Player.PlayerGameMode.Map;
        SceneManager.LoadScene("1.MainScene");
    }
    public void OnStageSelectButton()
    {
        UIManager_Block.Instance.stageUI.SetActive(true);
        UIManager_Block.Instance.inGameUI.SetActive(false);
        gameState = GameState.Stop;
    }
}
