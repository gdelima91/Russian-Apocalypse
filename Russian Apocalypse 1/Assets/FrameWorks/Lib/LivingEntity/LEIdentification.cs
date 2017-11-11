﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LEIdentification : MonoBehaviour
{
    const string Non = "Non";

    [SerializeField]private string union = Non;

    public LEType letype = LEType.AI;

    
}

public enum LEType
{
    AI,
    Player
}
