using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float move_speed;
    private float horizontal, vertical;
    public CharacterController cc;
    public Transform FollowedCamera;
    private float gravity = -9.8f;
    public float jump_speed;
    private Vector3 dir_init,moveDir;
    private Animator animator;
    private Vector3 fall_speed;
    public enum CharacterState { Normal,Dribbling}
    private CharacterState currentState = CharacterState.Normal;
    private void Start()
    {
        animator = GetComponent<Animator>();
        fall_speed = Vector3.zero;
    }
    private void FixedUpdate()
    {
        
        switch(currentState)
        {
            case CharacterState.Normal:
                PlayerMovement();
                break;
            case CharacterState.Dribbling:
                break;
        }
    }

    void PlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        dir_init = new Vector3(horizontal, 0, vertical);
        animator.SetFloat("Speed", dir_init.magnitude);
        moveDir = FollowedCamera.forward * vertical + FollowedCamera.right * horizontal;
        moveDir.y = 0;
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);      
        }
        cc.Move( transform.forward*dir_init.magnitude * move_speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            fall_speed.y = jump_speed;
        }
        if (!cc.isGrounded)
        {
            fall_speed.y += gravity * Time.deltaTime;
            cc.Move(fall_speed * Time.deltaTime);
        }
    }

    public void switchToNewState(CharacterState newState)
    {
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Dribbling:
                break;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Dribbling:
                break;
        }
    }

}
