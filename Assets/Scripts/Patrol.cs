using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _distance;

    private bool movingRight = true;

    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moves the enemy right
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        //creates a ray starts that shoots downward at the position of ground detection for a distance of 2
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector3.down, _distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
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
