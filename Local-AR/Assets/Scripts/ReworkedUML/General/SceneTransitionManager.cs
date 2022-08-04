using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene { Menu, Combat }

public class SceneTransitionManager
{
    //# Public Variables 

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
