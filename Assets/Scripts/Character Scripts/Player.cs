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
        gm.boostAmount = curBoost;
        if (gm.boostActive && canBoost)
        {
            canBoost = false;
            boosting = true;
        }
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space) && canBoost)
        {
            canBoost = false;
            boosting = true;
        }
#endif
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
            curBoost += Time.deltaTime/2;
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
#if UNITY_EDITOR
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

#else
        rigid.velocity = transform.up * Time.deltaTime * (gm.verticalInput * moveSpeed * 100);
        #endif
    }
    void Boost()
    {
#if UNITY_EDITOR
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
#else
        curBoost -= Time.deltaTime;
        rigid.velocity = transform.up * Time.deltaTime * (gm.verticalInput * moveSpeed * boostMultiplier * 100);
#endif

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

        transform.Rotate(0, 0, (-rotSpeed * 100 * gm.horizontalInput) * Time.deltaTime, Space.Self);
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
