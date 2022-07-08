//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 24-06-22
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
    //> I feel like all of these are unnecessary
    // public Monster user;
    // public Monster target    //< I don't think this is needed, as the action does not do anything with the information about the target.
    // {
    //     get { return target; }
    //     set { target = value; }
    // }
    public ElementalType type;
    public ActionCategory category;
    public TargetType targetType;
    public int powerPoints;
    public int basePower;
    public int baseAccuracy;
    public List<StatModification> StatModifications;
    public string description;

    //# Public Methods 
    public void use(Monster user, Monster enemy)
    {
        // Todo: -How do we get the enemy in here?- Is this a good way to get the enemy here?
        //#> Select targets based on targetType of this action 
        List<Monster> Targets = null;
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
            //#> Apply damage or healing 
            Debug.Log($"Action {name} has been used by {user}, targeting {target}.");
            // Deal damage or whatever this action does

            //#> Apply StatModifications, if there are any, on the target(s) 
            if (StatModifications.Count > 0)
            {
                foreach (StatModification modification in StatModifications)
                {
                    target.ApplyStatModification(modification);
                    // InvokeStatModification(modification, target);
                }
            }
        }
    }
    // public void InvokeStatModification(StatModification modification, Monster target)
    // {
        
    // }
}
