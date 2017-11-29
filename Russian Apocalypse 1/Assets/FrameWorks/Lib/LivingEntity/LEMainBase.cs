using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[RequireComponent(typeof(LEIECompositer))]
[RequireComponent(typeof(LEAnimatorManager))]
[RequireComponent(typeof(LERotationManager))]
[RequireComponent(typeof(LETransiationManager))]
[RequireComponent(typeof(LEIdentification))]
[RequireComponent(typeof(LEPhysicsManager))]
public abstract class LEMainBase : MonoBehaviour {

    public ProcessType processType = ProcessType.Command_Input;

    protected LEIECompositer inputableObjectManager;

    public List<SubManager> subManagers = new List<SubManager>();
    public LERotationManager rotationManager;
    public LEAnimatorManager animatorManager;
    public LETransiationManager transitionManager;

    InputManager inputManager;

    protected bool enableBaiscMovement = true;

    protected bool alive = true;
    public bool Alive { get { return alive; } }
    public bool IsAlive() { return alive; }

    StateMachineBase stateMachine;

    bool init = false;

    protected void Start()
    {
        if (!init)
        {
            Init();
        }
    }

    private void Update()
    {
        

        if (processType == ProcessType.MouseKey_Input)
        {
            foreach (SubManager submanager in subManagers)
            {
                inputManager.UpdateInput();
                submanager.UpdateManager(inputManager.INPUTDATA);
            }
        }
        else
        {

        }
    }

    void Init()
    {
        inputManager = GetComponent<InputManager>();

        rotationManager = GetComponent<LERotationManager>();
        subManagers.Add(rotationManager);

        animatorManager = GetComponent<LEAnimatorManager>();
        subManagers.Add(animatorManager);

        transitionManager = GetComponent<LETransiationManager>();
        subManagers.Add(transitionManager);

        init = true;
    }

    public abstract void Pause(bool b);

    public void SetBasicMovement_ActiveStatu(bool value)
    {
        enableBaiscMovement = value;
    }

    public StateMachineBase StateMachine { get { if (stateMachine == null) { stateMachine = GetComponent<StateMachineBase>(); if (!init) Init(); }return stateMachine; } }

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

    public enum ProcessType
    {
        MouseKey_Input,
        Command_Input
    }
}

