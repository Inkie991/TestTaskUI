using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    public GameObject highlighter;
    [HideInInspector] public bool isActive;
    [HideInInspector] public int id;

    private DragManager _dragManager;
    private Canvas _canvas;
    private RectTransform rect;
    private List<Image> images = new List<Image>();

    public DataUI dataUI;

    public void Startup()
    {
        _dragManager = FindObjectOfType<DragManager>();
        _canvas = FindObjectOfType<Canvas>();
        rect = GetComponent<RectTransform>();
        images.AddRange(GetComponentsInChildren<Image>());
        images.Remove(images[images.Count - 1]);
        highlighter.SetActive(false);
        
        //StartSetings();
    }

    private void StartSetings()
    {
        rect.position = new Vector3(dataUI.posX, dataUI.posY, rect.position.z);
        rect.localScale = new Vector3(dataUI.scaleFactor, dataUI.scaleFactor, rect.localScale.z);
        foreach (var image in images)
        {
            Color color = image.color;
            color.a = dataUI.opacityValue;
            image.color = color;
        }
    }

    public void UpdateSizeAndOpacity(float sizeFactor, float opacityValue)
    {
        rect.localScale = new Vector3(sizeFactor, sizeFactor, rect.localScale.z);
        foreach (var image in images)
        {
            Color color = image.color;
            color.a = opacityValue;
            image.color = color;
        }
        UpdateData();
    }

    private void UpdateData()
    {
        dataUI.id = id;
        dataUI.posX = rect.position.x;
        dataUI.posY = rect.position.y;
        dataUI.scaleFactor = rect.localScale.x;
        dataUI.opacityValue = images[0].color.a;
    }

    public void DragHandler(BaseEventData data)
    {
        if (!isActive) _dragManager.SwitchActiveElement(id);
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, pointerData.position,
            _canvas.worldCamera, out position);

        transform.position = _canvas.transform.TransformPoint(position);
    }
}
