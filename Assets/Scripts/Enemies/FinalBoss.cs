using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private Transform target;
    public float speed = 1.0f;
    private bool _playerHit = false;
    PlayerMovement player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.Log("Player is null");
        }
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (target == null)
        {
            Debug.Log("Target is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerHit)
        {
            Flip();
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.tag == "Player")
        {
            player.Damage();
            _playerHit = true;
        }
    }
}
