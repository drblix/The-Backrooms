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


    private void Start()
    {
        StartCoroutine(CreateLevel());
    }

    private IEnumerator CreateLevel()
    {
        int roomsMade = 0;

        Instantiate(rooms[Mathf.RoundToInt(Random.Range(0f, rooms.Length - 1))], Vector3.zero, Quaternion.identity);

        
        while (roomsMade < roomsToMake)
        {
            SpawnPoint[] possibleLocations = FindObjectsOfType<SpawnPoint>();
            Transform location = possibleLocations[Mathf.RoundToInt(Random.Range(0f, possibleLocations.Length - 1))].transform;

            SpawnPoint spawnPoint = location.GetComponent<SpawnPoint>();

            if (!spawnPoint.Occupied)
            {
                spawnPoint.ToggleOccupied(true);
                Instantiate(rooms[Mathf.RoundToInt(Random.Range(0f, rooms.Length - 1))], location.position, Quaternion.identity);
                roomsMade++;
            }
            else
            {
                Debug.LogWarning("Space occupied!");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
