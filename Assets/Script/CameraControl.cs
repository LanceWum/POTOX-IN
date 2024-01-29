using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineImpulseSource inpulseSourse;
    public VoidEventSO cameraShakeEvent;
    // Start is called before the first frame update
    void OnEnable()
    {
        cameraShakeEvent.OnEventRaised+=OnCameraShakeEvent;
    }
    void OnDisable()
    {
        cameraShakeEvent.OnEventRaised-=OnCameraShakeEvent;
    }
    void OnCameraShakeEvent()
    {
        inpulseSourse.GenerateImpulse();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
