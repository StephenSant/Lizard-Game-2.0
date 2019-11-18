using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cricket : Bug
{
    public float moveSpeed;

    public int jumpDist;
    public int jumpDistMax;
    public LayerMask obsticalLayer;
    public float waitTime;

    public bool pathClear;

    public Collider2D hitBox;
    public Rigidbody2D rigid;

    public bool atPosition;
    Vector3 landingPos;
    bool moveForward;

    void CheckAhead()
    {
        pathClear = !Physics2D.OverlapCircle(transform.position + (transform.up * jumpDist), 0.5f, obsticalLayer);
    }

    void Start()
    {
        StartCoroutine(Jump());
    }

    void Update()
    {
        base.Update();
        CheckAhead();
        if (moveForward)
        {
            if (!((transform.position - landingPos).magnitude <= 0.1f))
            {
                rigid.velocity = transform.up * Time.deltaTime * moveSpeed;
                atPosition = false;
            }
            else
            {
                atPosition = true;
                rigid.velocity = Vector3.zero;
                hitBox.enabled = true;
            }
        }
    }

    IEnumerator Jump()
    {
        if (pathClear)
        { 
            hitBox.enabled = false;
            Vector3 landingPos = transform.position + transform.up * jumpDist;
            moveForward = true;
            yield return new WaitUntil(() => atPosition);
            moveForward = false;
            yield return new WaitForSeconds(waitTime);
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
        
        
        StartCoroutine(Jump());
    }
}
