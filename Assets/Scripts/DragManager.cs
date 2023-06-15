using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    //private const string FileName = "uiData.json";
    private const string FileName = "defaultData.json";
    private const string DefaultFile = "defaultData.json";
    
    [SerializeField] private Drag[] dragElements;
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private Slider opacitySlider;

    private Drag activeElement;
    private List<DataUI> elementsData;
    
    // Start is called before the first frame update
    void Start()
    {
        elementsData = FileHandler.ReadListFromJSON<DataUI>(FileName);
        if (elementsData.Count < 1) elementsData = FileHandler.ReadListFromJSON<DataUI>(DefaultFile);
        Debug.Log(elementsData);

        for (int i = 0; i < dragElements.Length; i++)
        {
            DataUI correctData = new DataUI();
            foreach (var data in elementsData)
            {
                if (data.id == i) correctData = data;
            }

            dragElements[i].dataUI = correctData;
            dragElements[i].Startup();
            dragElements[i].isActive = false;
            dragElements[i].id = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeElement != null) activeElement.UpdateSizeAndOpacity(scaleSlider.value / 100, opacitySlider.value / 100);
    }

    private void SetSliders()
    {
        scaleSlider.value = activeElement.transform.localScale.x * 100;
        opacitySlider.value = activeElement.GetComponent<Image>().material.color.a * 100;
    }

    public void SwitchActiveElement(int id)
    {
        for (int i = 0; i < dragElements.Length; i++)
        {
            if (i == id)
            {
                dragElements[i].isActive = true;
                activeElement = dragElements[i];
                dragElements[i].highlighter.SetActive(true);
            }
            else
            {
                dragElements[i].isActive = false;
                dragElements[i].highlighter.SetActive(false);
            }
        }
        
        SetSliders();
    }

    public void SaveData()
    {
        foreach (var dragable in dragElements)
        {
            elementsData.Add(dragable.dataUI);
        }
        
        FileHandler.SaveToJSON<DataUI>(elementsData, FileName);
    }

    public void DefaultData()
    {
        elementsData = FileHandler.ReadListFromJSON<DataUI>(FileName);
    }
}
