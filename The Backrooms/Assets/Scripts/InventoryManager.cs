using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Slot References")]

    [SerializeField]
    private Transform slot1;
    [SerializeField]
    private Transform slot2;
    [SerializeField]
    private Transform slot3;

    private Image slot1Icon;
    private Image slot2Icon;
    private Image slot3Icon;

    [SerializeField]
    private Color selectionColor;
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private List<Item> inventory = new List<Item>();

    private void Awake()
    {
        slot1Icon = slot1.GetChild(0).GetComponent<Image>();
        slot2Icon = slot2.GetChild(0).GetComponent<Image>();
        slot3Icon = slot3.GetChild(0).GetComponent<Image>();

        slot1.GetComponent<Image>().color = selectionColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateSelection("Slot1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateSelection("Slot2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateSelection("Slot3");
        }
    }

    private void UpdateSelection(string slot)
    {  
        switch (slot)
        {
            case "Slot1":
                slot1.GetComponent<Image>().color = selectionColor;
                slot2.GetComponent<Image>().color = defaultColor;
                slot3.GetComponent<Image>().color = defaultColor;
                break;

            case "Slot2":
                slot1.GetComponent<Image>().color = defaultColor;
                slot2.GetComponent<Image>().color = selectionColor;
                slot3.GetComponent<Image>().color = defaultColor;
                break;

            case "Slot3":
                slot1.GetComponent<Image>().color = defaultColor;
                slot2.GetComponent<Image>().color = defaultColor;
                slot3.GetComponent<Image>().color = selectionColor;
                break;
        }
    }
}
