using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IRaycastable
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float sfx_delay = 0f;

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string animTriggerWord = "";

    [Header("Emission color and light")]
    [SerializeField] private Renderer rend;
    [SerializeField] private Light emissionLight;
    private Material mat;
    private Color emissionColor;


    public UnityEvent onPressedButton;

    private void Awake()
    {
		if(!audioSource)
		{
            audioSource = GetComponent<AudioSource>();
		}

		if(rend)
		{
            mat = rend.material;
            emissionColor = mat.GetColor("_EmissionColor");
            emissionLight.color = emissionColor;
        }
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

            // Animate if have it
            if(anim)
            {
                anim.SetTrigger(animTriggerWord);
            }

			// Enable emission it have it
			if(rend)
			{
                SetEmission(true);
			}

            // Invoke the funtions attached in inspector
            onPressedButton?.Invoke();
        }

        return true;
    }

    private void SetEmission(bool enabled)
	{
		if(enabled)
		{
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", emissionColor);
		}
		else
		{
            mat.SetColor("_EmissionColor", Color.black);
        }

        emissionLight.gameObject.SetActive(enabled);
    }

    // Animation Event if added
    public void OnAnimationEnd()
	{
        SetEmission(false);
    }

}
