using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerMovement _playerController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        SetAnimations();
    }

    public void SetAnimations()
    {
        bool isMoving=Mathf.Abs(_playerController.inputX)>0.1||Mathf.Abs(_playerController.inputY)>0.1;
        anim.SetBool("isMoving", isMoving);
        // anim.SetBool("isDead", _playerController.isDead);
        anim.SetBool("isAttack", _playerController.isAttack);
        anim.SetFloat("holdDuration",_playerController.holdDuration);
    }

    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}
