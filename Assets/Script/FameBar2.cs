using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FameBar2 : MonoBehaviour
{
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image=this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount=1.0f*LevelManager2.instance.fame/200.0f;
    }

    void FixedUpdate()
    {

    }
}
