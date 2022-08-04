//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Package
{
    //# Public variables 
    public enum PackageType { JoinPackage, CombatPackage, /*MonsterSwap*/ }

    public PackageType packageType;
}
