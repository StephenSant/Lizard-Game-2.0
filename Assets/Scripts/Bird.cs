using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

    public Transform player;

    Rigidbody2D rigid;

    GameManager gm;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        gm = GameManager.instance;
    }
    void Update()
    {
        if (player != null)
        {
            rigid.velocity = transform.up * Time.deltaTime * (moveSpeed * (gm.score+50));
            transform.rotation = RotateTowards(player.position, rotSpeed + gm.score);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    Quaternion RotateTowards(Vector3 target, float rotationSpeed)
    {
        Vector2 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }
    Quaternion RotateTowards(Vector3 target)
    {
        Vector2 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        return rot;
    }

}
