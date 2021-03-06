using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private bool occupied = false;
    public bool Occupied { get { return occupied; } }

    [SerializeField] [Range(1, 4)] [Tooltip("1 = Top opening; 2 = Right opening; 3 = Bottom opening; 4 = Left opening")]
    private int doorOpening;
    // 1 = Opening on top; 2 = Opening on right; 3 = Opening on bottom; 4 = Opening on left

    private readonly float waitTime = 0.25f; // Lowest possible wait time is 0.25f; any lower time and collisions have no time to register

    private RoomManager roomManager;

    private Transform roomsMasterParent;
    private Transform closedRoomsParent;

    private void Awake()
    {
        roomManager = FindObjectOfType<RoomManager>();
        roomsMasterParent = GameObject.FindGameObjectWithTag("MasterRoomParent").transform;
        closedRoomsParent = GameObject.FindGameObjectWithTag("ClosedRoomParent").transform;
    }

    private void Start()
    {
        StartCoroutine(CreateRoom());
    }

    private IEnumerator CreateRoom()
    {
        yield return new WaitForSeconds(waitTime);
        if (!occupied)
        {
            GameObject room = roomManager.SelectRoom(doorOpening);

            if (room == null) { yield break; }

            Instantiate(room, transform.position, room.transform.rotation, roomsMasterParent);

            SetOccupiedState(true);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            yield return new WaitForSeconds(0.1f); // Gives time to check collisions

            if (!other.GetComponent<SpawnPoint>().Occupied && !Occupied)
            {
                GameObject room = roomManager.RequestClosedRoom();

                Instantiate(room, transform.position, room.transform.rotation, closedRoomsParent);
                Destroy(gameObject);
            }

            SetOccupiedState(true);
        }
    }

    private void SetOccupiedState(bool state)
    {
        occupied = state;
    }

}
