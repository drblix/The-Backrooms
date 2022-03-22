using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{
    private UIManager uiManager;

    [SerializeField] [Tooltip("Rooms that possess openings on the bottom")]
    private GameObject[] topRooms; // For opening type 1
    [SerializeField] [Tooltip("Rooms that possess openings on the left")]
    private GameObject[] rightRooms; // For opening type 2
    [SerializeField] [Tooltip("Rooms that possess openings on the top")]
    private GameObject[] bottomRooms; // For opening type 3
    [SerializeField] [Tooltip("Rooms that possess openings on the right")]
    private GameObject[] leftRooms; // For opening type 4

    [SerializeField]
    private GameObject closedRoom;

    [SerializeField] [Min(250)]
    private int roomsToMake;

    private int roomsLeft;

    private bool roomsDoneLoading = false;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        roomsToMake = MainMenu.enteredRoomsToGenerate;
        roomsLeft = roomsToMake;
    }

    public GameObject SelectRoom(int openingType)
    {
        if (roomsDoneLoading) { return null; }

        if (roomsLeft <= 0)
        {
            roomsDoneLoading = true;
            RoomsFinished();
            return null;
        }

        roomsLeft--;

        uiManager.UpdateLoadingBar(roomsToMake - roomsLeft, roomsToMake);

        switch (openingType)
        {
            case 1:
                int numT = Random.Range(0, topRooms.Length - 1);
                return topRooms[numT];

            case 2:
                int numR = Random.Range(0, rightRooms.Length - 1);
                return rightRooms[numR];

            case 3:
                int numB = Random.Range(0, bottomRooms.Length - 1);
                return bottomRooms[numB];

            case 4:
                int numL = Random.Range(0, leftRooms.Length - 1);
                return leftRooms[numL];

            default:
                Debug.LogError("Opening type out of range");
                return null;
        }
    }

    public GameObject RequestClosedRoom()
    {
        return closedRoom;
    }

    private void RoomsFinished()
    {
        Debug.Log("Rooms have finished loading!");

        uiManager.FinishedLoading();
    }
}
