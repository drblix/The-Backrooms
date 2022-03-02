using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private bool occupied = false;
    public bool Occupied { get { return occupied; } }

    [SerializeField] [Range(1, 4)] [Tooltip("1 = Top opening; 2 = Right opening; 3 = Bottom opening; 4 = Left opening")]
    private int doorOpening;
    // 1 = Opening on top; 2 = Opening on right; 3 = Opening on bottom; 4 = Opening on left

    private float waitTime = 1f;

    private RoomManager roomManager;

    private Transform roomsMasterParent;

    private void Awake()
    {
        roomManager = FindObjectOfType<RoomManager>();
        roomsMasterParent = GameObject.FindGameObjectWithTag("MasterRoomParent").transform;
    }

    private void Start()
    {
        Invoke(nameof(CreateRoom), waitTime);
    }

    private void CreateRoom()
    {
        if (!occupied)
        {
            GameObject room = roomManager.SelectRoom(doorOpening);

            Instantiate(room, transform.position, room.transform.rotation, roomsMasterParent);
        }
        else
        {
            Debug.Log("Space occupied");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            SetOccupiedState(true);
        }
    }

    private void SetOccupiedState(bool state)
    {
        occupied = state;
    }
}
