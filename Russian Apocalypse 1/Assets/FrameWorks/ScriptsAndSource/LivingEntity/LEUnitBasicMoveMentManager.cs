using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEUnitBasicMoveMentManager : MonoBehaviour {

    
    private float yaw;  //Rotation around Y Axis
    public float turnReactionSpped = 0.1f;
    float turnSmoothVelocity;

    public bool drivenByInput;

    Vector3 velocityNor = Vector3.zero;

    [SerializeField]
    protected Vector3 velocity3D = Vector3.zero;
    protected float postTargetMoveSpeed = 0.0f;

    protected float currentSpeed = 0.0f;
    protected float speedSmoothVelocity;
    

    protected bool strafe = false;
    protected Vector2 InputVH;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float speedScales;
    [SerializeField]
    protected float maxSpeed;

    protected Transform cameraT;
    protected CameraType cameraType;
    Visin1_1.CameraManager cameramanager;

    // Use this for initialization
    protected virtual void Start() {  

        cameramanager = GetComponentInChildren<Visin1_1.CameraManager>();
        if (cameramanager)
        {
            cameraT = cameramanager.transform;
            cameraType = cameramanager.cameraType;
        }
        maxSpeed = moveSpeed * speedScales;
    }

    public abstract void UpdateBasicMoveMent();

    //This is ususally for first person Camera view
    protected void TransformLookAtCameraForward()
    {
        float targetDegree = Mathf.Atan2(InputVH.x, InputVH.y) * Mathf.Rad2Deg + cameramanager.Yaw();
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
    }

    protected void LookAroundMouseDir()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Visin1_1.MouseAndCamera.GetMouseGroundIntersectionPoint();
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos);
        }
    }

    //When player press W the character will move forwad direction of the camera
    //When player press A the character will move left direction of the camera
    protected void TurnAroundBasedOn_CameraDir()
    {
        float targetDegree = Mathf.Atan2(InputVH.x, InputVH.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
    }

    protected void TunOnTargetPosition_XZ_SmoothDamp(Vector3 target)
    {
        float targetDegree = Visin1_1.WeiTransform.AngleFromForwardToTarget_XZ(target, transform);
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
    }

    protected void TunOnTargetPosition_XZ_Directly(Vector3 target)
    {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    //When player press W move direction will be transform.forward
    //When player press S move direction will be -transform.forward
    protected void VelocityDirBasedOnForwardDirection_2D()
    {
        velocityNor = transform.forward * Mathf.Sign(InputVH.x);
    }

    protected void VelocityDirBasedOnForwardDirection_4D()
    {
        velocityNor = transform.forward * Mathf.Sign(InputVH.x) + transform.right * Mathf.Sign(InputVH.y);
    }

    //This is used for riggid body driven.....
    protected void CalculateCurrentVelocity3D()
    {
        postTargetMoveSpeed = maxSpeed * InputVH.normalized.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, postTargetMoveSpeed, ref speedSmoothVelocity, turnReactionSpped);

        velocity3D = currentSpeed * velocityNor;
    }


    public virtual void SetStrafe(bool _strafe) { strafe = _strafe; }

    public virtual void SetInputVH(Vector2 _inputVH) { InputVH = _inputVH; }
}
