using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player_Object;
    public Player player;

    void Start()
    {
        // Player GameObject�� �پ��ִ� Player ������Ʈ�� �����ɴϴ�.
        player = player_Object.GetComponent<Player>();
    }

    void Update()
    {
        player.OnMove();
    }
}
