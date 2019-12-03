using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float distance;
    //variables to control the fire rate
    public float _canFire = 2.1f;
    public float _fireRate = 6.0f;

    [SerializeField] private GameObject _particlePrefab;

    private Animator anim;

    private bool movingLeft = true;

    //public Transform groundDetection;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        /*transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false) {

            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }

        }*/
        StartCoroutine(ParticleDelay());
    }

    void fireParticle()
    {
        //used to control the firing rate
        _canFire = Time.time + _fireRate;

        Instantiate(_particlePrefab, transform.position + new Vector3(-0.4f, -0.25f, 0), Quaternion.identity);
    }

    IEnumerator ParticleDelay()
    {
        anim.SetTrigger("Attack2");
        if (Time.time > _canFire)
        {
            fireParticle();
            Debug.Log(Time.time);
        }
        yield return new WaitForSeconds(0.9f);
    }

}
