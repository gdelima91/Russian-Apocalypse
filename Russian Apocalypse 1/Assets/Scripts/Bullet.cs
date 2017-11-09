using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 0.5f;
    public int damage = 20;
    public Transform target;
    public Rigidbody rb;
    public GameObject sparks;
    public Collider nonTriggerCollider;
    public float bulletSpeedModifier;

    void Start() {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
        speed = Random.Range(-bulletSpeedModifier, bulletSpeedModifier) + speed;
    }

    void Update() {
        GoForward();
    }

    void GoForward() {
        rb.velocity = (transform.right * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != 8) {
            if (other.gameObject.layer == 11) {

                //Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
                //enemyScript.TakeDamage(damage);
                //print("Do Damage");
            }
            //speed = 0;
            //ToggleTrigger();
        }

        if (other.gameObject.tag == "Enemy") {
            Stats stat;
            stat = other.GetComponent<Stats>();
            stat.TakeDamage(damage);

            print("Hit");
            Destroy(gameObject);
        }

        //ContactPoint contact = other.contacts[0];
        //GameObject newSparks = Instantiate(sparks, contact.point, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
        //Destroy(newSparks, 0.5f);

    }

    //void ontriggerenter(collider other) {
    //    if (other.gameobject.layer != 8) {
    //        if (other.gameobject.layer == 11) {

    //            enemy enemyscript = other.getcomponent<enemy>();
    //            enemyscript.takedamage(damage);
    //            print("do damage");
    //        }
    //        //speed = 0;
    //        //toggletrigger();
    //    }

    //}

    //void ToggleTrigger () {
    //    nonTriggerCollider.isTrigger = false;
    //}
}
