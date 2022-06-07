//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Material playerMaterial;
    public Material enemyMaterial;
    public Material errorMaterial;

    public int playerLifePoints;
    public int enemyLifePoints;

    public int spawnAmountPlayer = 2;
    public int spawnAmountEnemy = 2;
    [Range(0f, 50f)] public int spawnRadius = 10;
}
