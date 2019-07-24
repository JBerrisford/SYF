using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    public void Init()
    {
        player = Player_Manager.Instance.GetPlayerGO();
        offset = new Vector3(0.0f, 6.0f, -3.5f);
    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }

}
