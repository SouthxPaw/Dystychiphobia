using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private bool movingRight = true;

    GameObject enemy;
    private Transform playerPos;
    Rigidbody2D rb;

    private bool facingRight;

    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        enemy = GameObject.FindWithTag("Enemy");
        rb = enemy.GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindWithTag("Player").transform;

        if (rb == null)
        {
            Debug.Log("Rb is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerPos.position.x -  rb.position.x);
        float horizontal = Input.GetAxis("Horizontal");
        /*if ((playerPos.position.x - rb.position.x) < .02 && (playerPos.position.x - rb.position.x) > -.02)
        {
            Flip(horizontal);
        }*/

        Flip(horizontal);
    //TurnCharModel();
    //moves the enemy right
    /*transform.Translate(Vector3.right * _speed * Time.deltaTime);
    //creates a ray starts that shoots downward at the position of ground detection for a distance of 2
    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, _distance);
    if (groundInfo.collider == null)
    {
        Debug.Log("Raycast is null");
    }
    if (groundInfo.collider == false)
    {
        if (movingRight == true)
        {
            Debug.Log("Moving Right");
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        } else
        {
            Debug.Log("Moving Left");
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }*/
}

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage player
        if (other.tag == "Player")
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();

            if (player != null)
            {
               player.Damage();
            }
        }
    }
}
