using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    public Color trailColour;
    public float speed = 10;
    public float aKDamage = 100;
    public float shotgunDamage = 200;
    float damage = 100;

    float lifetime = 3;
    float skinWidth = .1f;
    Rigidbody rg;

    void Start()
    {
        Destroy(gameObject, lifetime);
        rg = GetComponent<Rigidbody>();
        GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColour);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void LateUpdate()
    {
        float moveDistance = speed * Time.deltaTime;
        //transform.Translate(Vector3.forward * moveDistance);
        rg.AddForce(transform.forward * moveDistance);
    }

    private void OnCollisionEnter(Collision collision) {
        LEPhysicsPros physicsPros = collision.collider.GetComponent<LEPhysicsPros>();
        if (physicsPros != null) {
            physicsPros.Recive_Damage(damage);
            if (collision.collider.GetComponent<HitmarkerPlayer>() != null) {
                collision.collider.GetComponent<HitmarkerPlayer>().PlayHitmarker();
            }

            //LE_Enemy1 leEnemy = collision.collider.GetComponent<LE_Enemy1>();
            //if (leEnemy != null) {
            //    leEnemy.Damage(damage);
            //    if (collision.collider.GetComponent<HitmarkerPlayer>() != null) {
            //        collision.collider.GetComponent<HitmarkerPlayer>().PlayHitmarker();
            //    }
            //}
            
        }
        Destroy(gameObject);
    }  
}
