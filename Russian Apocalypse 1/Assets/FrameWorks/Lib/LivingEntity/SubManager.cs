using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubManager : MonoBehaviour {
    public abstract void UpdateManager(MKInputData inputData);
    //public abstract void UpdateManager();
    public abstract void CompositeData(MKInputData inputData);
}
