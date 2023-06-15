using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value + "%";
    }
}
