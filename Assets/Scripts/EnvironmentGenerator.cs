using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    
    public GameObject[] rockPrefabs;

    int squareIndex;
    //game size 18 x 18
    Vector2[] spaces = new Vector2[17 * 17];
    public void PlaceObsticals()
    {

        for (int x = 0; x < 17; x+=3)
        {
            for (int y = 0; y < 17; y+=3)
            {
                int r = Random.Range(0, 2);
                //Debug.Log(r);
                if (r == 0)
                {
                    if (x != 8 && y != 8)
                    {
                        CreateObject(new Vector2(x, y));
                        spaces[squareIndex] = new Vector2(x, y);
                    }
                }
                squareIndex++;
            }
        }
    }

    void CreateObject(Vector2 gridSpace)
    {
        Instantiate(rockPrefabs[Random.Range(0,4)], gridSpace + new Vector2(-8f, -8f), transform.rotation,transform);
    }
}
