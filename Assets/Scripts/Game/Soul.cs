using System;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] public float speed = 1.0f;

	[Header("Colors")]
	[SerializeField] private Color soulColor = Color.blue;
	[SerializeField] private Color interiorSoulColor = Color.blue;

	[Header("VFX Effect")]
	[SerializeField] private GameObject onDieEffectPrefab;
	[SerializeField] private Vector3 vfxPosOffset;

	private bool isMoving = false;
	private Transform target;

	private Material soulBallMat;
	private List<Material> particleSystemsMats;
	private Light pointLight;

	public event Action onReachDestination;
	
	//------------------------------------
	// Unity methods

	private void Awake()
	{
		GetAllColorChangeReferences();
		onReachDestination += OnDieEffect;
	}

	private void Start()
	{
		SetSoulColor(soulColor, interiorSoulColor);
	}

	private void Update()
	{
		if(isMoving && target)
		{
			// Move our position a step closer to the target.
			var step = speed * Time.deltaTime; // calculate distance to move
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);

			// Check if the position of the cube and sphere are approximately equal.
			if(Vector3.Distance(transform.position, target.position) < 0.001f)
			{
				target = null;
				isMoving = false;

				onReachDestination?.Invoke();
			}
		}
	}

	private void OnDestroy()
	{
		foreach(Material mat in particleSystemsMats)
		{
			Destroy(mat);
		}
	}

	//------------------------------------
	// Methods

	// In Awake
	private void GetAllColorChangeReferences()
	{
		soulBallMat = transform.GetChild(0).GetComponent<Renderer>().material;

		particleSystemsMats = new List<Material>();
		ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem ps in particleSystems)
		{
			ParticleSystemRenderer rend = ps.GetComponent<ParticleSystemRenderer>();
			particleSystemsMats.Add(rend.material);
		}
		pointLight = GetComponentInChildren<Light>();
	}

	private void OnDieEffect()
	{
		if(onDieEffectPrefab)
		{
			GameObject dieEffect = Instantiate(onDieEffectPrefab, this.transform.position + vfxPosOffset, Quaternion.identity);
			ParticleSystem ps = dieEffect.GetComponent<ParticleSystem>();
			ParticleSystem.MainModule main = ps.main;
			main.startColor = soulColor;
		}
	}

	public void SetSoulColor(Color color, Color interiorColor)
	{
		soulBallMat.SetColor("_Fresnel_Color", color);
		soulBallMat.SetColor("_InteriorColor", interiorColor);
		foreach(Material mat in particleSystemsMats)
		{
			mat.SetColor("_TintColor", color);
		}
		pointLight.color = color;
	}

	public void MoveTowards(Transform target)
	{
		this.target = target;
		isMoving = true;
	}

	
}
