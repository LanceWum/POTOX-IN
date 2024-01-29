using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FameBar : MonoBehaviour
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
        image.fillAmount=1.0f*LevelManager.instance.fame/100.0f;
    }

    void FixedUpdate()
    {

    }
}
