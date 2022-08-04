using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene { Menu, Combat }

public class SceneTransitionManager
{
    //# Public Variables 
    public static Delegate OnBeforeCombatLoad;
    public static Delegate OnAfterMenuLoad;
    public static Scene currentScene = Scene.Menu;  //< This is a little unclean, but the game should always start in the Menu anyways

    //# Public Methods 
    public static void LoadScene(Scene scene)
    {
        // if (scene == Scene.Combat)
        //     OnBeforeCombatLoad();

        SceneManager.LoadSceneAsync(ResolveSceneName(scene));
        currentScene = scene;

        // if (scene == Scene.Menu)
        //     OnAfterMenuLoad();
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
