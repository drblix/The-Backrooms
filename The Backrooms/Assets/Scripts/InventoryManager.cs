using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Colors")]

    [SerializeField]
    private Color selectionColor;
    [SerializeField]
    private Color defaultColor;

    [Header("Config")]

    [SerializeField]
    private Transform[] slots = new Transform[3];

    [SerializeField]
    private string[] inventory = new string[3]
    {
        "null",
        "null",
        "null",
    };
    // 1 = Slot 1; 2 = Slot 2; 3 = Slot 3
    [SerializeField]
    private bool[] slotOccupied = new bool[3]
    {
        false,
        false,
        false,
    };

    private readonly string[] items = new string[]
    {
        "Soda",
        "Candy Bar",
    };

    private enum Slots
    {
        Slot01,
        Slot02,
        Slot03
    }

    private Slots currentSlot = Slots.Slot01;

    //// ^^^ Variables ^^^ ////

    private void Awake()
    {
        slots[0].GetComponent<Image>().color = selectionColor;
        slots[0].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        slots[1].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        slots[2].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

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
            UseItem();
        }
    }

    public void PickupItem(ItemInfo itemInfo)
    {
        bool itemExists = false;

        // Checks if provided item exists
        for (int i = 0; i < items.Length; i++)
        {
            if (itemInfo.ObjName == items[i])
            {
                itemExists = true;
            }
        }

        if (!itemExists) { Debug.LogWarning("Supplied item does not exist"); return; }

        for (int i = 0; i < inventory.Length; i++)
        {
            if (!slotOccupied[i])
            {
                inventory[i] = itemInfo.ObjName;
                itemInfo.gameObject.SetActive(false);
                itemInfo.transform.parent = slots[i].Find("ItemHolder");

                slots[i].Find("Icon").GetComponent<Image>().sprite = itemInfo.ObjIcon;
                slots[i].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);

                slotOccupied[i] = true;
                return;
            }
        }

        Debug.Log("Inventory full");
    }

    private void UseItem()
    {
        int slotNum;
        Debug.Log(currentSlot);

        switch (currentSlot)
        {
            case Slots.Slot01:
                slotNum = 0;
                break;

            case Slots.Slot02:
                slotNum = 1;
                break;

            case Slots.Slot03:
                slotNum = 2;
                break;

            default:
                Debug.LogError("No variable matching");
                return;
        }

        if (!slotOccupied[slotNum]) { Debug.LogWarning("No item in slot"); return; }

        switch (inventory[slotNum])
        {
            case "Soda":
                Debug.Log("Use Soda");
                RemoveItem(slotNum);
                break;

            case "Candy Bar":
                Debug.Log("Use Candy Bar");
                break;
        }
    }

    private void RemoveItem(int index)
    {
        inventory[index] = "null";
        slots[index].Find("Icon").GetComponent<Image>().sprite = null;
        slots[index].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

        Destroy(slots[index].Find("ItemHolder").GetChild(0).gameObject);
        slotOccupied[index] = false;
    }

    private void UpdateSelection(string slot)
    {  
        switch (slot)
        {
            case "Slot1":
                currentSlot = Slots.Slot01;
                slots[0].GetComponent<Image>().color = selectionColor;
                slots[1].GetComponent<Image>().color = defaultColor;
                slots[2].GetComponent<Image>().color = defaultColor;
                break;

            case "Slot2":
                currentSlot = Slots.Slot02;
                slots[0].GetComponent<Image>().color = defaultColor;
                slots[1].GetComponent<Image>().color = selectionColor;
                slots[2].GetComponent<Image>().color = defaultColor;
                break;

            case "Slot3":
                currentSlot = Slots.Slot03;
                slots[0].GetComponent<Image>().color = defaultColor;
                slots[1].GetComponent<Image>().color = defaultColor;
                slots[2].GetComponent<Image>().color = selectionColor;
                break;
        }
    }
}
