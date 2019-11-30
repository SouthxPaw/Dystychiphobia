using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _distDeAgro = 2.0f;
    [SerializeField] private float _distAttackAnim = 0.24f;
    private float _heightAboveGnd;
    private bool _playerJump = false;
    private bool _enemyFlying = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        _heightAboveGnd = playerPos.position.y;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position, _speed * Time.deltaTime);
        if (Mathf.Abs((animator.transform.position.x) - playerPos.position.x) > _distDeAgro || Mathf.Abs((animator.transform.position.x) - playerPos.position.x) < _distAttackAnim && !_playerJump && !_enemyFlying)
        {
            animator.SetBool("isFollowing", false);
        }
        if (playerPos.position.y > _heightAboveGnd)
        {
            _playerJump = true;
        }
        else
        {
            _playerJump = false;
        }
        if (animator.transform.position.y > _heightAboveGnd)
        {
            _enemyFlying = true;
        }
        else
        {
            _enemyFlying = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
