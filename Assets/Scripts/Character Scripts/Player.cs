using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2;
    public float rotSpeed = 3;
    public float boostMultiplier = 2;
    public float maxBoost;
    float curBoost;
    bool canBoost;
    bool boosting;
    Rigidbody2D rigid;
    GameManager gm;

    void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        curBoost = maxBoost;
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canBoost)
        {
            canBoost = false;
            boosting = true;
        }
        if (boosting)
        {
            Boost();
        }
        else
        {
            Move();
        }
        if (curBoost <= 0)
        {
            boosting = false;
        }
        if (!boosting && !canBoost)
        {
            curBoost += Time.deltaTime;
        }
        if (curBoost >= maxBoost)
        {
            curBoost = maxBoost;
            canBoost = true;
        }
        Turn();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = transform.up * Time.deltaTime * (moveSpeed * 100);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = transform.up * Time.deltaTime * (-moveSpeed * 100);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }
    void Boost()
    {
        curBoost -= Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = transform.up * Time.deltaTime * (moveSpeed * boostMultiplier * 100);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = transform.up * Time.deltaTime * (-moveSpeed * boostMultiplier * 100);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }
    void Turn()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, (rotSpeed * 100) * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, (-rotSpeed * 100) * Time.deltaTime, Space.Self);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
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
