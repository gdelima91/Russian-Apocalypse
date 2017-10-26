using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerCamera{
    CameraType CameraType { get; }
    Transform Transform { get; }

    void InitialCamera();
    void UpdateCamera();
    void LateUpdateCamera();
    void SetCameraDetal(Visin1_1.CameraManager.CameraDelta delta);
    float Yaw { get; }
}

public enum CameraType { FirstPerson, ThridPerson, God }
public enum CameraRotateModel { Fixed,Free,KeyBoardRestrict}
