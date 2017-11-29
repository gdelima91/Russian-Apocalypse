using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class GunController : MonoBehaviour {

    public GameObject bullet;
    private Transform bulletSpawnPoint;
    private Transform muzzlePoint;
    public AudioClip gunShot;
    private AudioSource audioSource;
    private bool canShoot = true;
    public float fireRate = 0.25f;
    PlayerStats playerStats;

    public float recoil;
    public float maxRecoil;
    public float recoilClimb;
    public float recoilRegen;

    // Use this for initialization
    void Start () {
        
        bulletSpawnPoint = GameObject.Find("BulletSpawnPoint").transform;
        muzzlePoint = GameObject.Find("MuzzlePoint").transform;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerInput();
	}

    void PlayerInput () {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (canShoot) {
                //wantsToShoot = true;
                SpawnBullet();
                canShoot = false;
                StartCoroutine("WaitFireRate");
                //playerStats.canHeal = false;





                //cameraController.StartCoroutine(cameraController.Shake());
            }

        }

        else {

           // playerStats.canHeal = true;
            if (recoil > 0) {
                recoil -= recoilRegen * Time.deltaTime;
            }

            else {
                recoil = 0;
            }
        }
    }

    void SpawnBullet() {
        if (recoil < maxRecoil) {
            recoil += recoilClimb * Time.deltaTime;
        }

        if (recoil > maxRecoil) {
            recoil = maxRecoil;
        }
        //SpawnBullet();
        //for (int i = 0; i < 6 && shotCounter <= 0; i++) {

        // The shotCounter is set to the fireRate, and the fireRate must be less or equal to zero before spawning another bullet.
        //shotCounter = fireRate;
        // Set the bullet's Vector.right to the gun's angle and add recoil.
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-recoil, recoil)));
        Instantiate(bullet, bulletSpawnPoint.position, new Quaternion(bulletSpawnPoint.rotation.x, bulletSpawnPoint.rotation.y + Random.Range(-recoil, recoil), bulletSpawnPoint.rotation.z, bulletSpawnPoint.rotation.w));
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GameObject.Find("Player").GetComponent<Collider>(), true);
        // Play gunShot audio
        audioSource.pitch = (Random.Range(0.75f, 1.25f));
        GetComponent<AudioSource>().PlayOneShot(gunShot, 1);

    }

    IEnumerator WaitFireRate() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
