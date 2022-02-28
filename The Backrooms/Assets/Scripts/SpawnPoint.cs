using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private BoxCollider myBoxCollider;

    [Header("Config")]

    [SerializeField]
    private bool occupied = false;
    public bool Occupied { get { return occupied; } }

    [SerializeField] [Range(1, 4)] // 1 = Top; 2 = Right; 3 = Bottom; 4 = Left
    private int rotation = 1;
    public int Rotation { get { return rotation; } }

    private void Awake()
    {
        myBoxCollider = GetComponent<BoxCollider>();
    }

    public void ToggleOccupied(bool state)
    {
        occupied = state;
    }

    public void UpdateRotationInt(int newValue)
    {
        rotation = newValue;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.name)
        {
            case "Top":
                UpdateRotationInt(1);
                return;

            case "Right":
                UpdateRotationInt(2);
                return;

            case "Bottom":
                UpdateRotationInt(3);
                return;

            case "Left":
                UpdateRotationInt(4);
                return;

            default:
                break;
        }

        ToggleOccupied(true);
    }
}
