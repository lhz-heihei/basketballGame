using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float move_speed;  //移动速度
    private float horizontal, vertical;//水平、垂直虚拟轴
    public CharacterController cc;  //获得CharacterController组件
    public Transform FollowedCamera; //相机的transform组件
    private float gravity = -9.8f;  //重力
    public float jump_speed;       //跳跃初速度
    private Vector3 dir_init,moveDir;  //移动方向
    private Animator animator;      //动画器
    private Vector3 fall_speed;     //竖直方向移动速度
    public float hand_baskrtball_distance;  //手与篮球之间的距离
    public enum CharacterState { Normal,Dribbling,Shooting}   //状态枚举
    public CharacterState currentState = CharacterState.Normal;  //当前状态
    private void Start()
    {
        animator = GetComponent<Animator>();  //动画器
        fall_speed = Vector3.zero;            //初始化
    }
    private void FixedUpdate()
    {
        
        switch(currentState)
        {
            case CharacterState.Normal:
                PlayerMovement();
                animator.SetFloat("Speed", dir_init.magnitude);//Speed数值控制跑步动画
                break;
            case CharacterState.Dribbling:
                PlayerMovement();
                break;
            case CharacterState.Shooting:
                break;
        }
    }

    void PlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        dir_init = new Vector3(horizontal, 0, vertical);
        
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

    public void switchToNewState(CharacterState newState) //切换新状态
    {
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Dribbling:
                break;
            case CharacterState.Shooting:
                break;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Dribbling:
                animator.SetBool("pickup", true);  //切换后执行一次捡球，进入运球动画
                break;
            case CharacterState.Shooting:
                break;
        }
        currentState = newState;
    }

}
