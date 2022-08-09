//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 08-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeavePackage
{
    //# Public Variables 
    public string packageType = "LeavePackage";
    public int packageAuthorUUID;

    //# Constructors 
    public LeavePackage(int UUID)
    {
        packageAuthorUUID = UUID;
    }
}