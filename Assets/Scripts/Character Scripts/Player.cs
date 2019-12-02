using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2;
    public float boostMultiplier = 2;
    Vector2 keptDirection;
    public float maxBoost;
    float curBoost;
    bool canBoost;
    bool boosting;
    public bool hidden;
    public Rigidbody2D rigid;
    public Animator animator;
    GameManager gm;
    Vector2 inputAxis;

    public SpriteRenderer playerSprite;
    public ParticleSystem boostParticles;

    private void Start()
    {
        curBoost = maxBoost;
        gm = GameManager.instance;
    }

    void Update()
    {
        gm.uIManager.pointsIndicator.position = Camera.main.WorldToScreenPoint(transform.position);

        animator.SetFloat("Speed", inputAxis.magnitude);

        gm.playerHidden = hidden;

        if (hidden)
        {
            playerSprite.color = Color.grey;
        }
        else
        {
            playerSprite.color = Color.white;
        }

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
            Sound runSound;
            runSound = System.Array.Find(gm.audioManager.sounds, sound => sound.name == "Run");
            if (runSound != null)
            {
                if (!runSound.source.isPlaying)
                {
                    gm.audioManager.PlaySound("Run");
                }
            }

        }
        else
        {
            Move();
            animator.SetBool("Running", false);
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

        Rotate();

#if UNITY_EDITOR || UNITY_STANDALONE|| UNITY_WEBGL
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#else
        inputAxis = new Vector2(gm.horizontalInput, gm.verticalInput);
#endif

        if (boosting && rigid.velocity.magnitude > 0.5f)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            boostParticles.enableEmission = true;
#pragma warning restore CS0618 // Type or member is obsolete
        }
#pragma warning disable CS0618 // Type or member is obsolete
        else { boostParticles.enableEmission = false; }
#pragma warning restore CS0618 // Type or member is obsolete
    }



    void Move()
    {
        rigid.velocity = new Vector2(Time.deltaTime * (inputAxis.x * moveSpeed * 100), Time.deltaTime * (inputAxis.y * moveSpeed * 100));

    }
    void Boost()
    {
        curBoost -= Time.deltaTime;
        rigid.velocity = new Vector2(Time.deltaTime * (inputAxis.x * moveSpeed * boostMultiplier * 100), Time.deltaTime * (inputAxis.y * moveSpeed * boostMultiplier * 100));
    }

    void Rotate()
    {
        if (inputAxis.x == 0 && inputAxis.y == 0)
        {
            transform.up = keptDirection;
        }

        else
        {
            Vector2 dirPos = transform.position + new Vector3(inputAxis.x, inputAxis.y, 0);
            Vector2 direction = new Vector2(dirPos.x - transform.position.x, dirPos.y - transform.position.y);
            transform.up = direction;
            keptDirection = direction;
        }


    }

    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            hidden = true;
            other.GetComponent<Plant>().fadeToTransparency = true;
        }
        if (other.CompareTag("Bug"))
        {
            Bug bug = other.GetComponent<Bug>();
            gm.score += bug.pointsToGive;
            gm.uIManager.ShowPointsAdded(bug.pointsToGive);
            Destroy(other.gameObject);
            if (other.GetComponent<Ant>())
            {
                gm.audioManager.PlaySound("Eat Ant");
            }
            else if (other.GetComponent<Cricket>())
            {
                gm.audioManager.PlaySound("Eat Cricket");
            }
            else if (other.GetComponent<Beetle>())
            {
                gm.audioManager.PlaySound("Eat Beetle");
            }
        }
        if (!hidden)
        {
            if (other.CompareTag("Bird"))
            {
                gm.PlayerEaten();
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            hidden = false;
            other.GetComponent<Plant>().fadeToTransparency = false;
        }
    }
}
