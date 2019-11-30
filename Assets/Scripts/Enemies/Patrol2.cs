using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol2 : MonoBehaviour
{
    PlayerMovement player;
    public float speed = 2.0f;

    private Transform target;
    [SerializeField]
    private Animator animator;
    public bool moveRight;
    public bool inRange;
    public bool playerHit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHit)
        {
            Patrol();

        }
    }

    private void Patrol()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, 1);
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
