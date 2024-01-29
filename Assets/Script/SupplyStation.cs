using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyStation : MonoBehaviour
{
    public float SupplyRate=50f;
    private bool isSupplying=false;
    // Start is called before the first frame update
    void Start()
    {
        isSupplying=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSupplying)
        {
            LevelManager.instance.botox+=SupplyRate*Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            isSupplying=true;
            AudioManager.Instance.PlaySFX("refill");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            isSupplying=false;
        }
    }
}
