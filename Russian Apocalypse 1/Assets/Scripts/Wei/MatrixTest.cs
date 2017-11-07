using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour {


    Vector3 Direction = Vector3.forward;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Direction * 5.0f);
        Gizmos.DrawCube(transform.position, new Vector3(1.0f, 0.000001f, 1.0f));

    }


    [ContextMenu("Rotate 45 Dgree")]
    public void RotateDir()
    {
        Matrix4x4 R = Matrix4x4Extention.CreateRotationMatrix(Vector3.up, 45);
        Direction = R.MultiplyVector(Direction);

        //Direction = V.WeiVector3.RotateVectorAround(Direction, Vector3.up, 45);
    }

    public void RotatePlane_Axis_Z()
    {

    }

}
