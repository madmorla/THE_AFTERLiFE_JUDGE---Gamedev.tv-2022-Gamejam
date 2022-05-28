using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;

	[Header("Camera Controllers")]
	[SerializeField] private GameObject mainCamera;
	[SerializeField] private GameObject menuCamera;

	[Header("UI Elements")]
	[SerializeField] private UnityEngine.UI.Button playButton;

	[Header("Music")]
	[SerializeField] private AudioSource mainMenuTrack;
	[SerializeField] private AudioSource gameplayTrack;

	Animator anim;

	private bool isMenuShowed = true;

	//---------------------------
	// Unity Methods

	private void Awake()
	{
		anim = GetComponent<Animator>();
		gameManager.onGameFinished += FinishGame;
	}

	private void Start()
	{
		mainMenuTrack.Play();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isMenuShowed) 
			{
				HideMenu();
			}
			else
			{
				ShowMenu();
			}
		}
	}

	//---------------------------
	// Methods
	public void StartGame()
	{
		HideMenu();
		gameManager.NewGame();
	}
	private void FinishGame()
	{
		ShowMenu();

		// Show credits in scroll??
		// or in the right side of menu??  <-- This easier

		// show credits
	}

	public void ExitGame()
	{
		Application.Quit();
	}


	private void ShowMenu()
	{
		gameplayTrack.Stop();
		mainMenuTrack.Play();

		isMenuShowed = !isMenuShowed;
		Conveniences.Mouse.ToggleCursor(true);
		mainCamera.SetActive(false);
		menuCamera.SetActive(true);
		anim.SetTrigger("toggleMenu");
	}

	private void HideMenu()
	{
		mainMenuTrack.Stop();
		gameplayTrack.Play();

		isMenuShowed = !isMenuShowed;
		Conveniences.Mouse.ToggleCursor(false);
		mainCamera.SetActive(true);
		menuCamera.SetActive(false);
		anim.SetTrigger("toggleMenu");
	}

}
