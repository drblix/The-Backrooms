using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] [Tooltip("Identifer for the object; must confine to existing objects in inventory manager")]
    private string objName;
    public string ObjName { get { return objName; } }

    [SerializeField] [Tooltip("Icon for the object; will be displayed in the inventory icon area when picked up")]
    private Sprite objIcon;
    public Sprite ObjIcon { get { return objIcon; } }
}
