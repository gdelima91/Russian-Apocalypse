using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManager : Singleton<TempManager> {

    protected TempManager() { }

    //This is not a good Way to Do it.....
    //  But I got some BUG on AI FieldOfView. And Have not time to Find out and fix it.
    // This is the simplest way to do it.
    public Collider playerCollider;

}
