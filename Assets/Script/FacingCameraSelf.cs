using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCameraSelf : MonoBehaviour
{
    Transform[] childs;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.rotation=Camera.main.transform.rotation;
    }
}
