using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public Transform bugSpawnerParent;
    public Transform[] bugSpawners;
    /// <summary>
    /// Bugs spawn per second.
    /// </summary>
    public float spawnRate;
    public GameObject bugPrefab;

    void Awake()
    {
        for (int i = 0; i < bugSpawners.Length; i++)
        {
            bugSpawners[i] = bugSpawnerParent.GetChild(i);
        }
        PlaceSpawners();

    }

    public void PlaceSpawners()
    {
        bugSpawners[0].position = new Vector3(Random.Range(-8,9),-10,0);
        bugSpawners[0].rotation = Quaternion.Euler(Vector3.forward * 0);
        bugSpawners[1].position = new Vector3(-10, Random.Range(-8, 9), 0);
        bugSpawners[1].rotation = Quaternion.Euler(Vector3.forward * -90);
        bugSpawners[2].position = new Vector3(Random.Range(-8, 9), 10, 0);
        bugSpawners[2].rotation = Quaternion.Euler(Vector3.forward * 180);
        bugSpawners[3].position = new Vector3(10, Random.Range(-8, 9), 0);
        bugSpawners[3].rotation = Quaternion.Euler(Vector3.forward * 90);
    }

    public void IsSpawning(bool isSpawning)
    {
        if (isSpawning)
        {
            StartCoroutine(SpawnBugs());
        }
        else
        {
            StopCoroutine(SpawnBugs());
        }
    }

    IEnumerator SpawnBugs()
    {
        Transform bugSpawnPoint = bugSpawners[Random.Range(0, bugSpawners.Length)];
        Instantiate(bugPrefab, bugSpawnPoint.position, bugSpawnPoint.rotation);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnBugs());
        PlaceSpawners();
    }
}
