using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public float wallSensorLength = 1;
    public float wallSensorSeperation = 1;
    public LayerMask hitLayer;
    public Transform rightWallSensor, leftWallSensor;

    public bool hitRight, hitLeft;
    Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (wallSensorSeperation != leftWallSensor.localEulerAngles.z || wallSensorSeperation != -rightWallSensor.localEulerAngles.z)
        {
             leftWallSensor.localEulerAngles = Vector3.forward * wallSensorSeperation;
             rightWallSensor.localEulerAngles = Vector3.forward * -wallSensorSeperation;
        }

        Debug.DrawRay(transform.position, rightWallSensor.transform.up*wallSensorLength, Color.red);
        Debug.DrawRay(transform.position, leftWallSensor.transform.up * wallSensorLength, Color.red);
        hitRight = Physics2D.Raycast(transform.position, rightWallSensor.transform.up, wallSensorLength, hitLayer);
        hitLeft = Physics2D.Raycast(transform.position, leftWallSensor.transform.up, wallSensorLength, hitLayer);
    }
    private void Update()
    {
        if (hitRight && hitLeft)
        {
            transform.Rotate(Vector3.forward);
        }
        else if (hitRight)
        {
            rigid.velocity = transform.up * Time.deltaTime;
            transform.Rotate(Vector3.forward);
        }
        else if (hitLeft)
        {
            rigid.velocity = transform.up*Time.deltaTime;
            transform.Rotate(-Vector3.forward);
        }
        else
        {
            rigid.velocity = transform.up;
        }
    }

}
