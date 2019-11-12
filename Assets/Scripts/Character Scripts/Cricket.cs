using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cricket : Bug
{
    public int jumpDist;
    public int jumpDistMax;
    public LayerMask obsticalLayer;
    public float waitTime;

    public bool pathClear;

    //public Collider2D collider;

    void CheckAhead()
    {
        pathClear = !Physics2D.OverlapCircle(transform.position + (transform.up*jumpDist),0.5f,obsticalLayer);
    }

    void Start()
    {
        StartCoroutine(Jump());
    }

    void Update()
    {
        CheckAhead();
    }

    IEnumerator Jump()
    {
        if (pathClear)
        {
            transform.position += transform.up * jumpDist;
        }
        else
        {
            if (jumpDist < jumpDistMax)
            {
                jumpDist++;
            }
            else
            {
                int a = Random.Range(0, 2);
                if (a == 0)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * 90);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * -90);
                }
            }
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Jump());
    }
}
