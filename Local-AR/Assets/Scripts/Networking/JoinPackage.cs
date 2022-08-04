//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JoinPackage
{
    //# Public Variables 
    public static string packageType = "JoinPackage";
    public string username;
    public List<Monster> Monsters;

    //# Constructors 
    public JoinPackage(Player playerData)
    {
        username = playerData.username;
        Monsters = new List<Monster>(playerData.Monsters);
        //< Just to make sure that no references get messed up, monsterOnField is left blank in this transmission to be filled by CombatHandler upon receiving the package.
    }
}