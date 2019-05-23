using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    
    public GameObject obstical;

    int squareIndex;
    //game size 18 x 10
    Vector2[] spaces = new Vector2[18 * 10];
    void Start()
    {

        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 10; y++)
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
        Instantiate(obstical, gridSpace + new Vector2(-8.5f, -4.5f), transform.rotation);
    }
}
