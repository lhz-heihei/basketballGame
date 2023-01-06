using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    public float move_speed;
    private float horizontal, vertical;
    public CharacterController cc;
    public Transform FollowedCamera;
    private float fall_speed = 9.8f;
    private Vector3 dir_init,moveDir;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        
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

        }
        if (!cc.isGrounded)
        {
            Vector3 fall = Vector3.up * (-fall_speed);
            cc.Move(fall*Time.deltaTime);
        }
        

    }
}
