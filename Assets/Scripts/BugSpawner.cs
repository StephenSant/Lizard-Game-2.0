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
    }

    public void isSpawning(bool isSpawning)
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
    }
}
