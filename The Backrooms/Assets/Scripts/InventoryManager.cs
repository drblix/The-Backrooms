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
    private Transform itemDropArea;

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

    [Header("Sounds")]

    private AudioSource audioSrc;

    [SerializeField]
    private AudioClip pickUp;
    [SerializeField]
    private AudioClip drinking;
    [SerializeField]
    private AudioClip wrapper;

    //// ^^^ Variables ^^^ ////

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();

        slots[0].GetComponent<Image>().color = selectionColor;
        slots[0].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        slots[1].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        slots[2].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
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
                GameObject prevParent;

                try
                {
                    prevParent = itemInfo.transform.parent.gameObject;
                }
                catch (System.Exception)
                {
                    prevParent = null;
                }

                inventory[i] = itemInfo.ObjName;
                itemInfo.gameObject.SetActive(false);
                itemInfo.transform.parent = slots[i].Find("ItemHolder");

                if (prevParent != null) { Destroy(prevParent); }

                slots[i].Find("Icon").GetComponent<Image>().sprite = itemInfo.ObjIcon;
                slots[i].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);

                slotOccupied[i] = true;

                audioSrc.clip = pickUp;
                audioSrc.Play();
                return;
            }
        }

        Debug.Log("Inventory full");
    }

    private void UseItem()
    {
        int slotNum = RetrieveCurrentIndex();
        Debug.Log(currentSlot);

        if (!slotOccupied[slotNum]) { Debug.LogWarning("No item in slot"); return; }

        switch (inventory[slotNum])
        {
            case "Soda":
                Debug.Log("Use Soda");
                PlayerMovement plrMovement = FindObjectOfType<PlayerMovement>();
                plrMovement.StartCoroutine(plrMovement.StaminaPowerup(4f));
                RemoveItem(slotNum, false);

                audioSrc.clip = drinking;
                audioSrc.Play();
                break;

            case "Candy Bar":
                Debug.Log("Use Candy Bar");
                // Candy bar function goes here
                RemoveItem(slotNum, false);

                audioSrc.clip = wrapper;
                audioSrc.Play();
                break;
        }
    }

    private void RemoveItem(int index, bool fromDropItem)
    {
        inventory[index] = "null";
        slots[index].Find("Icon").GetComponent<Image>().sprite = null;
        slots[index].Find("Icon").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

        if (!fromDropItem)
        {
            Destroy(slots[index].Find("ItemHolder").GetChild(0).gameObject);
        }

        slotOccupied[index] = false;
    }

    private void DropItem()
    {
        int index = RetrieveCurrentIndex();

        if (!slotOccupied[index]) { return; }

        RemoveItem(index, true);

        GameObject item = slots[index].Find("ItemHolder").GetChild(0).gameObject;
        item.transform.parent = null;
        item.transform.position = itemDropArea.position;
        item.SetActive(true);
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

    private int RetrieveCurrentIndex()
    {
        switch (currentSlot)
        {
            case Slots.Slot01:
                return 0;

            case Slots.Slot02:
                return 1;

            case Slots.Slot03:
                return 2;

            default:
                Debug.LogError("No variable matching");
                return 0;
        }
    }
}
