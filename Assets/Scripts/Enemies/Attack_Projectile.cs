using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser to left
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        //if attack particle is less than -14.23 then destroy the particle on the x
        if (transform.position.x < -14.22)
        {
            //check if this object has a parent
            //destory parent object too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
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
