using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LE_Enemy1 : LEMainBase {

    public float health = 5000;
    public GameObject ragDoll;
    public GameObject blood;
    private Player_Bark_Call pBC;
    private DropManager dropManager;
    private bool hasDied = false;
    public Vector3 velocity;

    void Start() {
        pBC = GetComponent<Player_Bark_Call>();
        dropManager = GetComponent<DropManager>();
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
        if (health <= 0) {
            Die(velocity);
        }
        return false;
    }

    public void Die(Vector3 velocity)
    {
        if (!hasDied) {
            hasDied = true;
            GameObject deadRagDoll = Instantiate(ragDoll, new Vector3 (transform.position.x, 0, transform.position.z), transform.rotation);
            deadRagDoll.GetComponent<Rigidbody>().AddForce(transform.forward * 10000000);
            //GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),transform.position,transform.rotation);
            dropManager.RNGDrop(dropManager.rNGtoMinus);
            Instantiate(blood, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
        }
        pBC.RNGBark();

        Destroy(gameObject);
        

    }

    private void OnDestroy() {
       
        //deadRagDoll.transform.parent = null;
    }

}
