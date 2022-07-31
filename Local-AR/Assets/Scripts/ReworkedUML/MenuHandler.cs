//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 30-07-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuName { Tutorial, Home, ChangeMonsters, Credits, Lobby, Combat, PostCombat }

public class MenuHandler : MonoBehaviour
{
    //# Public Variables 
    public Scene isInScene;
    public static MenuName currentMenu;

    //# Private Variables 
    [SerializeField] private List<Canvas> Menus;

    //# Monobehaviour Events 
    private void Start()
    {
        //> Populate list "Menus" with all Canvases from GameObjects tagged with "Menu".
        GameObject[] MenuGameObjects = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject entry in MenuGameObjects)
        {
            Menus.Add(entry.GetComponent<Canvas>());
        }
        if (Menus.Count == 0) Debug.LogWarning($"MenuHandler: The Menus list is empty. Populate it before starting the game!", this);

        //> On scene start set a menu depending on in which scene this menuhandler is and depending on if the tutorial was shown already.
        if (isInScene == Scene.Menu)
        {
            if (SaveDataManager.didShowTutorial == false)
                SwitchToMenu(MenuName.Tutorial);
            else
                SwitchToMenu(MenuName.Home);
        }
        else if (isInScene == Scene.Combat)
            SwitchToMenu(MenuName.Lobby);   //< Is executed when in CombatScene.

    }

    //# Public Methods 
    public Canvas SwitchToMenu(MenuName _name)
    {
        //> Set flag if the current menu was the tutorial
        if (currentMenu == MenuName.Tutorial && SaveDataManager.didShowTutorial == false)
            SaveDataManager.didShowTutorial = true;

        //> Switch to new menu from the list and save it in currentMenu
        Canvas newMenu;
        newMenu = Menus.Find(m => m.GetComponent<CanvasMenu>().name == _name);
        if (newMenu == null)
        {
            Debug.LogError($"MenuHandler: Could not find menu \"{_name}\" in this scene. Please make sure to set it properly before starting the game! ERROR_MH1", this);
            return null;
        }

        foreach (var entry in Menus)
        {
            ToggleVisibility(entry, false);
        }
        ToggleVisibility(newMenu, true);

        currentMenu = newMenu.GetComponent<CanvasMenu>().name;
        return newMenu;
    }

    //# Private Methods 
    private void ToggleVisibility(Canvas targetCanvas, bool visibility)
    {
        CanvasGroup canvasGroup = targetCanvas.GetComponent<CanvasGroup>();
        switch (visibility)
        {
            case true:
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                break;
            case false:
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                break;
        }
    }
}
