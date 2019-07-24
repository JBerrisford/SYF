using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour
{
    public DoorHinge PosDoor;
    public DoorHinge NegDoor;

    public DoorCollision buddy;

    public Collider myCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(myCollider == null && other.gameObject.GetComponent<Character_Base>() != null)
        {
            myCollider = other;

            if(buddy.myCollider == null)
            {
                PosDoor.OpenCoroutine(true, true);
                NegDoor.OpenCoroutine(true, false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myCollider != null && other.gameObject.tag == "Player")
        {
            myCollider = null;

            if (buddy.myCollider == null)
            {
                PosDoor.OpenCoroutine(false, true);
                NegDoor.OpenCoroutine(false, false);
            }
        }
    }
}
