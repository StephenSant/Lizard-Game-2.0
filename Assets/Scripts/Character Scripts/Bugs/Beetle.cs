using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Bug
{
    public float moveSpeed;
    public float rotSpeed;
    public float wallSensorLength = 1;
    public float wallSensorSeperation = 1;
    public LayerMask hitLayer;
    public Transform rightWallSensor, leftWallSensor;

    public float swayTimeMin = 0.5f;
    public float swayTimeMax = 1.5f;
    public float swayValue;

    public bool hitRight, hitLeft;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(Sway());
    }

    void FixedUpdate()
    {
        #region Sensors
        if (wallSensorSeperation != leftWallSensor.localEulerAngles.z || wallSensorSeperation != -rightWallSensor.localEulerAngles.z)
        {
            leftWallSensor.localEulerAngles = Vector3.forward * wallSensorSeperation;
            rightWallSensor.localEulerAngles = Vector3.forward * -wallSensorSeperation;
        }

        Debug.DrawRay(rightWallSensor.transform.position, rightWallSensor.transform.up * wallSensorLength, Color.red);
        Debug.DrawRay(leftWallSensor.transform.position, leftWallSensor.transform.up * wallSensorLength, Color.red);
        hitRight = Physics2D.Raycast(rightWallSensor.transform.position, rightWallSensor.transform.up, wallSensorLength, hitLayer);
        hitLeft = Physics2D.Raycast(leftWallSensor.transform.position, leftWallSensor.transform.up, wallSensorLength, hitLayer);
        #endregion
    }
    private void Update()
    {
        base.Update();
        if (hitRight && hitLeft)
        {
            rigid.velocity = Vector2.zero;
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        }
        else if (hitRight && !hitLeft)
        {
            rigid.velocity = transform.up * Time.deltaTime * moveSpeed;
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        }
        else if (hitLeft && !hitRight)
        {
            rigid.velocity = transform.up * Time.deltaTime * moveSpeed;
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        }
        else
        {
            rigid.velocity = transform.up * Time.deltaTime * moveSpeed;
            transform.Rotate(Vector3.forward * rotSpeed * swayValue * Time.deltaTime);
        }
    }

    IEnumerator Sway()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(swayTimeMin, swayTimeMax));
            swayValue = Random.Range(-0.5f, 0.6f);
        }
        //StartCoroutine(Sway());
    }
}
