﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[RequireComponent(typeof(LEInputableObjectManager))]
[RequireComponent(typeof(LEAnimatorManager))]
[RequireComponent(typeof(LERotationManager))]
[RequireComponent(typeof(LETransiationManager))]
[RequireComponent(typeof(LEIdentification))]
[RequireComponent(typeof(LEPhysicsManager))]
public abstract class LEMainBase : MonoBehaviour {

    protected LEInputableObjectManager inputableObjectManager;
    protected LERotationManager rotationManager;
    protected LEAnimatorManager animationManager;
    protected LETransiationManager transitionManager;

    protected bool enableBaiscMovement = true;

    protected bool alive = true;
    public bool Alive { get { return alive; } }
    public bool IsAlive() { return alive; }

    StateMachineBase stateMachine;

    protected virtual void OnEnable()
    {
        inputableObjectManager = GetComponent<LEInputableObjectManager>();
        rotationManager = GetComponent<LERotationManager>();
        transitionManager = GetComponent<LETransiationManager>();
        animationManager = GetComponent<LEAnimatorManager>();
    }

    public abstract void Pause(bool b);

    public void SetBasicMovement_ActiveStatu(bool value)
    {
        enableBaiscMovement = value;
    }

    public StateMachineBase StateMachine { get { if (stateMachine == null) { stateMachine = GetComponent<StateMachineBase>(); if (stateMachine != null) { stateMachine.leBase = this; stateMachine.transitionManager = transitionManager; stateMachine.animatorManager = animationManager; } }return stateMachine; } }

    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public abstract void Dispatch_Animation_Message(AnimationMessageType messageType,object messageValue);

    //Why this return bool ? 
    //Because we need to check if we Successfully eat a health potion or mana potion,
    //then we return true,if not,we return false...... 
    public virtual bool AddHealth(float addHealth) { return false; }
    public virtual bool AddMana(float addMana) { return false; }
    public virtual bool Damage(float number) { return false; }
    public virtual bool SlowDown(float percentage, float time) { return false; }
    public virtual bool Stun(float time) { return false; }
    

}
