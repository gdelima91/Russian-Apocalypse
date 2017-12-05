using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool opensFromFront = false;
    public bool opensFromBack = true;
    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void OpenDoor (Triggers trigger) {
        if (trigger == Triggers.Front && opensFromFront) {
            animator.SetBool("Open", true);
        }

        if (trigger == Triggers.Back && opensFromBack) {
            animator.SetBool("Open", true);
        }
    }

    public enum Triggers {Front, Back};
}
