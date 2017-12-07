using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Shotgun : MonoBehaviour {

    private bool canFire = true;
    public Projectile projectile;
    public Transform muzzlePoint;
    public float shotSpread;
    public int numberOfBullets;
    private float angle;
    public float muzzleVelocity = 500;

	// Use this for initialization
	void Start () {
        angle = (shotSpread * 2) / numberOfBullets;

    }
	
	// Update is called once per frame
	void Update () {
        PlayerInput();
	}

    void PlayerInput () {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (canFire) {
                canFire = false;
                Shoot();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            canFire = true;
        }
    }

    void Shoot () {
        float currentAngle = 0;
        for (int i = 0; i < numberOfBullets; i++) {
            Projectile bullet = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation) as Projectile;
            if (i == 0) {
                bullet.transform.rotation = Quaternion.Euler(muzzlePoint.rotation.x, muzzlePoint.rotation.y - shotSpread, muzzlePoint.rotation.z);
                currentAngle = bullet.transform.rotation.y;
                bullet.gameObject.layer = gameObject.layer;
                bullet.SetSpeed(muzzleVelocity);
            }
            else {
                bullet.transform.rotation = Quaternion.Euler(muzzlePoint.rotation.x, muzzlePoint.rotation.y + currentAngle, muzzlePoint.rotation.z);
                currentAngle = currentAngle + angle;
                bullet.gameObject.layer = gameObject.layer;
                bullet.SetSpeed(muzzleVelocity);
            }
        }
    }
}
