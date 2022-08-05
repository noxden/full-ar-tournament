//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144) & Jan Alexander
// Last changed: 04-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JoinPackage
{
    //# Public Variables 
    public string packageType = "JoinPackage";
    public int packageAuthorUUID;
    public string username;
    public List<int> MonsterDataIndexList;

    //# Constructors 
    public JoinPackage(int UUID, Player playerData)
    {
        packageAuthorUUID = UUID;

        username = playerData.username;
        MonsterDataIndexList = new List<int>();
        foreach (MonsterData entry in playerData.MonsterDataList)
        {
            int monsterDataIndex = GameManager.Instance.GetLibraryIndexOfMonster(entry);
            MonsterDataIndexList.Add(monsterDataIndex);
        }
        //< Just to make sure that no references get messed up, monsterOnField is left blank in this transmission to be filled by CombatHandler upon receiving the package.
    }
}