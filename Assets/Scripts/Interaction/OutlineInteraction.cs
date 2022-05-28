using UnityEngine;

public class OutlineInteraction : MonoBehaviour
{
    [SerializeField] private Outline outline;
    public Outline OutlineProp
    {
        get
        {
            if(!outline)
            { outline = GetComponentInChildren<Outline>(); }
            return outline;
        }
    }

	private void Awake()
	{
        if(!OutlineProp) { return; }
        OutlineProp.enabled = false;
	}

	void OnMouseEnter()
    {
		if(!OutlineProp) { return; }
        OutlineProp.enabled = true;
    }

    void OnMouseExit()
    {
        if(!OutlineProp){ return; }
        OutlineProp.enabled = false;
    }
}