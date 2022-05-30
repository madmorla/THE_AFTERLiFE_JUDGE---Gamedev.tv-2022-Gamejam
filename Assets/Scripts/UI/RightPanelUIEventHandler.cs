using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPanelUIEventHandler : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    void OnRightPanelAnimationEnds()
	{
		mainMenu.creditsButton.interactable = true;
	}
}
