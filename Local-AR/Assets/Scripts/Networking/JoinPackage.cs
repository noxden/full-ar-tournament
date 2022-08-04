//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinPackage : Package
{
    //# Constructors 
    public JoinPackage(Player playerData)
    {
        packageType = PackageType.JoinPackage;
        username = playerData.username;
        Monsters = new List<Monster>(playerData.Monsters);
        //< Just to make sure that no references get messed up, monsterOnField is left blank in this transmission to be filled by CombatHandler upon receiving the package.
    }

    //# Public Variables 
    public string username;
    public List<Monster> Monsters;
}
