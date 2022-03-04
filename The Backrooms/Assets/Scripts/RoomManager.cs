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

    [SerializeField]
    private GameObject closedRoom;

    [SerializeField] [Min(10)]
    private int roomsToMake = 10;

    private int roomsLeft;

    private void Awake()
    {
        roomsLeft = roomsToMake;
    }

    public GameObject SelectRoom(int openingType)
    {
        Debug.Log(roomsLeft);

        if (roomsLeft <= 0)
        {
            return null;
        }

        roomsLeft--;

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
}
