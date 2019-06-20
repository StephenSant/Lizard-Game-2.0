using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input = Vector2.zero;

    public float handlerRange = 1;
    public float deadZone = 0;
    public float moveThreshold = 1;
    public RectTransform background = null;
    public RectTransform handle = null;

    #region References
    private RectTransform baseRect = null;
    private Canvas canvas;
    private Camera cam;
    #endregion

    public float MoveThreshold
    {
        get { return moveThreshold; }
        set { moveThreshold = Mathf.Abs(value); }
    }

    public float HandleRange
    {
        get { return handlerRange; }
        set { handlerRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    void Start()
    {
        HandleRange = handlerRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("The Joystcik is not placed inside the canvas");
        }
        //Offset to the center
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.anchoredPosition = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            return localPoint - (background.anchorMax * baseRect.sizeDelta);
        }
        return Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized);
        handle.anchoredPosition = input * radius * handlerRange;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    void HandleInput(float magnitude, Vector2 normalised)
    {
        if (magnitude > deadZone)
        {
            input = magnitude > 1 ? normalised : Vector2.zero;
        }
    }
}
