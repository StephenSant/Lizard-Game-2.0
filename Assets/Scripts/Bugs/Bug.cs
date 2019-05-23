using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public float wallSensorLength = 1;
    public float wallSensorSeperation = 0.1f;
    public LayerMask hitLayer;

    RaycastHit2D hitRight, hitLeft;
    Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, new Vector2(wallSensorSeperation,1), Color.red);
        Debug.DrawRay(transform.position, new Vector2(-wallSensorSeperation, 1), Color.red);
        hitRight = Physics2D.Raycast(transform.position, new Vector2(wallSensorSeperation, 1), wallSensorLength, hitLayer);
        hitLeft = Physics2D.Raycast(transform.position, new Vector2(-wallSensorSeperation, 1), wallSensorLength, hitLayer);


    }
    private void Update()
    {
        if (hitRight.collider != null && hitLeft.collider != null)
        {
            transform.Rotate(Vector3.forward);
        }
        else if (hitRight.collider != null)
        {
            Debug.Log("Right");
            rigid.velocity = transform.up;
            transform.Rotate(Vector3.forward);
        }
        else if (hitLeft.collider != null)
        {
            Debug.Log("Left");
            rigid.velocity = transform.up;
            transform.Rotate(-Vector3.forward);
        }
        else
        {
            rigid.velocity = transform.up;
        }
    }
}
