using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[RequireComponent(typeof(InputClientManager))]
[RequireComponent(typeof(LEUnitAnimatorManager))]
[RequireComponent(typeof(LEUnitBasicMoveMentManager))]
public abstract class LEUnitProcessorBase : MonoBehaviour {

    protected InputClientManager inputClientManager;
    protected LEUnitBasicMoveMentManager basicMovementManager;
    protected LEUnitAnimatorManager animationManager;

    protected bool enableBaiscMovement = true;

    protected bool alive = true;
    public bool Alive { get { return alive; } }
    public bool IsAlive() { return alive; }

    private void OnEnable()
    {
        inputClientManager = GetComponent<InputClientManager>();
        basicMovementManager = GetComponent<LEUnitBasicMoveMentManager>();
        animationManager = GetComponent<LEUnitAnimatorManager>();
    }

    protected virtual void Start(){}

    public abstract void Pause(bool b);

    public void SetBasicMovement_ActiveStatu(bool value)
    {
        enableBaiscMovement = value;
    }
    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public abstract void Dispatch_Animation_Message(AnimationMessageType messageType,object messageValue);

    public virtual bool AddHealth(float addHealth) { return false; }
    public virtual bool AddMana(float addMana) { return false; }

}
