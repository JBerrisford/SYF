using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    public bool isOpen;

    public bool isLerping;

    public Quaternion defaultDir;

    public Quaternion positive;
    public Quaternion negative;

    public void Start()
    {
        defaultDir = transform.rotation;

        positive = defaultDir;
        negative = defaultDir;

        positive.eulerAngles = transform.eulerAngles - (Vector3.up * 100.0f);
        negative.eulerAngles = transform.eulerAngles - (Vector3.up * -100.0f);
    }

    public void OpenCoroutine(bool pOpen, bool pIsPos)
    {
        //if(pOpen != isOpen)
        //{
            StopAllCoroutines();
            isLerping = false;

            StartCoroutine(Open(pOpen, pIsPos));
        //}
    }

    public IEnumerator Open(bool pOpen, bool pIsPos)
    {
        isLerping = true;
        isOpen = pOpen;

        Quaternion goToRotD1 = (isOpen) ? GetRot(pIsPos) : defaultDir;

        float t = 0.0f;
        float speed = 0.01f;

        // Lerp from the current to the goto
        while (isLerping)
        {
            t += speed;
            transform.rotation = Quaternion.Lerp(transform.rotation, goToRotD1, t);
            yield return new WaitForFixedUpdate();

            if (t >= 0.95f)
            {
                isLerping = false;
            }
        }

        yield break;
    }

    Quaternion GetRot(bool pIsPos)
    {
        return (pIsPos) ? positive : negative;
    }
}
