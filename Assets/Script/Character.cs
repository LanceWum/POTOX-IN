using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    // public DissolveController dissolveController;
    [Header("Basic Attributes")] 
    public float maxHealth;
    public float currentHealth;

    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    public bool constantInvulnerable=false;
    public float CharacterPoint=0;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(constantInvulnerable)return;
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if(constantInvulnerable)return;
        if(invulnerable)
            return;
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        { 
            currentHealth = 0;
            OnDie?.Invoke();
            LevelManager.instance.AddFame(CharacterPoint);
            LevelManager.instance.MobCount--;
            gameObject.GetComponent<Animator>().SetBool("isDead",true);
            Destroy(gameObject,0.5f);
            // dissolveController.StartDissolving();
        }
        
    }

    private void TriggerInvulnerable()
    {
        invulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
}
