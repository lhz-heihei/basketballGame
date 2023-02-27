using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float move_speed;  //�ƶ��ٶ�
    private float horizontal, vertical;//ˮƽ����ֱ������
    public CharacterController cc;  //���CharacterController���
    public Transform FollowedCamera; //�����transform���
    private float gravity = -9.8f;  //����
    public float jump_speed;       //��Ծ���ٶ�
    private Vector3 dir_init,moveDir;  //�ƶ�����
    private Animator animator;      //������
    private Vector3 fall_speed;     //��ֱ�����ƶ��ٶ�
    public float hand_baskrtball_distance;  //��������֮��ľ���
    public enum CharacterState { Normal,Dribbling,Shooting}   //״̬ö��
    public CharacterState currentState = CharacterState.Normal;  //��ǰ״̬
    private void Start()
    {
        animator = GetComponent<Animator>();  //������
        fall_speed = Vector3.zero;            //��ʼ��
    }
    private void FixedUpdate()
    {
        
        switch(currentState)
        {
            case CharacterState.Normal:
                PlayerMovement();
                animator.SetFloat("Speed", dir_init.magnitude);//Speed��ֵ�����ܲ�����
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

    public void switchToNewState(CharacterState newState) //�л���״̬
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
                animator.SetBool("pickup", true);  //�л���ִ��һ�μ��򣬽������򶯻�
                break;
            case CharacterState.Shooting:
                break;
        }
        currentState = newState;
    }

}
