using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [Header("Rooms")]

    [SerializeField]
    private GameObject[] rooms = new GameObject[2];

    [Header("Config")]

    [SerializeField]
    private int roomsToMake = 10;

    private int roomsMade = 0;

    private void Start()
    {
        StartCoroutine(CreateLevel());
    }

    private IEnumerator CreateLevel()
    {
        while (roomsMade < roomsToMake)
        {
            SpawnPoint[] possibleLocations = FindObjectsOfType<SpawnPoint>();
            Transform location = possibleLocations[Mathf.RoundToInt(Random.Range(0f, possibleLocations.Length - 1))].transform;

            SpawnPoint spawnPoint = location.GetComponent<SpawnPoint>();

            if (!spawnPoint.Occupied)
            {
                CreateRoom(spawnPoint, location);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void CreateRoom(SpawnPoint spawnPoint, Transform location)
    {
        spawnPoint.ToggleOccupied(true);

        Quaternion roomRotation;

        // Sets rotation variable based on spawnpoint rotation variable
        switch (spawnPoint.Rotation)
        {
            case 1:
                roomRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                break;

            case 2:
                roomRotation = Quaternion.identity;
                break;

            case 3:
                roomRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                break;

            case 4:
                roomRotation = Quaternion.identity;
                break;

            default:
                Debug.LogError("Spawn point lacks rotation declaration");
                roomRotation = Quaternion.identity;
                break;
        }

        GameObject newRoom = Instantiate(rooms[Mathf.RoundToInt(Random.Range(0f, rooms.Length - 1))], location.position, Quaternion.identity);

        newRoom.transform.GetChild(0).rotation = roomRotation;
        newRoom.transform.GetChild(1).rotation = roomRotation;
    
        roomsMade++;
    }
}
