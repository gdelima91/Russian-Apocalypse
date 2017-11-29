﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace AiUtility{

    //Dont use Serializable proporty and Constructor at the same time
    //Because when we use Constructor. We actually create a instance of eye Transform.
    //the eye Transform will not be update with the original Transform

    [System.Serializable]
    public class FieldOfView
    {
        public Transform eye;
        public float viewDistance;
        public float viewAngle;

        [HideInInspector]public Collider collider;

        public T Find_Nearest_Obj_InsideFieldOfViewRange<T>() where T : MonoBehaviour {
            List<T> allObjectInrange = new List<T>();
            List<T> allObjectInView = new List<T>();

            //Find all Object in the Radius range
            AiFind.Finds<T>(eye.position, viewDistance,ref allObjectInrange);

            //find all Object in View
            foreach (T t in allObjectInrange) {
                if (Vector3.Angle(eye.forward, t.transform.position - eye.position) < viewAngle / 2.0f)
                {
                    allObjectInView.Add(t);
                }
            }

            float miniDis = float.MaxValue;
            T nearestObj = null;

            foreach (T obj in allObjectInView)
            {
                float dist = (obj.transform.position - eye.position).magnitude;
                if (dist < miniDis) {
                    miniDis = dist;
                    nearestObj = obj;
                }
            }

            return nearestObj;

        }

        public List<Collider> Get_All_Collider_InsideFieldOfViewRange()
        {
            List<Collider> allColliderInView = new List<Collider>();

            //get all Collider in a sphere
            Collider[] allColliders =  Physics.OverlapSphere(eye.position, viewDistance);

            //get all Collider in View
            foreach (Collider c in allColliders) {
                if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2) {
                    allColliderInView.Add(c);
                }
            }
            return allColliderInView;
        }

        public List<Collider> Get_All_Collider_InsideFieldOfViewRange(LayerMask layerMask)
        {
            List<Collider> allColliderInView = new List<Collider>();

            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance,layerMask);

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                {
                    allColliderInView.Add(c);
                }
            }
            return allColliderInView;
        }
     
        public void Get_All_Collider_InsideFieldOfViewRange<T>(ref List<T> allColliderTInView, Action callBack,bool boundsRaycast = false) where T : MonoBehaviour
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance);

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                T t = c.GetComponent<T>();
                if (t)
                {
                    //1. if the t aready in side the List, we skipe
                    if (allColliderTInView.Contains(t))
                    {
                        continue;
                    }

                    //Debug.Log(Vector3.Angle(eye.forward, c.transform.position - eye.position));
                    if (boundsRaycast)
                    {
                        //2. Simple check if the center of object inside Field of View
                        if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                        {
                            allColliderTInView.Add(t);
                            callBack();
                            continue; //and then return.
                        }

                        Ibounds b = c.GetComponent<Ibounds>();
                        Vector3[] vertices = b.Vertices;
                        float boundsHeight_1 = b.Bounds.center.y;   //get bounds height //instead of use a magic number. we should use bounds.center.y
                        Vector3 orginalPos_fixed = new Vector3(eye.position.x, boundsHeight_1, eye.position.z);

                        Vector3 forwardUnnormaledDir = eye.forward * viewDistance + eye.position;
                        Vector3 forwardUnnormaledDir_Fixed = new Vector3(forwardUnnormaledDir.x, boundsHeight_1, forwardUnnormaledDir.z) - orginalPos_fixed;

                        RaycastHit hitinfo;

                        //3. Raycast forward.
                        // check if we can directly get raycast from forward direction.
                        //for some very big object, for player to the center angle check may not work.
                        if (Physics.Raycast(orginalPos_fixed, forwardUnnormaledDir_Fixed,out hitinfo, viewDistance))
                        {
                            if (hitinfo.collider.gameObject == c.gameObject)
                            {
                                allColliderTInView.Add(t);
                                callBack();
                                continue;
                            }
                        }
                        else //4.Scan Field Of View
                        {
                            //check if any vertices of the bounds inside view angle. if there is, we raycast the tow view edge
                            //if hit the collider. means we can see the bounds
                            foreach (Vector3 v in vertices)
                            {
                                //Check if corner inside the view angle
                                if (Vector3.Angle(forwardUnnormaledDir_Fixed, v - orginalPos_fixed) < viewAngle / 2.0f)
                                {
                                    if (ScanFromViewEdge(orginalPos_fixed, forwardUnnormaledDir_Fixed,5))
                                    {
                                        allColliderTInView.Add(t);
                                        callBack();                                       
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //Dont use bounds raycast
                    else if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                    {
                        allColliderTInView.Add(t);
                        callBack();
                    }
                }
            }  
        }

        public Collider Get_Nearest_Collider_InsideFieldOfViewRange() {

            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance);
            //Get Check the nearest collider
            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                bool infieldOfView = Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2;
                if (infieldOfView)
                {
                    float dist = (c.transform.position - eye.position).magnitude;
                    if (dist < nearestDis) { nearestDis = dist; nearestCollider = c; }
                }
            }
            return nearestCollider;
        }

        public Collider Get_Nearest_Collider_InsideFieldOfViewRange(LayerMask layerMask)
        {
            List<Collider> allColliderInView = Get_All_Collider_InsideFieldOfViewRange(layerMask);

            //Get Check the nearest collider
            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;
            foreach (Collider collider in allColliderInView)
            {
                float dist = (collider.transform.position - eye.position).magnitude;
                if (dist < nearestDis)
                {
                    nearestDis = dist;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }

        public Collider Get_Nearest_Collider_InsideFieldOfViewRange<T>()
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance);

            //Get Check the nearest collider
            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                bool infieldOfView = Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2;
                bool hasComponentOfT = c.GetComponent<T>() != null;
                if(infieldOfView && hasComponentOfT) {
                    float dist = (c.transform.position - eye.position).magnitude;
                    if (dist < nearestDis) { nearestDis = dist; nearestCollider = c; }
                }  
            }
            return nearestCollider;

        }

        public Collider Get_Nearest_Collider_InsideFieldOfViewRange<T>(LayerMask layerMask) where T : MonoBehaviour
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance,layerMask);
            //Get Check the nearest collider
            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                bool infieldOfView = Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2;
                bool hasComponentOfT = c.GetComponent<T>() != null;
                if (infieldOfView && hasComponentOfT)
                {
                    float dist = (c.transform.position - eye.position).magnitude;
                    if (dist < nearestDis) { nearestDis = dist; nearestCollider = c; }
                }
            }
            return nearestCollider;
        }

        public Collider Get_Nearest_Collider_InsideFiledOfViewRange<T>(System.Func<T,bool> extraConditionCall) where T : MonoBehaviour
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance);

            //Get Check the nearest collider
            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                bool infieldOfView = Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2;
                T t = c.GetComponent<T>();
                if (t == null) { continue; }
                bool extraCondition = extraConditionCall(t);
                if (infieldOfView && extraCondition)
                {
                    float dist = (c.transform.position - eye.position).magnitude;
                    if (dist < nearestDis) { nearestDis = dist; nearestCollider = c; }
                }
            }
            return nearestCollider;
        }

        public Collider Get_First_Collider_InsideFieldOfViewRange()
        {

            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance);

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                {
                    return c; //Get the first in FieldOf View
                }
            }
            return null;
        }

        public Collider Get_First_Collider_InsideFieldOfViewRange(LayerMask layerMask)
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance, layerMask);

            //get all Collider in View
            foreach (Collider c in allColliders)
            {
                if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                {
                    return c;
                }
            }
            return null;
        }

        public T Get_Nearest_T_InsideFiledOfViewRange<T>(System.Func<T,FieldOfView,bool> extraConditionCall,float frequence, ref float timer,LayerMask layerMask) where T : MonoBehaviour
        {

            if(timer > Time.time) {return null;}
            timer = Time.time + frequence;

            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance,layerMask);

            //Get Check the nearest collider
            T returnT = null;
            float nearestDis = float.MaxValue;

            //get all Collider in View
            foreach (Collider c in allColliders)
            {

                bool infieldOfView = Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2;
                if (!infieldOfView) continue;
                T t = c.GetComponent<T>();
                if (t == null) { continue; }
                bool extraCondition = extraConditionCall(t,this);
                if (extraCondition)
                {
                    float dist = (c.transform.position - eye.position).magnitude;
                    if (dist < nearestDis) {
                        nearestDis = dist; returnT = t;
                    }
                }
            }
            return returnT;
        }

        public bool Get_First_Collider_CanSeeInFieldOfViewRange(LayerMask layerMask, string tag, ref Collider collider)
        {
            //get all Collider in a sphere
            Collider[] allColliders = Physics.OverlapSphere(eye.position, viewDistance, layerMask);

            foreach (Collider c in allColliders)
            {
                Vector3 dirToTarget = (c.transform.position - eye.position).normalized;
                //Check if it is Indide Field Of View
                if (Vector3.Angle(eye.forward, c.transform.position - eye.position) < viewAngle / 2)
                {
                    //Check if we can see it Directly.
                    RaycastHit hit;
                    if (Physics.Raycast(eye.transform.position, dirToTarget, out hit, viewDistance))
                    {
                        if (hit.collider.tag == tag)
                        {
                            collider = c;
                            return true;
                        }
                    }
                }
            }
            collider = null;
            return false;
        }

        //need to fix
        public bool If_Object_Bounds_InFieldOfView(Ibounds ib,bool useBoundsCheck=false)
        {
            if((ib.Transform.position - eye.position).sqrMagnitude > viewDistance * viewDistance) { return false; }
            if (useBoundsCheck)
            {
                //1. Simple check if the center of object inside Field of View
                if (Vector3.Angle(eye.forward, ib.Transform.position - eye.position) < viewAngle / 2)
                {
                    return true; //and then return.
                }

                //2. Advanced bounds checking
                Vector3[] vertices = ib.Vertices;
                float boundsHeight_1 = vertices[0].y - 0.1f;    //get bounds height
                Vector3 orginalPos_fixed = new Vector3(eye.position.x, boundsHeight_1, eye.position.z);

                Vector3 forwardPos = eye.forward * viewDistance + eye.position;
                Vector3 forwardPos_Fixed = new Vector3(forwardPos.x, boundsHeight_1, forwardPos.z) - orginalPos_fixed;

                // check if we can directly get raycast from forward direction  
                if (Physics.Raycast(orginalPos_fixed, forwardPos_Fixed, viewDistance))
                {
                    return true;
                }
                else
                {
                    //check if any vertices of the bounds inside view angle. if there is, we raycast the two view edge
                    //if hit the collider. means we can see the bounds
                    foreach (Vector3 v in vertices)
                    {
                        //Check if corner inside the view angle
                        if (Vector3.Angle(forwardPos_Fixed, v - orginalPos_fixed) < viewAngle / 2.0f)
                        {
                            if (ScanFromViewEdge(orginalPos_fixed, forwardPos_Fixed, 5))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            //Use simple object cnenter forward angle checking
            else if(Vector3.Angle(eye.forward, ib.Transform.position - eye.position) < viewAngle / 2)
            {
                return true;
            }
            return false;
        }

        public bool If_Position_InFieldOfView_AngleRange(Vector3 pos)
        {
            if (Vector3.Angle(eye.forward, pos - eye.position) < viewAngle / 2)
            {
                return true;
            }
            return false;
        }

        bool ScanFromViewEdge(Vector3 originalPos,Vector3 forwarUnnormaledDir,int stepTimes)
        {

            float halfViewAngle = viewAngle/2;
            Vector3 forwardEdge_fixed1 = Quaternion.AngleAxis(halfViewAngle, Vector3.up) * forwarUnnormaledDir;
            Vector3 forwardEdge_fixed2 = Quaternion.AngleAxis(-halfViewAngle, Vector3.up) * forwarUnnormaledDir;
            if (Physics.Raycast(originalPos, forwardEdge_fixed1, viewDistance) || Physics.Raycast(originalPos, forwardEdge_fixed2, viewDistance))
            {
                return true;
            }else{
                float rotateAnglestep = viewAngle / stepTimes;
                for (int i = 0; i < stepTimes-1; i++)
                {   //Scan from -halfViewAngle to halfViewAngle
                    forwardEdge_fixed2 = Quaternion.AngleAxis(rotateAnglestep, Vector3.up) * forwardEdge_fixed2;
                    Debug.DrawRay(originalPos, forwardEdge_fixed2,Color.black);
                    if (Physics.Raycast(originalPos, forwardEdge_fixed2, viewDistance))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        static Vector3 staticOrigin;
        static Vector3 staticForward;

        public void DebugDrawFielOfView()
        {
            Vector3 forwardPos = eye.forward * viewDistance;
            Vector3 pos1 = Quaternion.AngleAxis(-viewAngle/2.0f, Vector3.up) * forwardPos;
            Vector3 pos2 = Quaternion.AngleAxis(viewAngle/2.0f, Vector3.up) * forwardPos;
            Vector3 eyePos = new Vector3(eye.position.x, eye.position.y, eye.position.z);
            Debug.DrawRay(eyePos, forwardPos,Color.red);
            Debug.DrawRay(eyePos, pos1, Color.red);
            Debug.DrawRay(eyePos, pos2, Color.red);

        }

        public bool See_Collider_InFieldOfView(Collider c)
        {
            if (If_Position_InFieldOfView_AngleRange(c.transform.position))
            {
                if (V.VPhysics.RaycastExtention.If_Collide_Target_FromAtoB(eye.position,c.transform.position,c)) {
                    return true;
                }
            }
            return false;
            
        }

    }


    //Static Filed........
    public static class AiFind
    {
        //Find the nearest T; center is a given position;
        public static T FindNearestObj<T>(Vector3 position) where T : MonoBehaviour
        {

            float minDistance = float.MaxValue;

            T retObj = null;
            foreach (T obj in UnityEngine.Object.FindObjectsOfType(typeof(T)))
            {
                //Debug.Log(obj);
                float dist = (obj.gameObject.transform.position - position).magnitude;
                if (dist < minDistance)
                {
                    minDistance = dist;
                    retObj = obj;
                }
            }
            return retObj;
        }

        //Find the nearest T which inside the radius of range; center is a given position;
        public static T FindNearestObj<T>(Vector3 position, float range) where T : MonoBehaviour
        {

            float minDistance = float.MaxValue;

            T retObj = null;
            foreach (T obj in UnityEngine.Object.FindObjectsOfType(typeof(T)))
            {
                //Debug.Log(obj);
                float dist = (obj.gameObject.transform.position - position).magnitude;
                if (dist < minDistance)
                {
                    minDistance = dist;
                    retObj = obj;
                }
            }

            //Check the range
            if ((retObj.gameObject.transform.position - position).magnitude <= range)
            {
                return retObj;
            }
            else
            {
                return null;
            }
        }

        public static UnityEngine.Object[] Finds<T>() where T : MonoBehaviour
        {
           return UnityEngine.Object.FindObjectsOfType(typeof(T));
        }

        //Find all T Objects inside the raduis range; center is a given position. and out put to the out_objs. Return all object inside the Scene
        public static UnityEngine.Object[] Finds<T>(Vector3 position, float range, ref List<T> out_objs) where T : MonoBehaviour
        {
            UnityEngine.Object[] objs = UnityEngine.Object.FindObjectsOfType(typeof(T));
            foreach (T obj in objs)
            {
                if ((obj.transform.position - position).magnitude <= range)
                {
                    out_objs.Add(obj);
                }
            }
            return objs;
        }

        //Find all Collider with LayerMask of targetMask inside the raduis range; center is a given position
        public static Collider[] FindCollidersOverlapSphere(Vector3 position, float range)
        {
            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range); //gameobject with targetMask will be selected
            return targetsInRangeRadius;
        }

        //Find all Collider with LayerMask of targetMask inside the raduis range; center is a given position
        public static Collider[] FindCollidersOverlapSphere(Vector3 position, float range, LayerMask targetMask)
        {
            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range, targetMask); //gameobject with targetMask will be selected
            return targetsInRangeRadius;
        }

        public static void FindCollidersWithTypeOf<T>(ref List<T> targets,Vector3 pos, float range, LayerMask targetMask)
        {
            Collider[] targetsMaskInRangeRadius = Physics.OverlapSphere(pos, range, targetMask);

            if (targetsMaskInRangeRadius.Length <= 0) return;

            targets.Clear();

            foreach (Collider c in targetsMaskInRangeRadius)
            {
                T t = c.GetComponent<T>();
                if (t != null)
                {
                    targets.Add(t);
                }
            }
        }

        //Find the nearest Collider inside the raduis range; center is a given position
        public static Collider FindNearestColliderOverlapSphere(Vector3 position, float range)
        {

            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range); //gameobject with targetMask will be selected

            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            foreach (Collider collider in targetsInRangeRadius)
            {
                float dist = (collider.transform.position - position).magnitude;
                if (dist < nearestDis)
                {
                    nearestDis = dist;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }

        //Find the nearest Collider with LayerMask of targetMask inside the raduis range; center is a given position
        public static Collider FindNearestColliderOverlapSphere(Vector3 position, float range, LayerMask targetMask)
        {

            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range, targetMask); //gameobject with targetMask will be selected

            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            foreach (Collider collider in targetsInRangeRadius)
            {
                float dist = (collider.transform.position - position).magnitude;
                if (dist < nearestDis)
                {
                    nearestDis = dist;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }

        public static Collider FindNearestAliveLEColliderOverlapSphere<T>(Vector3 position, float range, LayerMask targetMask,Func<T,bool> callback) where T : MonoBehaviour
        {

            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range, targetMask); //gameobject with targetMask will be selected

            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            foreach (Collider collider in targetsInRangeRadius)
            {

                T t = collider.GetComponent<T>();
                if (!callback(t)) continue;
                float dist = (collider.transform.position - position).magnitude;
                if (dist < nearestDis)
                {
                    nearestDis = dist;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }

        //Find the nearest Collider with LayerMask of targetMask inside the raduis range; center is a given position
        public static Collider FindNearestColliderOverlapSphere(Vector2 position, float range, LayerMask targetMask)
        {
            Collider[] targetsInRangeRadius = Physics.OverlapSphere(position, range, targetMask); //gameobject with targetMask will be selected

            Collider nearestCollider = null;
            float nearestDis = float.MaxValue;

            foreach (Collider collider in targetsInRangeRadius)
            {
                float dist = (new Vector2(collider.transform.position.x, collider.transform.position.z) - position).magnitude;
                if (dist < nearestDis)
                {
                    nearestDis = dist;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }
    }


    public static class PathFindingHelper
    {
        public static bool ArriveDestination_NotPathPending(UnityEngine.AI.NavMeshAgent angent)
        {
            return (angent.remainingDistance <= angent.stoppingDistance && !angent.pathPending);
        }
    }

}
