//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 17-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModelHandler : MonoBehaviour
{
    //# Public Variables 
    public Transform yourMonsterOrigin;
    public Transform enemyMonsterOrigin;

    //# Private Variables 
    [SerializeField] private Player yourPlayer;
    [SerializeField] private Player enemyPlayer;
    [SerializeField] private GameObject yourCurrentModel;
    [SerializeField] private GameObject enemyCurrentModel;

    //# Monobehaviour Events 
    //> Subscribe to & Unsubscribe from delegates
    private void OnEnable()
    {
        Player.OnMonsterOnFieldSwapped += UpdateModels;
        CombatHandler.MatchStart += OnMatchStart;
    }
    private void OnDisable()
    {
        Player.OnMonsterOnFieldSwapped -= UpdateModels;
        CombatHandler.MatchStart -= OnMatchStart;
    }

    //# Public Methods 

    //# Private Methods 
    private void OnMatchStart()
    {
        yourPlayer = CombatHandler.Instance.GetYourPlayer();
        enemyPlayer = CombatHandler.Instance.GetEnemyPlayer();
    }

    private void UpdateModels(Player owner)  //! Way too messy
    {
        GameObject newModel = owner.GetMonsterOnField().monsterData.modelPrefab;
        if (newModel == null)
            return;

        GameObject currentModel = null;
        Transform monsterOrigin = null;
        if (owner == CombatHandler.Instance.GetYourPlayer())
        {
            currentModel = yourCurrentModel;
            monsterOrigin = yourMonsterOrigin;
        }
        else if (owner == CombatHandler.Instance.GetEnemyPlayer())
        {
            currentModel = enemyCurrentModel;
            monsterOrigin = enemyMonsterOrigin;
        }
        else
            Debug.LogError($"Player {owner.username} matches neither your player ({CombatHandler.Instance.GetYourPlayer().username}), nor enemy player ({CombatHandler.Instance.GetEnemyPlayer().username}). Something must have gone wrong. ERROR_MMH1");

        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(newModel, monsterOrigin.position, monsterOrigin.rotation);
        currentModel.transform.SetParent(monsterOrigin);
        currentModel.name = $"Model of {owner.username}'s {owner.GetMonsterOnField().name}";

        if (owner == CombatHandler.Instance.GetYourPlayer())
            yourCurrentModel = currentModel;
        else if (owner == CombatHandler.Instance.GetEnemyPlayer())
            enemyCurrentModel = currentModel;
        else
            Debug.LogError($"Player {owner.username} matches neither your player ({CombatHandler.Instance.GetYourPlayer().username}), nor enemy player ({CombatHandler.Instance.GetEnemyPlayer().username}). Something must have gone wrong. ERROR_MMH1");
    }

    //# Input Event Handlers 
}
