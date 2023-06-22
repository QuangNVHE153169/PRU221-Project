using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    public Camera cameraObject;
    public float mapWidth;
    public float mapHeight;

    Dictionary<Vector2, GameObject> spawnedMaps = new Dictionary<Vector2, GameObject>();

    public List<GameObject> activeMaps = new List<GameObject>();

    void Start()
    {
        cameraObject = Camera.main;
        mapWidth = mapPrefabs[0].GetComponent<Renderer>().bounds.size.x;
        mapHeight = mapPrefabs[0].GetComponent<Renderer>().bounds.size.y;
        SetupMap();
    }
    public void SetupMap()
    {
        float xPos = cameraObject.transform.position.x;
        float yPos = cameraObject.transform.position.y;
        CheckSpawnMapAtIndex(new Vector2(xPos - mapWidth / 2, yPos + mapHeight / 2), 0);
        CheckSpawnMapAtIndex(new Vector2(xPos + mapWidth / 2, yPos + mapHeight / 2), 1);
        CheckSpawnMapAtIndex(new Vector2(xPos - mapWidth / 2, yPos - mapHeight / 2), 2);
        CheckSpawnMapAtIndex(new Vector2(xPos + mapWidth / 2, yPos - mapHeight / 2), 3);

    }
    void Update()
    {
        float xPos = cameraObject.transform.position.x;
        float yPos = cameraObject.transform.position.y;

        if (xPos < activeMaps[0].transform.position.x)
        {
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x - mapWidth, activeMaps[0].transform.position.y), 0);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x, activeMaps[0].transform.position.y - mapHeight), 2);
            RemoveMap(activeMaps[5]);
            RemoveMap(activeMaps[3]);
            
        }
        else if (xPos > activeMaps[1].transform.position.x)
        {
            RemoveMap(activeMaps[2]);
            RemoveMap(activeMaps[0]);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x + mapWidth, activeMaps[0].transform.position.y), 1);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[1].transform.position.x, activeMaps[1].transform.position.y - mapHeight), 3);
        }
        if (yPos < activeMaps[2].transform.position.y)
        {
            RemoveMap(activeMaps[1]);
            RemoveMap(activeMaps[0]);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x, activeMaps[0].transform.position.y - mapHeight), 2);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[1].transform.position.x, activeMaps[1].transform.position.y - mapHeight), 3);

        }
        else if (yPos > activeMaps[0].transform.position.y)
        {
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x, activeMaps[0].transform.position.y + mapHeight), 0);
            CheckSpawnMapAtIndex(new Vector2(activeMaps[0].transform.position.x + mapWidth, activeMaps[0].transform.position.y), 1);

            RemoveMap(activeMaps[5]);
            RemoveMap(activeMaps[4]);
        }

    }


    void SpawnMap(Vector2 spawnPosition, int index)
    {
        GameObject mapPrefab;
        if (spawnedMaps.ContainsKey(spawnPosition))
        {
            mapPrefab = spawnedMaps[spawnPosition]; 
        }
        else
        {
            mapPrefab = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
            spawnedMaps.Add(spawnPosition, mapPrefab);
        }
        GameObject newMap = Instantiate(mapPrefab, spawnPosition, Quaternion.identity);
        activeMaps.Insert(index, newMap);

    }
    void CheckSpawnMapAtIndex(Vector2 checkPosition, int index)
    {
        foreach (GameObject map in activeMaps)
        {
            if (Vector2.Distance(map.transform.position, checkPosition) < 0.5f)
            {
                return;
            }
        }
        SpawnMap(checkPosition, index);
    }


    void RemoveMap(GameObject map)
    {
        activeMaps.Remove(map);
        Destroy(map);
    }
}

