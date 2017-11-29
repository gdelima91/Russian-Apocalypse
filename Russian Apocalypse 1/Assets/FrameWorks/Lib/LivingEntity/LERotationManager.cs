using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LERotationManager : MonoBehaviour {

    public RotationControlType controlType = RotationControlType.Look_To_CameraDir;

    private float yaw;  //Rotation around Y Axis
    public float turnReactionSpped = 0.1f;
    public GameObject gun;
    public GameObject projec_Spawner;
    public GameObject ball;
    float turnSmoothVelocity;


    protected float postTargetMoveSpeed = 0.0f;

    protected float currentSpeed = 0.0f;
    protected float speedSmoothVelocity;

    protected CameraType cameraType;

    protected Transform SkeletonT;
    V.LECameraManager cameramanager;

    // Use this for initialization
    protected virtual void Start() {

        cameramanager = GetComponentInChildren<V.LECameraManager>();
        if (cameramanager)
        {
            cameraType = cameramanager.cameraType;
        }
        SkeletonT = transform.GetComponentInChildren<Animator>().transform;
    }

    public void UpdateRotation(V.LEUserInput userInput) {
        if (controlType == RotationControlType.Look_To_CameraDir) { Turn_To_CameraDir_Smooth(userInput); }
        else if (controlType == RotationControlType.Look_At_MouseDir) { Turn_To_MouseDir(); }
        else if (controlType == RotationControlType.Non_Strafe_And_Back) {/*this means no Rotation Control for this LE Object*/ }
    }

    public void Set_RotationControlType(RotationControlType _controlType) {
        controlType = _controlType;
    }

    //The Player Look at where The mouse and Ground Intersection
    protected void Turn_To_MouseDir()
    {
        //Gavin Edit - bringing up ground plane to match with the gun's plane
        

        if (Input.GetMouseButton(0)) {
            //Vector3 mousePos = V.MouseAndCamera.GetMouseGroundIntersectionPoint();

            Plane groundPlane = new Plane(Vector3.up, gun.transform.position);
            Vector3 mousePos;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float cameraToIntersectpointDst;
            

            if (groundPlane.Raycast(ray, out cameraToIntersectpointDst)) {
                mousePos = ray.GetPoint(cameraToIntersectpointDst);

                mousePos.y = projec_Spawner.transform.position.y;
                SkeletonT.LookAt(mousePos);
                projec_Spawner.transform.LookAt(mousePos);
                Debug.DrawLine(Camera.main.transform.position, mousePos);
                Debug.Log(mousePos);
                ball.transform.position = mousePos;
            }
        }
    }

    //When player press W the character will move forwad direction of the camera
    //When player press A the character will move left direction of the camera
    protected void Turn_To_CameraDir_Smooth(V.LEUserInput userInput)
    {
        if (userInput.currentVH == Vector2.zero) { return; }
        float targetDegree = Mathf.Atan2(userInput.currentVH.x, userInput.currentVH.y) * Mathf.Rad2Deg + cameramanager.Yaw(); // cameramanager.CurrentCamera.Transform.eulerAngles.y;
        SkeletonT.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(SkeletonT.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
    }

    //Player will look the target Position smoothly
    protected void Turn_To_TargetPosition_XZ_Smooth(Vector3 target)
    {
        float targetDegree = V.WeiTransform.AngleFromForwardToTarget_XZ(target, SkeletonT);
        SkeletonT.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(SkeletonT.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
    }

    protected void Turn_To_TargetPosition_XZ_Directly(Vector3 target)
    {
        target.y = SkeletonT.position.y;
        SkeletonT.LookAt(target);
    }

    public void Set_FacingDir()
    {

    }

}

public enum RotationControlType
{
    Look_At_MouseDir,
    Look_To_CameraDir,
    Non_Strafe_And_Back
}
