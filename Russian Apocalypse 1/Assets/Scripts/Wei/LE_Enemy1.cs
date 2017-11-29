using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LE_Enemy1 : LEMainBase {

    public float health = 5000;

    public override void Dispatch_Animation_Message(AnimationMessageType messageType, object messageValue)
    {
       
    }

    public override void Pause(bool b)
    {
        
    }

    public override bool Damage(float number)
    {
        health -= number;
        if (health < 0) { Die(); }
        return false;
    }

    public void Die()
    {
        GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),transform.position,transform.rotation);
        Destroy(gameObject);
    }

}
