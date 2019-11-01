using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public int maxScore = 25;
    public float moveSpeedMultiplyer = 10;
    public float rotSpeed = 0.05f;
    public float swayTimeMin = 0.5f;
    public float swayTimeMax = 2f;
    public float swayValue;

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
        StartCoroutine(Sway());
    }
    void Update()
    {
        if (gm != null)
        {

            if (!gm.playerHidden && player != null)
            {
                if (gm.score < maxScore)
                {
                    rigid.velocity = transform.up * (gm.score * moveSpeedMultiplyer) * Time.deltaTime;
                    transform.rotation = RotateTowards(player.position, rotSpeed * gm.score);
                }
                else
                {
                    rigid.velocity = transform.up * (maxScore * moveSpeedMultiplyer) * Time.deltaTime;
                    transform.rotation = RotateTowards(player.position, rotSpeed * maxScore);
                }
            }

            else if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 12 || transform.position.y < -12)
            {
                if (gm.score < maxScore)
                {
                    Return(gm.score);
                }
                else
                {
                    Return(maxScore);
                }
            }

            else if (gm.playerHidden || player == null)
            {
                if (gm.score > maxScore)
                {
                    Wander(maxScore);
                }
                else { Wander(gm.score); }

            }
        }
        else
        {
            if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 12 || transform.position.y < -12)
            {
                Return(maxScore);
            }
            else
            {
                Wander(maxScore);
            }
        }
    }

    Quaternion RotateTowards(Vector3 target, float rotationSpeed)
    {
        Vector2 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }

    IEnumerator Sway()
    {
        yield return new WaitForSeconds(Random.Range(swayTimeMin, swayTimeMax));
        swayValue = Random.Range(-2.5f, 2.5f);
        StartCoroutine(Sway());
    }

    void Wander(int speedLevel)
    {

                    rigid.velocity = transform.up * (speedLevel * moveSpeedMultiplyer) * Time.deltaTime;
                    transform.rotation = RotateTowards(transform.position + (transform.right * swayValue), rotSpeed * speedLevel);
    }

    void Return(int speedLevel)
    {
        rigid.velocity = transform.up * (speedLevel * moveSpeedMultiplyer) * Time.deltaTime;
        transform.rotation = RotateTowards(Vector3.zero, rotSpeed * speedLevel);
    }
}
