using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Rooms that possess openings on the top")]
    private GameObject[] topRooms; // For opening type 1
    [SerializeField] [Tooltip("Rooms that possess openings on the right")]
    private GameObject[] rightRooms; // For opening type 2
    [SerializeField] [Tooltip("Rooms that possess openings on the bottom")]
    private GameObject[] bottomRooms; // For opening type 3
    [SerializeField] [Tooltip("Rooms that possess openings on the left")]
    private GameObject[] leftRooms; // For opening type 4


    public GameObject SelectRoom(int openingType)
    {
        switch (openingType)
        {
            case 1:
                int numT = Random.Range(0, topRooms.Length - 1);
                return topRooms[numT];

            case 2:
                int numR = Random.Range(0, rightRooms.Length - 1);
                return topRooms[numR];

            case 3:
                int numB = Random.Range(0, bottomRooms.Length - 1);
                return topRooms[numB];

            case 4:
                int numL = Random.Range(0, leftRooms.Length - 1);
                return topRooms[numL];

            default:
                Debug.LogError("Opening type out of range");
                return null;
        }
    }
}
