using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement2 : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    public float speed;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    public float inputX, inputY;
    public bool isAttack;
    public GameObject AttackZone;
    private float holdStartTime;
    public float holdDuration;
    public string AttackType;

    public bool isHurt;

    public float hurtForce;
    public bool isDead;
    private Attack attack;

    void Awake()
    {
        holdStartTime=-1.0f;
        holdDuration=-1.0f;
        AttackType="";
        inputControl=new PlayerInputControl();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attack=AttackZone.GetComponent<Attack>();
        inputControl.Gameplay.Fire.started+=AttackStarted;
        inputControl.Gameplay.Fire.performed+=AttackPerformed;
        inputControl.Gameplay.Fire.canceled+=AttackCanceled;
    }
    private void OnEnable() {
        inputControl.Enable();    
    }
    private void OnDisable() {
        inputControl.Disable();
    }

    void Update()
    {
        // gameObject.transform.rotation=Camera.main.transform.rotation;
        inputDirection=inputControl.Gameplay.Move.ReadValue<Vector2>();
        if(isAttack&&holdStartTime>=0)
        {
            holdDuration=Time.time-holdStartTime;
        }
        setAttackZone();
    }
    private void FixedUpdate() {
        if(!isAttack&&!isHurt)
            Move();
    }
    void Move()
    {
        inputX = inputDirection.x;
        inputY = inputDirection.y;
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;
        rigidbody.velocity = input * speed;
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    
    void AttackStarted(InputAction.CallbackContext obj)
    {
        if(LevelManager2.instance.botox_max-LevelManager2.instance.botox<=1e-2)return;
        if(isAttack)return;
        holdStartTime=Time.time;
    }
    void AttackPerformed(InputAction.CallbackContext obj)
    {
        if(LevelManager2.instance.botox_max-LevelManager2.instance.botox<=1e-2)return;
        rigidbody.velocity=Vector2.zero;
        // Debug.Log(obj.interaction);
        if(obj.interaction is HoldInteraction)
        {
            attack.damage=10;
            // AttackZone.GetComponent<SpriteRenderer>().color=Color.red;
            animator.SetTrigger("HeavyAttack");
            AudioManager.Instance.PlaySFX("needle3");
            AttackType="Heavy";
            isAttack=true;
        }
        else if(obj.interaction is TapInteraction)
        {
            attack.damage=5;
            // AttackZone.GetComponent<SpriteRenderer>().color=Color.green;
            animator.SetTrigger("LightAttack");
            AudioManager.Instance.PlaySFX("needle4");
            AttackType="Light";
            isAttack=true;
        }
    }
    void AttackCanceled(InputAction.CallbackContext obj)
    {
        if(LevelManager2.instance.botox_max-LevelManager2.instance.botox<=1e-2)return;
        if(isAttack&&AttackType.Equals("Light"))return;
        holdStartTime=-1.0f;
        holdDuration=-1.0f;
    }
    void setAttackZone()
    {
        if(holdDuration<0||isAttack==false)
        {
            AttackZone.SetActive(false);
            AttackZone.transform.localScale=new Vector3(1,1,1);
            return;
        }
        AttackZone.SetActive(true);
        if(AttackType.Equals("Light"))
        {
            float scale=3*1;
            AttackZone.transform.localScale=new Vector3(scale,scale,scale);
            LevelManager2.instance.suck(5f*Time.deltaTime);
        }
        else if(AttackType.Equals("Heavy"))
        {
            // float scale=3*holdDuration/2.0f;
            float scale=2.0f/holdDuration;
            AttackZone.transform.localScale=new Vector3(scale,scale,scale);
            LevelManager2.instance.suck(10f*Time.deltaTime);
        }
    }
    
}
