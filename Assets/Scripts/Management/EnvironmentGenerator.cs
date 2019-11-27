using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{

    public GameObject[] obsticalsPrefabs;

    public void PlaceObsticals()
    {

        for (int x = 0; x < 17; x += 3)
        {
            for (int y = 0; y < 17; y += 3)
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    if (x != 8 && y != 8)
                    {
                        CreateObject(new Vector2(x, y));
                    }
                }
            }
        }
    }

    void CreateObject(Vector2 gridSpace)
    {
        Instantiate(obsticalsPrefabs[Random.Range(0, obsticalsPrefabs.Length - 1)], new Vector3(gridSpace.x + -8f, gridSpace.y + -8f, -1f), transform.rotation, transform);
    }
}
