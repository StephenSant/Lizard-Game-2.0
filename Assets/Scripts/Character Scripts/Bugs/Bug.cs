using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bug : MonoBehaviour
{
    public int pointsToGive = 1;
    public void Update()
    {
        #region Destory Off Screen
        if (transform.position.x > 12 || transform.position.y > 12 || transform.position.x < -12 || transform.position.y < -12)
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
