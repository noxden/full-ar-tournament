//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR
// Script by:    Daniel Heilmann (771144)
// Last changed: 04-06-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class GameManager : MonoBehaviour
{
    //# Public Variables 
    public static GameManager Instance { set; get; }

    //# Private Variables 
    private Settings settings;
    public List<GameObject> players;
    public List<GameObject> enemies;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //< Referring to the gameObject, this singleton script (class) is attached to.
        }
        else
        {
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.
        }
    }

    // Old version of "spawnPrefabOfType", which named the GameObject in that function
    // private void Start()
    // {
    //     //> Linking up references
    //     settings = FindObjectOfType<Settings>();

    //     //> Other
    //     int spawnAmount = settings.spawnAmount;
    //     for (int i = 0; i < spawnAmount; i++)
    //     {
    //         spawnPrefabOfType(GameObjectType.Player, i + 1);
    //         spawnPrefabOfType(GameObjectType.Enemy, i + 1);
    //     }
    // }

    // //# Public Methods 

    // //# Private Methods 
    // private void spawnPrefabOfType(GameObjectType _type, int _instanceNumber)   //< Instantiates prefab and adds it to the players or enemies list accordingly.
    // {
    //     GameObject playerPrefab = settings.playerPrefab;
    //     GameObject enemyPrefab = settings.enemyPrefab;

    //     int spawnRadius = settings.spawnRadius;
    //     Vector3 spawnPosition = new Vector3(calcRandomInt(-spawnRadius, spawnRadius), calcRandomInt(-spawnRadius, spawnRadius), calcRandomInt(-spawnRadius, spawnRadius));

    //     GameObject NewCube;
    //     switch (_type)
    //     {
    //         case GameObjectType.Player:
    //             NewCube = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    //             players.Add(NewCube);
    //             break;
    //         case GameObjectType.Enemy:
    //             NewCube = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //             enemies.Add(NewCube);
    //             break;
    //         default:
    //             Debug.LogWarning($"GameManager.spawnPrefab: \"{_type}\" does not match any entry in enum (GameObjectType).");
    //             return;
    //     }
    //     NewCube.GetComponent<Cube>().title = $"{_type} {_instanceNumber}";
    //     NewCube.name = $"{_type} {_instanceNumber}";
    // }

    private void Start()
    {
        //> Linking up references
        settings = FindObjectOfType<Settings>();

        //> Spawn Players and Enemies
        PopulateScene();
    }

    //# Private Methods 
    private void PopulateScene()
    {
        GameObject newCube;
        for (int i = 0; i < settings.spawnAmountPlayer; i++)
        {
            //> Spawn 1 player
            //setCubeName(GameObjectType.Player, i, spawnPrefabOfType(GameObjectType.Player));  //< Too unreadable
            newCube = spawnPrefabOfType(GameObjectType.Player);
            setCubeName(GameObjectType.Player, i, newCube);
        }
        for (int i = 0; i < settings.spawnAmountEnemy; i++)
        {
            //> Spawn 1 enemy
            newCube = spawnPrefabOfType(GameObjectType.Enemy);
            setCubeName(GameObjectType.Enemy, i, newCube);
        }
    }

    private GameObject spawnPrefabOfType(GameObjectType _type)   //< Instantiates prefab, adds it to the players or enemies list accordingly and also returns that GameObject.
    {
        //> Generate random location
        int spawnRadius = settings.spawnRadius;
        Vector3 spawnPosition = new Vector3(randomFloat(-spawnRadius, spawnRadius), randomFloat(-spawnRadius, spawnRadius), randomFloat(-spawnRadius, spawnRadius));

        // Decide, which type of prefab to spawn
        GameObject cubePrefab;
        GameObject newCube;
        switch (_type)
        {
            case GameObjectType.Player:
                cubePrefab = settings.playerPrefab;
                newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                players.Add(newCube);
                break;
            case GameObjectType.Enemy:
                cubePrefab = settings.enemyPrefab;
                newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);  // Due to this line being a double, it might become a potential breaking point
                enemies.Add(newCube);
                break;
            default:
                Debug.LogWarning($"GameManager.spawnPrefab: \"{_type}\" does not match any entry in enum (GameObjectType).");
                return null;
        }
        return newCube;
    }

    private void setCubeName(GameObjectType _type, int cubeIndex, GameObject _target)
    {
        string cubeName;

        switch (_type)
        {
            case GameObjectType.Player:
                cubeName = $"{GameObjectType.Player} {cubeIndex + 1}";
                break;
            case GameObjectType.Enemy:
                cubeName = $"{GameObjectType.Enemy} {cubeIndex + 1}";
                break;
            default:
                cubeName = "";
                Debug.LogWarning($"GameManager.spawnPrefab: \"{_type}\" does not match any entry in enum (GameObjectType).");
                break;
        }
        _target.GetComponent<Cube>().title = cubeName;
        _target.name = cubeName;
    }

    private int randomInt(float _min, float _max)
    {
        return (int)Random.Range(_min, _max);
    }

    private float randomFloat(float _min, float _max)
    {
        return Random.Range(_min, _max);
    }

    //# Input Event Handlers 
    public void OnTap(LeanFinger finger)
    {
        //> Create raycast
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log($"Raycast returned {hit.collider.gameObject.name}");
            GameObject hitGameObject = hit.collider.gameObject;
            Cube hitCubeObject = hitGameObject.GetComponent<Cube>();

            if (hitCubeObject != null)
            {
                hitCubeObject.SwitchMaterial();
            }
            else
            {
                Debug.Log($"Hit GameObject did not have a Cube component.", this);
            }
        }
        else
        {
            Debug.Log($"Raycast returned no results.", this);
        }
    }

    public void OnRepopulate()
    {
        //> Delete all player GameObjects
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
        players.Clear();

        //> Delete all enemy GameObjects
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();

        //> Populate the scene again
        PopulateScene();
    }
}
