using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LE_Enemy1 : LEMainBase {

    public float health = 5000;
    public GameObject ragDoll;
    public GameObject blood;

    void Start() {
        Debug.Log(transform.position);
    }

    public override void Dispatch_Animation_Message(AnimationMessageType messageType, object messageValue)
    {
       
    }

    public override void Pause(bool b)
    {
        
    }

    public override bool Damage(float number)
    {
        health -= number;
        if (health <= 0) { Die(); }
        return false;
    }

    public void Die()
    {
        GameObject deadRagDoll = Instantiate(ragDoll, transform.position, transform.rotation);
        //GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),transform.position,transform.rotation);
        Instantiate(blood, transform.position, transform.rotation);
        Destroy(gameObject);
        

    }

    private void OnDestroy() {
       
        //deadRagDoll.transform.parent = null;
    }

}
