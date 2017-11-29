using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LETransitionDriver
{
    LETransiationManager manager;
    CharacterController cc;

    public TransitionDriverType driverType = TransitionDriverType.CharacterController;

    [Header("Character Controller Setting")]
    [Range(0.1f,100)]
    public float moveSpeed = 5.0f;
    public float gravity = -12;

    [Header("Riggid Body Setting")]
    public float JunpForce;

    float velocityY;
    Vector3 transitionVH;

    public void Start(LETransiationManager manager)
    {
        cc = manager.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (driverType == TransitionDriverType.CharacterController)
        {
            velocityY += Time.deltaTime * gravity;
            if (cc.isGrounded) { velocityY = 0; }

            transitionVH = transitionVH * moveSpeed + Vector3.up * velocityY;
            cc.Move(transitionVH * Time.deltaTime);
        }


        transitionVH = Vector3.zero;
    }

    public void Set_TransitionVH(Vector3 vh)
    {
        transitionVH = vh;
    }


}

public enum TransitionDriverType
{
    RigidBody,
    CharacterController
}
