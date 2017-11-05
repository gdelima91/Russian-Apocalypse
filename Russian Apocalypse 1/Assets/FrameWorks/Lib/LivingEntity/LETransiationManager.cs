using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LETransiationManager : MonoBehaviour {

    public TransitionControlType controlType = TransitionControlType.RootMotion;

    [SerializeField]
    protected float moveSpeed = 2.0f;
    [SerializeField]
    protected float speedScales = 1.0f;
    protected float currentSpeed;

    protected Transform mainBody;
    V.LECameraManager cameramanager;
    LEAnimatorManager animationManager;

    NavMeshAgent navMeshAgent;

    private void Start()
    {
        currentSpeed = moveSpeed * speedScales;
        cameramanager = GetComponentInChildren<V.LECameraManager>();
        mainBody = transform.root.GetComponentInChildren<Animator>().transform;
        animationManager = GetComponent<LEAnimatorManager>();
    }

    public void UpdateTransition(V.LEUserInput input)
    {
        if (controlType == TransitionControlType.RootMotion) { /*If We use Root Motion, We just Dont do Anything*/}
        else if (controlType == TransitionControlType.NaveMesh) { UpdateNaveMeshAngent(); }
        else if (controlType == TransitionControlType.Forward_Camera) { UpdateTransitionBasedOn_Camera_Forward(input); }
        else if (controlType == TransitionControlType.Forward_MainBody) { UpdateTransitionBasedOn_MainBody_forward(input); }
        else if (controlType == TransitionControlType.NaveMesh) { /*if NavMesh We dont do anything.*/}

        if (animationManager) { Update_Animation_TransitionMotion(input); }
    }

    public void UpdateNaveMeshAngent()
    {
        if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); if (navMeshAgent == null) { Debug.LogError("Game Object Dont have NaveMeshAgent Component"); return; } }
        


    }

    public void UpdateTransitionBasedOn_Camera_Forward(V.LEUserInput input)
    {
        if (input.currentVH != Vector2.zero)
        {
            Vector3 forward = new Vector3(cameramanager.CurrentCamera.Transform.forward.x, transform.position.y, cameramanager.CurrentCamera.Transform.forward.z).normalized;
            Vector3 right = new Vector3(cameramanager.CurrentCamera.Transform.right.x, transform.position.y, cameramanager.CurrentCamera.Transform.right.z).normalized;
            transform.position += (forward * input.currentVH.y + right * input.currentVH.x) * Time.deltaTime * currentSpeed;
        }
    }

    //Main Body is the transform which contain the animator....
    public void UpdateTransitionBasedOn_MainBody_forward(V.LEUserInput input)
    {
        if (input.currentVH != Vector2.zero)
        {
            Vector3 forward = new Vector3(mainBody.forward.x, transform.position.y, mainBody.forward.z).normalized;
            Vector3 right = new Vector3(mainBody.right.x, transform.position.y, mainBody.right.z).normalized;
            transform.position += (forward * input.currentVH.y + right * input.currentVH.x) * Time.deltaTime * currentSpeed;
        }
    }

    public void Set_Nav_Destination(Vector3 pos)
    {
        if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); if (navMeshAgent == null) { Debug.LogError("Game Object Dont have NaveMeshAgent Component"); return; } }
        navMeshAgent.SetDestination(pos);
    }

    void Update_Animation_TransitionMotion(V.LEUserInput input)
    {
        if (input.currentVH == Vector2.zero)
        {
            animationManager.SetFloat("Speed X", 0.0f);
            animationManager.SetFloat("Speed Z", 0.0f);
        }
        else
        {

            float yaw = cameramanager.CurrentCamera.Yaw;
            float bodyY = mainBody.eulerAngles.y;
            Vector2 vh = input.currentVH.Rotate(bodyY - yaw);
            animationManager.SetFloat("Speed X", vh.x);
            animationManager.SetFloat("Speed Z", vh.y);
        }
    }

}

public enum TransitionControlType
{
    RootMotion,
    Forward_Camera,
    NaveMesh,
    InputDir,
    Forward_MainBody
}
