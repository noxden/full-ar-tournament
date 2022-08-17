//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 17-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public delegate void PlayerDelegate(Player player);

public class Player : MonoBehaviour
{
    //# Public Variables 
    public static PlayerDelegate OnMonsterOnFieldSwapped;
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
            GameManager.QueueFlavourText($"{username} sent out {newMonster.name}!", this);
        else
            GameManager.QueueFlavourText($"{username} swapped out {monsterOnField.name} with {newMonster.name}!", this);

        monsterOnField = newMonster;
        OnMonsterOnFieldSwapped(this);
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

    private List<Monster> InstantiateMonstersFromData(List<MonsterData> _MonsterDataList)
    {
        List<Monster> InstantiatedMonsters = new List<Monster>();
        foreach (MonsterData monsterData in _MonsterDataList)
        {
            GameObject monsterGameObject = Instantiate(genericMonsterPrefab, -Vector3.zero, Quaternion.identity);
            monsterGameObject.name = $"{monsterData.name}";
            Monster monster = monsterGameObject.GetComponent<Monster>();
            monster.LoadMonsterData(monsterData);
            InstantiatedMonsters.Add(monster);
        }
        return InstantiatedMonsters;
    }
}
