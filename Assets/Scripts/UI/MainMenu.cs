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
	public UnityEngine.UI.Button startGameButton;
	public UnityEngine.UI.Button resumeGameButton;
	public UnityEngine.UI.Button creditsButton;
	[SerializeField] private GameObject crosshair;

	[Header("Music")]
	[SerializeField] private AudioSource mainMenuTrack;
	[SerializeField] private AudioSource gameplayTrack;

	[SerializeField] private Animator leftPanelAnim;
	[SerializeField] private Animator rightPanelAnim;
	[SerializeField] private Animator creditsPanelAnim;
	[SerializeField] private Animator storyPanelAnim;

	private bool isMenuShowed = true;
	private bool isAnimationEnds = true;

	//---------------------------
	// Unity Methods

	private void Awake()
	{
		gameManager.onGameFinished += FinishGame;
	}

	private void Start()
	{
		mainMenuTrack.Play();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q) && gameManager.IsGameStarted && isAnimationEnds)
		{
			EnableInteraction(false);

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
		isAnimationEnds = false;

		startGameButton.gameObject.SetActive(false);
		resumeGameButton.gameObject.SetActive(true);

		HideMenuToStory();
		ShowStoryPanel(true);
	}

	public void BeginJudge()
	{
		ShowStoryPanel(false);
		StartGameActions();
		gameManager.NewGame();
	}

	private void FinishGame()
	{
		startGameButton.gameObject.SetActive(true);
		resumeGameButton.gameObject.SetActive(false);

		ShowMenu();

		//Show Finish Panel and score?
		ToggleCredits(); // then Finish
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

		crosshair.SetActive(false);
		Conveniences.Mouse.ToggleCursor(true);
		mainCamera.SetActive(false);
		menuCamera.SetActive(true);
		leftPanelAnim.SetBool("hide", false);
	}

	private void HideMenu()
	{
		StartGameActions();
		HideMenuToStory();
	}

	private void StartGameActions()
	{
		mainMenuTrack.Stop();
		gameplayTrack.Play();

		isMenuShowed = !isMenuShowed;

		crosshair.SetActive(true);
		Conveniences.Mouse.ToggleCursor(false);
		mainCamera.SetActive(true);
		menuCamera.SetActive(false);
	}

	private void HideMenuToStory()
	{
		leftPanelAnim.SetBool("hide", true);
		if(rightPanelAnim.GetBool("show"))
		{
			rightPanelAnim.SetBool("show", false);
			creditsPanelAnim.SetBool("move", false);
		}
	}

	private void ShowStoryPanel(bool value)
	{
		storyPanelAnim.SetBool("show", value);
	}

	//---------------------------
	// Button Events

	public void ToggleCredits()
	{
		creditsButton.interactable = false;
		if(rightPanelAnim.GetBool("show"))
		{
			rightPanelAnim.SetBool("show", false);
			creditsPanelAnim.SetBool("move", false);
		}
		else
		{
			rightPanelAnim.SetBool("show", true);
			creditsPanelAnim.SetBool("move", true);
		}
	}

	public void ResumeGame()
	{
		HideMenu();
	}

	//---------------------------
	// Animation Events

	public void OnHideMenuAnimationEnd()
	{
		EnableInteraction(true);
	}

	private void EnableInteraction(bool value)
	{
		gameManager.PlayerController.CanInteract = value;
		isAnimationEnds = value;
	}
}
