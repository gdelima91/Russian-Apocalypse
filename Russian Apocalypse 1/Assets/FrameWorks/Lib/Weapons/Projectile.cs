using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    public Color trailColour;
    public float speed = 10;
    float damage = 1;

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

    private void OnCollisionEnter(Collision collision)
    {
        LEPhysicsPros physicsPros = collision.collider.GetComponent<LEPhysicsPros>();
        if (physicsPros != null)
        {
            physicsPros.Recive_Damage(100);
            Destroy(gameObject);
        }
    }

}
