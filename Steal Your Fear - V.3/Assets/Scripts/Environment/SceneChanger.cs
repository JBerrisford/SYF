using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public int SceneIndex;
    public CS_Enum.CHANGE_ZONE ZoneType;

    bool isActive;

    bool HasPlayer = false;
    bool HasEnemy = false;

    public void Awake()
    {
        isActive = true;
        StartCoroutine(LoopCheck());
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            HasPlayer = true;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            HasEnemy = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HasPlayer = false;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            HasEnemy = false;
        }
    }

    public IEnumerator LoopCheck()
    {
        float loopControl = 0.0f;

        while (isActive)
        {
            if (HasPlayer && !HasEnemy)
            {
                loopControl += Time.deltaTime;
            }
            else if (!HasPlayer)
            {
                loopControl = 0.0f;
            }

            if (loopControl >= 5.0f)
            {
                isActive = false;
                Debug.Log("Switching Level ... ");
                Map_Manager.Instance.SetNewZone(ZoneType);
                GameManager.Instance.Overall_State_Change(CS_Enum.OVERALL_STATE.NEW_SCENE, SceneIndex);
            }

            yield return null;
        }

        yield return null;
    }
}
