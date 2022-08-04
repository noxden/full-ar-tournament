//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CombatPackage
{
    //# Public Variables 
    public static string packageType = "CombatPackage";
    public Action action;
    public float tieBreaker;

    //# Constructors 
    public CombatPackage(Action actionData, float tieBreakerData)     //< Does not require user and opponent as for the actual UseAction, the CombatHandler can decide those.
    {
        action = actionData;
        tieBreaker = tieBreakerData;
    }
}
