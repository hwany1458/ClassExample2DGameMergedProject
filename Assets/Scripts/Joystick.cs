using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// Drag, PointerUp, PointerDown 3�� Interface�� ��ӹ޾�, �������̽��� �Լ��� ����
public class Joystick : MonoBehaviour,  IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Image backImg;  // Background �̹���
    Image stick;  // Stick �̹���
    Vector3 input;  // Sitck�� ��ǥ

    // Start is called before the first frame update
    void Start()
    {
        backImg = GetComponent<Image>();
        stick = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Point Down
    public void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    // Point Up
    public void OnPointerUp(PointerEventData data)
    {
        input = Vector3.zero;
        stick.rectTransform.anchoredPosition = input;
    }

    // Drag
    public void OnDrag(PointerEventData data)
    {
        var rect = backImg.rectTransform;
        var camera = data.pressEventCamera;
        var dataPos = data.position;
        
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, dataPos, camera, out pos))
        {
            // pos�� Background�� ũ�⿡ ���� ������ ����
            pos.x = pos.x / backImg.rectTransform.sizeDelta.x * 2;
            pos.y = pos.y / backImg.rectTransform.sizeDelta.y * 2;

            // Vector ����ȭ (-1.0 ~ 1.0���� ����)
            input = new Vector3(pos.x, pos.y, 0);
            input = (input.magnitude > 1) ? input.normalized : input;

            // Stick �̵� ���� ����
            float x = input.x * rect.sizeDelta.x * 0.4f;
            float y = input.y * rect.sizeDelta.y * 0.4f;

            stick.rectTransform.anchoredPosition = new Vector3(x, y, 0);
        }
    }

    // Horizontal value
    public float Horizontal()
    {
        return input.x;
    }

    // Vertical value
    public float Vertical()
    {
        return input.y;
    }
}
