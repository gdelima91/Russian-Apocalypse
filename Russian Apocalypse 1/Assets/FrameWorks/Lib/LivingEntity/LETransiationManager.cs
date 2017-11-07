using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LETransiationManager : MonoBehaviour {

    public bool freezeY = true;

    public TransitionControlType controlType = TransitionControlType.RootMotion;

    
    public LETransitionDriver transitionDriver = new LETransitionDriver();


    protected Transform mainBody;
    V.LECameraManager cameramanager;
    
    private Vector3 transitionVH;

   
    NavMeshAgent navMeshAgent;
    NavMeshHit hit = new NavMeshHit();

    private void Start()
    {
        cameramanager = GetComponentInChildren<V.LECameraManager>();
        mainBody = transform.root.GetComponentInChildren<Animator>().transform;
        transitionDriver.Start(this);
    }

    private void Update()
    {
        transitionDriver.Update();
    }

    public void UpdateTransition(V.LEUserInput input)
    {
        if (controlType == TransitionControlType.RootMotion) { /*If We use Root Motion, We just Dont do Anything*/}
        else if (controlType == TransitionControlType.NaveMesh) { UpdateNaveMeshAngent(); }
        else if (controlType == TransitionControlType.Forward_Camera) { UpdateTransitionBasedOn_Camera_Forward(input); }
        else if (controlType == TransitionControlType.Forward_MainBody) { UpdateTransitionBasedOn_MainBody_forward(input); }
        else if (controlType == TransitionControlType.NaveMesh) { /*if NavMesh We dont do anything.*/}
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
            transitionVH = (forward * input.currentVH.y + right * input.currentVH.x);

            if (freezeY){transitionVH.y = 0.0f; transitionVH.Normalize();}
            else {transitionVH.Normalize();}
            transitionDriver.Set_TransitionVH(transitionVH);
        }
    }

    //Main Body is the transform which contain the animator....
    public void UpdateTransitionBasedOn_MainBody_forward(V.LEUserInput input)
    {
        if (input.currentVH != Vector2.zero)
        {
            Vector3 forward = new Vector3(mainBody.forward.x, transform.position.y, mainBody.forward.z).normalized;
            Vector3 right = new Vector3(mainBody.right.x, transform.position.y, mainBody.right.z).normalized;
            transitionVH = (forward * input.currentVH.y + right * input.currentVH.x);

            if (freezeY) { transitionVH.y = 0.0f; transitionVH.Normalize(); }
            else { transitionVH.Normalize(); }

            transitionDriver.Set_TransitionVH(transitionVH);
        }
    }

    public void Set_Nav_Destination(Vector3 pos)
    {
        if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); if (navMeshAgent == null) { Debug.LogError("Game Object Dont have NaveMeshAgent Component"); return; } }
        navMeshAgent.SetDestination(pos);
    }

    public bool SamplePosition(Vector3 Pos)
    {
        if (NavMesh.SamplePosition(Pos, out hit, 1.0f, NavMesh.AllAreas)){ return true;} else { return false; }   
    }

    public bool ArriveDestination_NotPathPending()
    {
        if (navMeshAgent == null) { navMeshAgent = GetComponent<NavMeshAgent>(); if (navMeshAgent == null) { Debug.LogError("Game Object Dont have NaveMeshAgent Component"); return false; } }
        return navMeshAgent.ArriveDestination_NotPathPending();
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

