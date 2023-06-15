using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Scaler : MonoBehaviour
{
    private const float StandartSize = 1920f;
    // Start is called before the first frame update
    void Start()
    {
        Rect canvasSize = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect;
        if (canvasSize.width > canvasSize.height)
        {
            float value = canvasSize.width / StandartSize;
            transform.localScale = new Vector3(transform.localScale.x * value, transform.localScale.y * value, transform.localScale.z);
        }
        else
        {
            float value = canvasSize.height / StandartSize;
            transform.localScale = new Vector3(transform.localScale.x * value, transform.localScale.y * value, transform.localScale.z);
        }
    }
}
