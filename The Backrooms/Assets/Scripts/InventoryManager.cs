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

    [Header("Colors")]

    [SerializeField]
    private Color selectionColor;
    [SerializeField]
    private Color defaultColor;

    private enum Slots
    {
        Slot1,
        Slot2,
        Slot3
    }

    [Header("Config")]

    [SerializeField]
    private Slots selectedSlot;

    private string[] inventory = new string[3]
    {
        "null",
        "null",
        "null",
    };
    // 1 = Slot 1; 2 = Slot 2; 3 = Slot 3

    private bool slot1Occupied = false;
    private bool slot2Occupied = false;
    private bool slot3Occupied = false;

    private void Awake()
    {
        slot1Icon = slot1.GetChild(0).GetComponent<Image>();
        slot2Icon = slot2.GetChild(0).GetComponent<Image>();
        slot3Icon = slot3.GetChild(0).GetComponent<Image>();

        slot1Icon.color = new Color(255f, 255f, 255f, 0f);
        slot2Icon.color = new Color(255f, 255f, 255f, 0f);
        slot3Icon.color = new Color(255f, 255f, 255f, 0f);

        slot1.GetComponent<Image>().color = selectionColor;
        selectedSlot = Slots.Slot1;

        Debug.Log(inventory.Length);
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
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

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Use item");
        }
    }

    public bool PickupItem(string item)
    {
        Image currentSlotIcon;

        switch (selectedSlot)
        {
            case Slots.Slot1:
                if (slot1Occupied) { return false; }
                currentSlotIcon = slot1Icon;
                slot1Occupied = true;
                break;

            case Slots.Slot2:
                if (slot2Occupied) { return false; }
                currentSlotIcon = slot2Icon;
                slot2Occupied = true;
                break;

            case Slots.Slot3:
                if (slot3Occupied) { return false; }
                currentSlotIcon = slot3Icon;
                slot3Occupied = true;
                break;

            default:
                Debug.LogError("No selected slot");
                return false;
        }

        switch (item)
        {
            case "Soda":
                
                break;
        }
    }

    private void UpdateSelection(string slot)
    {  
        switch (slot)
        {
            case "Slot1":
                selectedSlot = Slots.Slot1;
                slot1.GetComponent<Image>().color = selectionColor;
                slot2.GetComponent<Image>().color = defaultColor;
                slot3.GetComponent<Image>().color = defaultColor;
                break;

            case "Slot2":
                selectedSlot = Slots.Slot2;
                slot1.GetComponent<Image>().color = defaultColor;
                slot2.GetComponent<Image>().color = selectionColor;
                slot3.GetComponent<Image>().color = defaultColor;
                break;

            case "Slot3":
                selectedSlot = Slots.Slot3;
                slot1.GetComponent<Image>().color = defaultColor;
                slot2.GetComponent<Image>().color = defaultColor;
                slot3.GetComponent<Image>().color = selectionColor;
                break;
        }
    }
}
