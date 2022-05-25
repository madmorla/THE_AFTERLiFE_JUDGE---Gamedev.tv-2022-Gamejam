using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	[Header("Movement")]
	public float speed = 1.0f;

	[Header("Colors")]
	[SerializeField] Color soulColor = Color.blue;
	[SerializeField] Color interiorSoulColor = Color.blue;

	private bool isMoving = false;
	private Transform target;

	Material soulBallMat;
	List<Material> particleSystemsMats;
	Light light;

	//------------------------------------
	// Unity methods

	private void Awake()
	{
		GetAllColorChangeReferences();
	}

	private void Start()
	{
		SetSoulColor(soulColor, interiorSoulColor);
	}

	private void SetSoulColor(Color color, Color interiorColor)
	{
		soulBallMat.SetColor("_Fresnel_Color", color);
		soulBallMat.SetColor("_InteriorColor", interiorColor);
		foreach(Material mat in particleSystemsMats)
		{
			mat.SetColor("_TintColor", color);
		}
		light.color = color;
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
		light = GetComponentInChildren<Light>();
	}

	public void MoveTowards(Transform target)
	{
		this.target = target;
		isMoving = true;
	}
}
