using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private Transform spawnPoint;
    
    public Soul SpawnSoul()
    {
        GameObject soulInstance = Instantiate(soulPrefab, spawnPoint.position, Quaternion.identity);
        Soul soulComp = soulInstance.GetComponent<Soul>();
        return soulComp;
    }

    public void SpawnSoul_Button()
	{
        SpawnSoul();

    }
}
