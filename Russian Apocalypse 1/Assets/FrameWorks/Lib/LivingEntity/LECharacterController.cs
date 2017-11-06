using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LECharacterController : MonoBehaviour
{ 
    CharacterController cc;

    [Range(0.1f,100)]
    public float moveSpeed = 5.0f;
    public float gravity = -12;

    float velocityY;
    Vector3 transitionVH;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        if (cc == null)
        {
            Debug.LogError("No Character Controller");
        }
    }

    private void Update()
    {
        velocityY += Time.deltaTime * gravity;
        if (cc.isGrounded) { velocityY = 0; }


        transitionVH = transitionVH * moveSpeed +  Vector3.up * velocityY ;
        cc.Move(transitionVH * Time.deltaTime);

        transitionVH = Vector3.zero;
    }

    public void Set_TransitionVH(Vector3 vh)
    {
        transitionVH = vh;
    }

}