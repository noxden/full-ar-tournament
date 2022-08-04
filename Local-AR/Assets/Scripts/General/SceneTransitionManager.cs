//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 03-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene { Menu, Combat }

public class SceneTransitionManager
{
    //# Public Methods 
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadSceneAsync(ResolveSceneName(scene));
    }

    //# Private Methods 
    private static string ResolveSceneName(Scene scene)
    {
        switch (scene)
        {
            case Scene.Menu:
                return "MenuScene";
            case Scene.Combat:
                return "CombatScene";
        }
        return "";
    }
}
