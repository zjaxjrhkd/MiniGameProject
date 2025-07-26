using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player_Object;
    public Player player;

    public static GameManager Instance { get; private set; }

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
        Setting();
        DataCheck();
    }

    void Update()
    {
        player.OnMove();
    }
    public void Setting()
    {
        player_Object = GameObject.FindGameObjectWithTag("Player");
        player = player_Object.GetComponent<Player>();
        player.playerGameMode = Player.PlayerGameMode.Map;
        player.rb.gravityScale = 0f;
        player_Object.transform.position = new Vector3(0f, 0f, 0f);
    }
    public void DataCheck()
    {
        if(player.isLoad)
        {
            return;
        }
        UIManager_World.Instance.startUI.SetActive(true);
        UIManager_World.Instance.GameDataCheck();
        player.isLoad = true;
    }
}
