using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject go;

    public AI_Base SpawnEnemies()
    {
        go = (GameObject)Instantiate(Resources.Load(CS_Utility.EnemyDirectory[0]));
        //go.transform.position = transform.position;
        go.name = "AI " + gameObject.name;

        AI_Base enemy = go.GetComponent<AI_Base>();
        go.GetComponent<AI_Base>().spawner = transform.position;

        return enemy;
    }
}
