//================================================================
// Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Local Multiplayer AR (by Jan Alexander)
// Script by:    Daniel Heilmann (771144)
// Last changed: 06-08-22
//================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuName { Tutorial, Home, ChangeMonsters, Credits, Lobby, Combat_Menu, Combat_Actions, Combat_Bag, Combat_Monsters, EndScreenWon, EndScreenLost, PermButtonToggleAR, PermButtonPlaceArena, PermHealthDisplay }

public class MenuHandler : MonoBehaviour
{
    //# Public Variables 
    public static MenuHandler Instance { set; get; }
    public static MenuName currentMenu;
    public MenuName startMenu;

    //# Private Variables 
    [SerializeField] private List<CanvasMenu> Menus;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance.
            Instance = this;
        else
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one.
    }
    private void Start()
    {
        //> Populate list "Menus" with all CanvasMenus in the Scene
        Menus = new List<CanvasMenu>(FindObjectsOfType<CanvasMenu>());
        if (Menus.Count == 0)
            Debug.LogWarning($"MenuHandler.Start: The \"Menus\" list is empty. Your menu canvases might not be set up correctly.");

        //> Replace MenuName.Tutorial in startMenu with MenuName.Home if the tutorial has already been displayed once
        if (startMenu == MenuName.Tutorial && SaveDataManager.didShowTutorial == true)
            startMenu = MenuName.Home;

        SwitchToMenu(startMenu);
    }

    //# Public Methods 
    public void SwitchToMenu(MenuName _name)
    {
        //> Turn off visibility of all canvases but the one that matches the input "_name" and also save that in currentMenu
        foreach (CanvasMenu canvasMenu in Menus)
        {
            if (canvasMenu.name == _name)        //< Could be shortened even more to -> canvasMenu.SetVisibility(canvasMenu.name == _name);
                canvasMenu.SetVisibility(true);
            else if (!canvasMenu.isPersistent)
                canvasMenu.SetVisibility(false);
        }
        currentMenu = _name;

        //> Set flag "didShowTutorial" if the new menu is the tutorial
        if (currentMenu == MenuName.Tutorial && SaveDataManager.didShowTutorial == false)
            SaveDataManager.didShowTutorial = true;

    }
    public void TogglePersistentMenu(MenuName _name)
    {
        foreach (CanvasMenu canvasMenu in Menus)
        {
            if (canvasMenu.name == _name)
                canvasMenu.SetVisibility(!canvasMenu.isVisible);
        }
    }

    public void TogglePersistentMenu(MenuName _name, bool visibility)
    {
        foreach (CanvasMenu canvasMenu in Menus)
        {
            if (canvasMenu.name == _name)
                canvasMenu.SetVisibility(visibility);
        }
    }
}