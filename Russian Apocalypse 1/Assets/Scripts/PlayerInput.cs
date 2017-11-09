using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {

    #region Variables
    [Header("Player Movement")]
    public float movementSpeed = 0.1f;
    public float fireRate = 0.25f;
    public float gravity = -0.5f;
    public bool gravityIsOn = true;

    public float recoil;
    public bool mouseControls;

    public Transform firePoint;
    //public Bullet bullet;
    //public MuzzleFlashController muzzleFlash;
    //public CameraController cameraController;
    public GameObject muzzleFlashGO;

    private float angle;
    private float shotCounter;
    private float x;
    private float y;
    public Vector3 pointToLook;
    private Vector3 object_pos;
    public GameObject revolver;

    private AudioSource audioSource;

    private bool canShoot = true;
    private Vector2 rightThumbstick;

    bool wantsToShoot = false;

    [Space]

    [Header("Misc")]
    public float groundCheckRadius;
    public float killY;
    public Transform groundCheck;
    public AudioClip gunShot;
    public GameObject bullet;

    [HideInInspector]
    public bool isGrounded;

    private Rigidbody rb;

    public bool usingController;

    private Vector2 leftThumbstickAngle;

    public Camera playerCamera;

    public float maxRecoil;
    public float recoilClimb;
    public float recoilRegen;

    public PlayerStats playerStats;
    public Animator animator;

    #endregion

    void Start()
    {
        // Initialize variables
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 12);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        audioSource = GetComponent<AudioSource>();
        muzzleFlashGO = GameObject.Find("MuzzleFlash");


    }

    void Update()
    {
        KillY();

        // Must go in the order of: PointToAim, HandCheck, PlayerShoot.
        PointToAim();
        //HandCheck();
        PlayerShoot();
        //Ray laser = new Ray(transform.position, transform.right);
        //Debug.DrawLine(GameObject.Find("Hand").transform.position, transform.right);

        UpdateAnim();
    }

    void FixedUpdate()
    {
        PlayerMove();
        ApplyGravity();
        
    }

    void UpdateAnim () {

        float speedX = rb.velocity.x;
        float speedZ = rb.velocity.z;
        animator.SetFloat("Speed X", speedX);
        animator.SetFloat("Speed Z", speedZ);
    }


    void PlayerMove()
    {
        //leftThumbstickAngle = Input.GetAxis()

        // Keyboard Controls

        // Move Left
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-movementSpeed, rb.velocity.y, rb.velocity.z);
        }
        // Move Right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(movementSpeed, rb.velocity.y, rb.velocity.z);
        }

        // Move Up
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movementSpeed);
        }

        // Move Down
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -movementSpeed);
        }

        // xbox controls

        // move right
        /*        if (Input.GetAxis("lt horizontal") > 0) {
                    rb.velocity = new Vector3(movementSpeed * Input.GetAxis("lt horizontal"), rb.velocity.y, 0);
                    isFacingRight = true;
                }
                // move left
                if (Input.GetAxis("lt horizontal") < 0) {
                    rb.velocity = new Vector3(movementSpeed * Input.GetAxis("lt horizontal"), rb.velocity.y, 0);
                    isFacingRight = false;
                } */
    }


    void ApplyGravity()
    {
        if (gravityIsOn)
        {
            rb.velocity = rb.velocity + new Vector3(0, gravity, 0);
        }
    }

    void KillY()
    {
        // If the player falls too far, restart the level
        if (transform.position.y < killY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void PlayerShoot()
    {



        // On mouse left click, spawn a bullet.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                //wantsToShoot = true;
                SpawnBullet();
                canShoot = false;
                StartCoroutine("WaitFireRate");
                playerStats.canHeal = false;





                //cameraController.StartCoroutine(cameraController.Shake());
            }

        }

        else {

            playerStats.canHeal = true;
            if (recoil > 0) {
                recoil -= recoilRegen * Time.deltaTime;
            }

            else {
                recoil = 0;
            }
        }

        while (wantsToShoot)
        {
            //shotCounter -= Time.deltaTime;
            //SpawnBullet();
            //cameraController.StartCoroutine(cameraController.Shake());


        }
        //for (int i =0; i < 6; i++) {
        //SpawnBullet();
        //}

        //else {
        //    shotCounter = 0;
        //}
    }

    void SpawnBullet()
    {
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
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-recoil, recoil)));
        Instantiate(bullet, firePoint.position, new Quaternion(firePoint.rotation.x, firePoint.rotation.y + Random.Range(-recoil, recoil), firePoint.rotation.z, firePoint.rotation.w));
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GameObject.Find("Player").GetComponent<Collider>(), true);
        // Play gunShot audio
        audioSource.pitch = (Random.Range(0.75f, 1.25f));
        GetComponent<AudioSource>().PlayOneShot(gunShot, 1);

        // Spawn muzzleFlash.
        //Instantiate(muzzleFlash, firePoint.position, transform.rotation);
        //Time.timeScale = 0;

        //}
        shotCounter = 0;
        wantsToShoot = false;

    }

    void PointToAim()
    {

        if (mouseControls)
        {
            Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            Plane playPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLenght;

            // Set the angle of the gun based on where the mouse cursor position.
            if (playPlane.Raycast(cameraRay, out rayLenght))
            {
                pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }

    IEnumerator WaitFireRate ()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }




}
