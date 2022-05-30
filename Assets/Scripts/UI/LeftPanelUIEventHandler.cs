using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelUIEventHandler : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    void OnAnimationEnds()
	{
		mainMenu.OnHideMenuAnimationEnd();
	}
}
