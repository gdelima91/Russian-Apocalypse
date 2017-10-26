using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputClient {
    void Init(InputClientManager manager);
    void GetKey_A();
    void GetKey_A_Down();
    void GetKey_A_Up();
    void GetKey_B_Down();
    void ShutDown();
    void SetIInputActableItemStatu(bool s);
    void SetUpLayer(int layer);
    void DisableCollision();
    void EnableCollision();
}
