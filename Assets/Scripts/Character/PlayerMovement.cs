using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private LevelChanger _resetLevel;

    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;

    private bool _playerDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _resetLevel = GameObject.Find("Level_Changer").GetComponent<LevelChanger>();

        if (_resetLevel == null)
        {
            Debug.Log("Reset level is null");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_playerDead == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetKey("space"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);

            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (transform.position.y < -5.4)
            {
                Damage();
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }


    public void OnCrouch(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        //Movement    
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void Damage()
    {
        _playerDead = true;
        animator.SetTrigger("DeathAnim");
        StartCoroutine(respawnDelay());
    }

    IEnumerator respawnDelay()
    {
        yield return new WaitForSeconds(2.7f);
        _resetLevel.GetComponent<LevelChanger>().FadeToSameLevel();
    }
}
