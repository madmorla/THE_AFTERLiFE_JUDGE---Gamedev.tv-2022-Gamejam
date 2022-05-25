using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	[Header("Movement")]
	public float speed = 1.0f;

	[Header("Colors")]
	[SerializeField] Color soulColor;
	[SerializeField] Color hellColor;
	[SerializeField] Color heavenColor;

	[SerializeField]private Transform target;
	private bool isMoving = false;

	private void Start()
	{
		
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

	public void MoveTowards(Transform target)
	{
		this.target = target;
		isMoving = true;
	}
}
