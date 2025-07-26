using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player_Object;

    private void Start()
    {
        player_Object = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        Vector3 playerPosition = player_Object.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }
}
