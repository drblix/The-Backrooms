using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomOptimizer : MonoBehaviour
{
    private BoxCollider boxCollider;
    private GameObject meshParent;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(2f, 2f, 2f);

        meshParent = transform.GetChild(0).gameObject;
        ToggleVisibility(false);
    }

    public void ToggleVisibility(bool state)
    {
        meshParent.SetActive(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Optimizer"))
        {
            ToggleVisibility(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Optimizer"))
        {
            ToggleVisibility(false);
            Debug.Log("Disable");
        }
    }
}
