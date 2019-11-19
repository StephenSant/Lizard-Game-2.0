using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    GameManager gm;
    public Transform bugSpawnerParent;
    public Transform[] bugSpawners;
    public Bugs[] bugs;
    public int bugEntryIndex = 0;

    void Awake()
    {
        for (int i = 0; i < bugSpawners.Length; i++)
        {
            bugSpawners[i] = bugSpawnerParent.GetChild(i);
        }
        PlaceSpawners();

    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    void Update()
    {
        if (bugEntryIndex == 0)
        {
            StartCoroutine(SpawnBug(0));
            bugEntryIndex++;
        }
        if (gm.score >= bugs[1].entryScore && bugEntryIndex < 2)
        {
            StartCoroutine(SpawnBug(1));
            bugEntryIndex++;
        }
        if (gm.score >= bugs[2].entryScore && bugEntryIndex < 3)
        {
            StartCoroutine(SpawnBug(2));
            bugEntryIndex++;
        }

    }

    public void PlaceSpawners()
    {
        bugSpawners[0].position = new Vector3(Random.Range(-7, 8), -10, 0);
        bugSpawners[0].rotation = Quaternion.Euler(Vector3.forward * 0);
        bugSpawners[1].position = new Vector3(-10, Random.Range(-7, 8), 0);
        bugSpawners[1].rotation = Quaternion.Euler(Vector3.forward * -90);
        bugSpawners[2].position = new Vector3(Random.Range(-7, 8), 10, 0);
        bugSpawners[2].rotation = Quaternion.Euler(Vector3.forward * 180);
        bugSpawners[3].position = new Vector3(10, Random.Range(-7, 8), 0);
        bugSpawners[3].rotation = Quaternion.Euler(Vector3.forward * 90);
    }

    IEnumerator SpawnBug(int bugIndex)
    {
        Transform bugSpawnPoint = bugSpawners[Random.Range(0, bugSpawners.Length)];
        Instantiate(bugs[bugIndex].bugPrefab, bugSpawnPoint.position, bugSpawnPoint.rotation);
        yield return new WaitForSeconds(bugs[bugIndex].spawnRate);
        StartCoroutine(SpawnBug(bugIndex));
        PlaceSpawners();
    }

}
[System.Serializable]
public struct Bugs
{
    public GameObject bugPrefab;
    public float spawnRate;
    public int entryScore;
}
