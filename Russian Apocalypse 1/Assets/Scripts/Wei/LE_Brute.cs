using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LE_Brute : LEMainBase {

    V.LEUserInput userInput;
    V.LECameraManager cameraManager;
   

    private void Start()
    {
        userInput = GetComponent<V.LEUserInput>();
        cameraManager = GetComponent<V.LECameraManager>();
    }

    private void Update()
    {
        userInput.UpdateInput();
        cameraManager.UpdateCameraManager(userInput);
        rotationManager.UpdateRotation(userInput);
        transitionManager.UpdateTransition(userInput);
   
    }

    public override void Dispatch_Animation_Message(AnimationMessageType messageType, object messageValue)
    {
        
    }

    public override void Pause(bool b)
    {
       
    }
}
