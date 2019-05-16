using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public int worldLength;
    public int worldHeight;

    int horizontalPos;
    int bendPos;

    public GameObject riverSquare;

    void Start()
    {
        RandomiseRiver();

        SpawnRiver(new Vector2(horizontalPos, bendPos));
    }

    void RandomiseRiver()
    {
        horizontalPos = Random.Range(-(worldLength / 2), worldLength / 2);
        bendPos = Random.Range(-(worldHeight / 2), worldHeight / 2);
    }

    void SpawnRiver(Vector2 posAndBend)
    {
        for (int i = worldHeight/2; i > bendPos-1; i--)
        {
            Instantiate(riverSquare, new Vector2(horizontalPos, i), transform.rotation, transform);
        }
        int bendDir = Random.Range(-1,1);
        for (int i = bendPos; i > (-(worldHeight/2)-1); i--)
        {
            Instantiate(riverSquare, new Vector2(horizontalPos+bendDir, i), transform.rotation, transform);
        }
    }
}
