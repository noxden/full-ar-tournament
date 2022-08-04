//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { Self, Enemy, Both }
public enum ActionCategory { Physical, Special, Status }

[CreateAssetMenu(fileName = "New Action", menuName = "Scriptable/Action")]
public class Action : ScriptableObject
{
    //# Public Variables 
    public new string name;
    public string description;
    public ElementalType type;
    public ActionCategory category;
    public TargetType targetType;
    public int powerPoints;
    public int basePower;
    public int baseAccuracy;
    public int speedBonus;
    public List<StatModification> StatModifications;

    //# Public Methods 
    public void Use(Monster user, Monster enemy)
    {
        //#> Select targets based on targetType of this action 
        List<Monster> Targets = new List<Monster>();
        switch (targetType)
        {
            case TargetType.Self:
                Targets.Add(user);
                break;
            case TargetType.Enemy:
                Targets.Add(enemy);
                break;
            case TargetType.Both:
                Targets.Add(user);
                Targets.Add(enemy);
                break;
        }

        foreach (Monster target in Targets)
        {
            Debug.Log($"<color=#FFFF00>Action.Use: Action \"{name}\" has been used by {user.GetName()}, targeting {target.GetName()}.</color>", this);

            if (category != ActionCategory.Status)
            {
                //#> Apply damage or healing 
                int userPower = 0;
                int targetDefense = 0;
                if (category == ActionCategory.Physical)
                {
                    userPower = user.attack;
                    targetDefense = target.defense;
                }
                else if (category == ActionCategory.Special)
                {
                    userPower = user.specialAttack;
                    targetDefense = target.specialDefense;
                }

                int damage = (int)Mathf.Floor((((((2 * user.level) / 5) + 2) * basePower * (userPower / targetDefense)) / 50) + 2);  //< Mirrors the actual pokemon calculations

                //StatModification healthModification = new StatModification(Stat.HP, -damage);
                StatModification healthModification = (StatModification)ScriptableObject.CreateInstance(typeof(StatModification));  //< See https://stackoverflow.com/questions/988658/unable-to-cast-from-parent-class-to-child-class
                healthModification.stat = Stat.HP;
                healthModification.value = -damage;
                
                target.ApplyStatModification(healthModification);
                //Debug.Log($"Action.Use: {target.GetName()}'s {healthModification.stat} has been reduced by {damage}.");
            }

            //#> Apply StatModifications, if there are any, on the target(s) 
            if (StatModifications.Count > 0)
            {
                foreach (StatModification modification in StatModifications)
                {
                    target.ApplyStatModification(modification);
                }
            }
        }
    }

    public int GetLibraryIndex()
    {
        int indexInLibrary = GameManager.Instance.ActionLibrary.FindIndex(m => m == this);
        return indexInLibrary;
    }
}
