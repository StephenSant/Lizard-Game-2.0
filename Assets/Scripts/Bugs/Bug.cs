using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bug : MonoBehaviour
{
    public float moveSpeed;
    public int pointsToGive;
    public Transform waypointParent;
    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;
    Rigidbody2D rigid;
    private void Awake()
    {
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints.Add(waypointParent.GetChild(i).transform);
        }
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigid.velocity = transform.up * Time.deltaTime * (moveSpeed * 100);
        Vector2 curWaypoint = waypoints[waypointIndex].position;
        if (Vector2.Distance(transform.position, curWaypoint) < 1f)
        {
            if (waypointIndex < waypoints.Count - 1)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex = 0;
            }
        }
        transform.rotation = RotateTowards(curWaypoint);
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
