using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class CanvasMenu : MonoBehaviour
{
    //# Public Methods 
    public new MenuName name;

    //# Monobehaviour Events 
    private void Awake()
    {
        if (this.gameObject.tag != "Menu")
            this.gameObject.tag = "Menu";
    }
}
