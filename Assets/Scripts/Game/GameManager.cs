using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoulSpawner soulSpawner;
    [SerializeField] private Scroll scroll;

	[Header("Portals")]
	[SerializeField] private Portal hellPortal;
	[SerializeField] private Portal heavenPortal;

	[Header("Options")]
	[SerializeField] private bool createRandomQueue = true;

    [SerializeField] private SoulDataSO[] soulDataList;

	private Queue<SoulDataSO> soulsDataQueue;

    private Soul currentSoul;

	private event Action onSoulPassPortal;

	//------------------------------------
	// Methods

	// Gameflow

	//--------
	//Loop until finish list of souls

	// 1- Spawn a 'random' soul
	//  1.1- Set SoulData in scroll
	//  1.2- Spawn animation soul???

	// 2- Push a button
	// NOTE: Player has to read the scroll to push buttons????

	// 3- Soul Move to portal
	//  3.1- Change color depending portal choosed
	//  3.2- Moving like a ghost??? make some curves or something like that?? change the shader??

	// 4- Destroy the soul and spawn new one (Maybe pooling?)
	//--------

	// 5- Finish the game with the last soul replace you??

	private void Start()
	{
		NewGame();
	}

	//---------------------------
	// Methods

	public void NewGame()
    {
		if(currentSoul)
		{
            Destroy(currentSoul.gameObject);
            currentSoul = null;
		}

		if(createRandomQueue)
		{
			CreateRandomSoulQueue();
		}
		else
		{
			CreateSequenceSoulQueue();
		}

		SpawnNextSoul();
	}

	private void CreateSequenceSoulQueue()
	{
		if(soulsDataQueue != null)	{ soulsDataQueue.Clear(); }
		soulsDataQueue = new Queue<SoulDataSO>();

		for(int i = 0; i < soulDataList.Length; i++)
		{
			soulsDataQueue.Enqueue(soulDataList[i]);
		}
	}

	private void CreateRandomSoulQueue()
    {
		if(soulsDataQueue != null) { soulsDataQueue.Clear(); }
        soulsDataQueue = new Queue<SoulDataSO>();

        List<int> indicesList = new List<int>();
        List<SoulDataSO> randomIndicesList = new List<SoulDataSO>();
		for(int i = 0; i < soulDataList.Length; i++)
		{
            indicesList.Add(i);
        }

		while(indicesList.Count > 0)
		{
            int randomIndex = UnityEngine.Random.Range(0, indicesList.Count);
			soulsDataQueue.Enqueue(soulDataList[indicesList[randomIndex]]);
            indicesList.RemoveAt(randomIndex);
        }
	}

	public bool SpawnNextSoul()
	{
		if(soulsDataQueue.Count <= 0)
		{ return false; }

		scroll.SetSoulData(soulsDataQueue.Dequeue());
		currentSoul = soulSpawner.SpawnSoul();
		currentSoul.onReachDestination += SoulPassThroughThePortal;

		return true;
	}

	public void SoulMoveToHeavenPortal()
	{
		hellPortal.StopIfSoundPlaying();
		currentSoul.MoveTowards(heavenPortal.transform);
		currentSoul.SetSoulColor(heavenPortal.SoulColor, heavenPortal.InteriorSoulColor);
		onSoulPassPortal += heavenPortal.PlaySound;
		onSoulPassPortal += heavenPortal.PlayPortalVFX;
	}

	public void SoulMoveToHellPortal()
	{
		heavenPortal.StopIfSoundPlaying();
		currentSoul.MoveTowards(hellPortal.transform);
		currentSoul.SetSoulColor(hellPortal.SoulColor, hellPortal.InteriorSoulColor);
		onSoulPassPortal += hellPortal.PlaySound;
		onSoulPassPortal += hellPortal.PlayPortalVFX;
	}

	//---------------------------
	// Event Methods

	// Event triggered when soul pass through the portal
	public void SoulPassThroughThePortal()
	{
		//Debug.Log("onReachDestination");
		onSoulPassPortal?.Invoke();
		onSoulPassPortal = null;

		Destroy(currentSoul.gameObject);
		currentSoul = null;

		if(!SpawnNextSoul())
		{
			Debug.Log("FINISHED");
		}
	}

}
