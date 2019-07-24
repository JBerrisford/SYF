using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Analouge_Stick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image backgroundImage;
    public Image joystick;

    private Vector3 input = Vector3.zero;

    public void OnPointerUp(PointerEventData pData)
    {
        input = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
        Player_Manager.Instance.GetPlayer().Movement(input.x, input.z);
    }

    public void OnPointerDown(PointerEventData pData)
    {
        OnDrag(pData);
    }

    public void OnDrag(PointerEventData pData)
    {
        Vector2 pos = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, pData.position, pData.enterEventCamera, out pos))
        {
            pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.y);

            float x = (backgroundImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (backgroundImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            input = new Vector3(x, 0, y);
            input = (input.magnitude > 1.0f) ? input.normalized : input;

            joystick.rectTransform.anchoredPosition = new Vector3(input.x * (backgroundImage.rectTransform.sizeDelta.x / 3.0f), input.z * (backgroundImage.rectTransform.sizeDelta.y / 3.0f));

            Player_Manager.Instance.GetPlayer().Movement(input.x, input.z);
        }
    }
}
