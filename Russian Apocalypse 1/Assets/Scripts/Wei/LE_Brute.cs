using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LE_Brute : LEMainBase {

    V.LEUserInput userInput;
    V.LECameraManager cameraManager;

    public float health = 500f;
   

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
        animationManager.UpdateAnimation(userInput);
    }

    public override void Dispatch_Animation_Message(AnimationMessageType messageType, object messageValue)
    {
        
    }

    public override void Pause(bool b)
    {
       
    }
    public override bool Damage(float number) {
        health -= number;
        if (health <= 0) {
            GameObject obj = new GameObject();
            obj.AddComponent<Camera>();
            obj.transform.position = cameraManager.CurrentCamera.Transform.position;
            obj.transform.rotation = cameraManager.CurrentCamera.Transform.rotation;
            Destroy(gameObject);
        }

        return false;
    }
}
