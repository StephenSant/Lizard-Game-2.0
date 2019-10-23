using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2;
    public float dir;
    public float boostMultiplier = 2;
    public float maxBoost;
    float curBoost;
    bool canBoost;
    bool boosting;
    public bool hidden;
    public Rigidbody2D rigid;
    public Animator animator;
    GameManager gm;


    private void Start()
    {
        curBoost = maxBoost;
        gm = GameManager.instance;
    }

    void Update()
    { 
        gm.boostAmount = curBoost;
        if (gm.boostActive && canBoost)
        {
            canBoost = false;
            boosting = true;
        }

        if (Input.GetKey(KeyCode.Space) && canBoost)
        {
            canBoost = false;
            boosting = true;
        }
        if (boosting)
        {
            Boost();
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
            Move();
        }
        if (curBoost <= 0)
        {
            boosting = false;
        }
        if (!boosting && !canBoost && !hidden)
        {
            curBoost += Time.deltaTime / 2;
        }
        if (curBoost >= maxBoost)
        {
            curBoost = maxBoost;
            canBoost = true;
        }

        Turn();
    }



    void Move()
    {
        rigid.velocity = new Vector2(Time.deltaTime * (gm.horizontalInput * moveSpeed * 100), Time.deltaTime * (gm.verticalInput * moveSpeed * 100));

        if (rigid.velocity.x < -0.01f || rigid.velocity.x > 0.01f || rigid.velocity.y < -0.01f || rigid.velocity.y > 0.01f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
    void Boost()
    {

        curBoost -= Time.deltaTime;
        rigid.velocity = new Vector2(Time.deltaTime * (gm.horizontalInput * moveSpeed * boostMultiplier * 100), Time.deltaTime * (gm.verticalInput * moveSpeed * boostMultiplier * 100));

    }

    void Turn()
    {
        Vector2 dirPos = transform.position + new Vector3(gm.horizontalInput, gm.verticalInput, 0);
        Vector2 direction = new Vector2(dirPos.x - transform.position.x, dirPos.y - transform.position.y);
        transform.up = direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            hidden = true;
        }
        if (!hidden)
        {
            if (other.CompareTag("Bug"))
            {
                Bug bug = other.GetComponent<Bug>();
                gm.score += bug.pointsToGive;
                Destroy(other.gameObject);
            }
            if (other.CompareTag("Bird"))
            {
                gm.GameOver();
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            hidden = false;
        }
    }
}
