using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player_Object;
    public Player player;

    void Start()
    {
        // Player GameObject에 붙어있는 Player 컴포넌트를 가져옵니다.
        player = player_Object.GetComponent<Player>();
    }

    void Update()
    {
        player.OnMove();
    }
}
