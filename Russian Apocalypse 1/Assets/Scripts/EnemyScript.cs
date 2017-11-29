using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject player;
    bool canShoot = true;
    public EnemyBullet bullet;
    public Transform firePoint;
    public float fireRate = 0.1f;
    public int minFireBurst;
    public int maxFireBurst;
    private int fireBurst;
    private bool resetBurst = true;
    public float minBurstWaitTime;
    public float maxBurstWaitTime;
    private float burstWaitTime;
    private AudioSource audioSource;
    public AudioClip gunShot;
    private bool canSeePlayer;
    private bool canBurstShoot = false;

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        fireBurst = Random.Range(minFireBurst, maxFireBurst);
        burstWaitTime = Random.Range(minBurstWaitTime, maxBurstWaitTime);
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        TryToShoot();
        VisionCheck();
    }

    void VisionCheck () {
        Ray visionRay = new Ray(transform.position, firePoint.transform.TransformDirection(Vector3.right));
        RaycastHit objectHit;

        // Find better way to write this
        
        if (Physics.Raycast(visionRay, out objectHit, 100f)) {
            if (Physics.Raycast(visionRay, out objectHit, Vector3.Distance(transform.position, objectHit.point))) {
                if (objectHit.transform.tag == "Player") {
                    canSeePlayer = true;
                }
                else {
                    canSeePlayer = false;
                }
            }
        }
        Debug.DrawLine(transform.position, objectHit.point, Color.green);
    }

    void TryToShoot() {
        if (canSeePlayer) {
            canBurstShoot = true;
            BurstShoot();
        }
    }

    void BurstShoot() {
        if (canBurstShoot) {
            canBurstShoot = false;
            while (canShoot && fireBurst > 0) {
                Shoot();
                Debug.Log("is shooting");
            }
            if (resetBurst && fireBurst < 1) {
                resetBurst = false;
                StartCoroutine("FireBurstTimer");
            }
        }
    }

    void Shoot () {
        Quaternion bulletRotation = transform.rotation;
        Instantiate(bullet, firePoint.position, new Quaternion(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z, firePoint.rotation.w));
        fireBurst -= 1;
        canShoot = false;
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.PlayOneShot(gunShot, 1);
        StartCoroutine("FireRateTimer");
    }


    IEnumerator FireRateTimer() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator FireBurstTimer() {
        yield return new WaitForSeconds(burstWaitTime);
        fireBurst = Random.Range(minFireBurst, maxFireBurst);
        resetBurst = true;
    }
}
