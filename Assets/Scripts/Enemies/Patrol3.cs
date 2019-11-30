using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol3 : MonoBehaviour
{
    public float speed = 2.0f;
    public float distToAggro = .1f;

    private Transform target;
    PlayerMovement player;
    [SerializeField]
    private Animator animator;
    public bool moveRight;
    public bool inRange = false;
    public bool lineOfSight = false;
    public bool colliderHit = false;
    public bool playerInRange = false;
    public bool playerSeen = false;
    public bool playerHit = false;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.Log("Player is null");
        }
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHit)
        {
            Flip();
            Patrol();

            if (inRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    private void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, target.position) < distToAggro && Vector2.Distance(transform.position, target.position) > 0)
        {

            inRange = true;
            playerSeen = true;
        }
        else
        {
            inRange = false;
        }

        if (!inRange)
        {
            if (playerSeen)
            {
                gameObject.transform.position = originalPos;
                transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
                transform.localScale = new Vector2(-1, 1);
                playerSeen = false;
            }
            if (moveRight)
            {
                transform.Translate(1 * Time.deltaTime * speed, 0, 0);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
        else if (trig.tag == "Player")
        {
            player.Damage();
            playerHit = true;
        }
    }
}