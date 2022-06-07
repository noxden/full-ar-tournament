//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    //# Public Variables 
    public GameObjectType type;
    public string title;

    //# Private Variables 
    private Settings settings;
    private new Renderer renderer;
    private int lifepoints;

    //# Monobehaviour Events 
    private void Start()
    {
        settings = FindObjectOfType<Settings>();
        renderer = GetComponent<Renderer>();
        lifepoints = LifePointsFor(type);

        if (settings == null)
            Debug.LogWarning($"{title}: Missing settings reference.", this);
        else
            renderer.material = MaterialFor(type);            


        #region deprecated renderer check 
        // if (renderer != null && settings != null)
        // {
        //     
        // }
        // else
        // {
        //     if (renderer == null)
        //     {
        //         Debug.LogWarning($"{title}: Missing renderer reference.", this);
        //     }
        //     if (settings == null)
        //     {
        //         Debug.LogWarning($"{title}: Missing settings reference.", this);
        //     }
        // }
        #endregion deprecated
    }

    //# Public Methods 
    public int ModifyLifePoints(int amount)
    {
        lifepoints += amount;
        lifepoints = Mathf.Min(lifepoints, 0);
        return lifepoints;  //< Returns new lifepoints value, so that it could be used in checks from the attacker's side
    }

    //# Private Methods 
    private int LifePointsFor(GameObjectType _type)
    {
        switch (_type)
        {
            case GameObjectType.Player:
                return settings.playerLifePoints;
            case GameObjectType.Enemy:
                return settings.enemyLifePoints;
            default:
                Debug.LogWarning($"Cube.LifePointsFor: \"{_type}\" does not match any entry in enum (GameObjectType), returning 0.");
                return 0;
        }
    }

    private Material MaterialFor(GameObjectType _type)
    {
        switch (_type)
        {
            case GameObjectType.Player:
                return settings.playerMaterial;
            case GameObjectType.Enemy:
                return settings.enemyMaterial;
            default:
                Debug.LogWarning($"Cube.LifePointsFor: \"{_type}\" does not match any entry in enum (GameObjectType), returning 0.");
                return settings.errorMaterial;
        }
    }
}
