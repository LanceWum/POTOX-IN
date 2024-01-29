using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteRunnaway : MonoBehaviour
{
    public float RunnawayInterval=4f;
    public float RunnawayCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        RunnawayCounter=0f;
    }

    // Update is called once per frame
    void Update()
    {
        RunnawayCounter+=Time.deltaTime;
        if(RunnawayCounter>=RunnawayInterval)
        {
            gameObject.GetComponent<Animator>().SetBool("isRun",true);
            gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
            Destroy(gameObject,0.5f);
        }
    }
}
