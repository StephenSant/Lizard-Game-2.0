using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    
    public GameObject obstical;

    int squareIndex;
    //game size 18 x 18
    Vector2[] spaces = new Vector2[17 * 17];
    public void PlaceObsticals()
    {

        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                int r = Random.Range(0, 12);
                //Debug.Log(r);
                if (r == 0)
                {
                    CreateObject(new Vector2(x, y));
                    spaces[squareIndex] = new Vector2(x, y);
                }
                squareIndex++;
            }
        }
    }

    void CreateObject(Vector2 gridSpace)
    {
        Instantiate(obstical, gridSpace + new Vector2(-8.5f, -8.5f), transform.rotation,transform);
    }
}
