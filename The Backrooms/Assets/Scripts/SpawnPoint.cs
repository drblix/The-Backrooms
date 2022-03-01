using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Config")]

    [SerializeField]
    private bool occupied = false;
    public bool Occupied { get { return occupied; } }

    [SerializeField] [Range(1, 4)] // 1 = Top; 2 = Right; 3 = Bottom; 4 = Left
    private int rotation = 1;
    public int Rotation { get { return rotation; } }

    private int colliders; // Fixed issue with the 'occupied' variable being modified after room was rotated during instantiation

    private void Awake()
    {
        ToggleOccupied(false);

        colliders = 0;
    }

    private void Update()
    {
        if (colliders <= 0)
        {
            ToggleOccupied(false);
        }
    }

    public void ToggleOccupied(bool state)
    {
        occupied = state;
    }

    public void UpdateRotationInt(int newValue)
    {
        rotation = newValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.CompareTag("RoomFloor"))
        {
            ToggleOccupied(true);
            colliders++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RoomFloor"))
        {
            ToggleOccupied(false);
            colliders--;
        }
    }
}
