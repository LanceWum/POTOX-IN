using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DrainStation : MonoBehaviour
{
    public float DrainRate=50f;
    private bool isDraining=false;
    // Start is called before the first frame update
    void Start()
    {
        isDraining=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDraining)
        {
            LevelManager2.instance.botox-=DrainRate*Time.deltaTime;
            LevelManager2.instance.botox=Mathf.Max(0,LevelManager2.instance.botox);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            isDraining=true;
            AudioManager.Instance.PlaySFX("refill");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            isDraining=false;
        }
    }
}
