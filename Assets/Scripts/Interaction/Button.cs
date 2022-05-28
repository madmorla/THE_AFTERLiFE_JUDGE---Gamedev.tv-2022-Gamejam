using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IRaycastable
{
    [SerializeField] private float sfx_delay = 0f;
    public UnityEvent onPressedButton;

    private AudioSource audioSource;

    


	private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool HandleRaycast(PlayerController callingController)
    {
        // Press the button with mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            // Play audio if have it
            if (audioSource)
            {
                audioSource.PlayDelayed(sfx_delay);
            }
            // Invoke the funtions attached in inspector
            onPressedButton?.Invoke();
        }

        return true;
    }
}
