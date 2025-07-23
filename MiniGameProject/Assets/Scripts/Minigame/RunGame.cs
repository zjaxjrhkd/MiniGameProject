using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
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
    public GameObject playerPrefab;
    public GameObject BackGround;
    public BgCreate bGCreate;
    public GameObject StageUI;
    public Text timer;
    public Player player;
    public GameObject player_Object;

    private float time = 30f;
    // Start is called before the first frame update
    void Start()
    {
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
                break;
            case GameState.Playing:
                PlayLoop();
                break;
            case GameState.End:
                break;
        }
    }
    public void StartGame()
    {
        gameState = GameState.Ready;
        bGCreate = BackGround.GetComponent<BgCreate>();
        player = player_Object.GetComponent<Player>();
        player.OnRunGame();
        StageUI.SetActive(true);
    }

    public void PlayLoop()
    {
        bGCreate.MapLoop();
        UpdateTimer();
        player.OnMove();
    }
    public void UpdateTimer()
    {
        time -= Time.deltaTime;
        timer.text = time.ToString("F2");
        if (time <= 0)
        {
            gameState = GameState.End;
        }
    }
    public void OnBUttonStage1()
    {
        gameState = GameState.Playing;
        StageUI.SetActive(false);

    }
    public void OnBUttonStage2()
    {
        gameState = GameState.Playing;
        StageUI.SetActive(false);

    }
    public void OnBUttonStage3()
    {
        gameState = GameState.Playing;
        StageUI.SetActive(false);

    }
    public void OnBUttonStage4()
    {
        gameState = GameState.Playing;
        StageUI.SetActive(false);

    }
    public void OnBUttonStage5()
    {
        gameState = GameState.Playing;
        StageUI.SetActive(false);

    }
}
