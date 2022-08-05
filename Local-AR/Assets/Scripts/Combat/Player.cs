//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //# Public Variables 
    public static Delegate OnMonsterOnFieldSwapped;
    public GameObject genericMonsterPrefab;
    public string username;
    public List<MonsterData> MonsterDataList;
    public int NumberOfMonsters { get { return Monsters.Count; } }

    //# Private Variables 
    [SerializeField] private List<Monster> Monsters;
    [SerializeField] private Monster monsterOnField; /*{ private set; get; }*/   //! Always set with SwapMonsterOnField()

    //# Constructors
    public void Set(Player player)
    {
        this.username = player.username;
        this.MonsterDataList = player.MonsterDataList;

        this.Monsters = InstantiateMonstersFromData(this.MonsterDataList);
        SwapMonsterOnField(GetFirstValidMonster());
    }

    public void Set(string _username, List<MonsterData> _MonsterDataList)
    {
        this.username = _username;
        this.MonsterDataList = _MonsterDataList;

        this.Monsters = InstantiateMonstersFromData(this.MonsterDataList);
        SwapMonsterOnField(GetFirstValidMonster());
    }

    //# Public Methods 
    public void SwapMonsterOnField(Monster newMonster)  //< Is used instead of standard set() to provide an interface for Monster's OnSwapped events, maybe?
    {
        if (monsterOnField == null)  //< Should only be the case when the battle just started or the former monsterOnField died
            Debug.Log($"{username} sent out {newMonster.GetName()}!");
        else
            Debug.Log($"{username} swapped out {monsterOnField.GetName()} with {newMonster.GetName()}!");

        monsterOnField = newMonster;
        OnMonsterOnFieldSwapped();
    }

    public Monster GetMonsterOnField()
    {
        return monsterOnField;
    }

    public Monster GetFirstValidMonster()
    {
        foreach (Monster monster in Monsters)
        {
            if (monster.isValid())
                return monster;
        }
        Debug.Log($"Player.GetFirstValidMonster: Cannot get any valid monster.", this);    //< Usually, if this happens, the player has lost / should lose the match.
        return null;
    }

    // TODO: Implement a monster stat reset function
    //# Private Methods 
    // private void FullyHealAllMonsters()     //! This still does not reset their modified stats!!
    // {
    //     // foreach (Monster monster in Monsters)
    //     //     monster.hpCurrent = monster.hpMax;
    // }

    private List<Monster> InstantiateMonstersFromData(List<MonsterData> _MonsterDataList)
    {
        List<Monster> InstantiatedMonsters = new List<Monster>();
        foreach (MonsterData monsterData in _MonsterDataList)
        {
            GameObject monsterGameObject = Instantiate(genericMonsterPrefab, -Vector3.zero, Quaternion.identity);
            monsterGameObject.name = $"{monsterData.GetName()}";
            Monster monster = monsterGameObject.GetComponent<Monster>();
            monster.LoadMonsterData(monsterData);
            InstantiatedMonsters.Add(monster);
        }
        return InstantiatedMonsters;
    }
}
