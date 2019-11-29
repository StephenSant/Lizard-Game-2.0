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

    Vector3 landingPos;
    Vector3 halfWayPos;
    bool moveForward;
    bool goingUp;

    public Sprite landedSprite;
    public Sprite jumpSprite;
    public SpriteRenderer spriteRenderer;

    void CheckAhead()
    {
        pathClear = !Physics2D.OverlapCircle(transform.position + (transform.up * jumpDist), 0.1f, obsticalLayer);
    }

    void Start()
    {
        StartCoroutine(Jump());
    }

    new void Update()
    {
        Debug.DrawLine(transform.position, landingPos);
        base.Update();
        CheckAhead();
        if (moveForward)
        {
            rigid.velocity = transform.up * Time.deltaTime * moveSpeed;
            hitBox.enabled = false;
            if (goingUp && spriteRenderer.transform.localScale.x <= 2)
            {
                spriteRenderer.transform.localScale += Vector3.one*1f * Time.deltaTime;
            }
            else
            {
                if (spriteRenderer.transform.localScale.x >= 1.25f)
                {
                    spriteRenderer.transform.localScale += Vector3.one * -0.5f * Time.deltaTime;
                }
            }
        }
        else
        {
            rigid.velocity = Vector3.zero;
            hitBox.enabled = true;
        }
    }

    IEnumerator Jump()
    {
        if (pathClear)
        {
            yield return new WaitForFixedUpdate();
            transform.position = transform.position + transform.forward * -1.25f;
            spriteRenderer.sprite = jumpSprite;
            landingPos = transform.position + transform.up * jumpDist;
            halfWayPos = transform.position + transform.up * (jumpDist / 2);
            moveForward = true;
            goingUp = true;
            yield return new WaitUntil(() => ((transform.position - halfWayPos).magnitude <= 0.1f));
            goingUp = false;
            yield return new WaitUntil(() => ((transform.position - landingPos).magnitude <= 0.1f));
            transform.position = transform.position + transform.forward * 1.25f;
            spriteRenderer.sprite = landedSprite;
            moveForward = false;
            yield return new WaitForSecondsRealtime(waitTime);
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
                switch (a)
                {
                    case 1:
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * 90);
                        break;
                    case 2:
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * -90);
                        break;
                    default:
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
                        break;

                }
            }
        }
        yield return new WaitForFixedUpdate();

        StartCoroutine(Jump());
    }
}
