using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Portal : MonoBehaviour
{
    [SerializeField] private VisualEffect portalVFX;
    [SerializeField] private Color soulColor = Color.white;
    [SerializeField] private Color interiorSoulColor = Color.white;

    private AudioSource audioSource;

	public Color SoulColor { get => soulColor; }
	public Color InteriorSoulColor { get => interiorSoulColor; }

	private void Awake()
	{
        audioSource = GetComponent<AudioSource>();
	}

    public void StopIfSoundPlaying()
	{
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlaySound()
	{
        audioSource.Play();
    }

    public void PlayPortalVFX()
    {
        portalVFX.Play();
    }

}
