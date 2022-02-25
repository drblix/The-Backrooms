using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private bool occupied = false;
    public bool Occupied { get { return occupied; } }

    public void ToggleOccupied(bool state)
    {
        occupied = state;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision);
    }
}
