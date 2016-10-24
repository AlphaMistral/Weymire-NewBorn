using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LensPack : MonoBehaviour 
{
	/// <summary>
	/// The singleton instance of the LensPack. 
	/// </summary>
	public static LensPack instance;

	/// <summary>
	/// The PhotoCamera compoenent of the main Camera.
	/// </summary>
	public PhotoCamera photoCamera;

	/// <summary>
	/// The 
	/// </summary>
	public UISprite backGroundPNG;

	/// <summary>
	/// The equipped lens item. 
	/// </summary>
	public LensItem equippedLens;

	//It is still controversial whether we should use a generic list or simply four different variables. 

	private void Awake ()
	{
		
	}

	/// <summary>
	/// Switch to the indicated lens.
	/// </summary>
	/// <param name="lens">Lens.</param>
	public void SwitchMode(LensItem lens)
	{
		
	}

	/// <summary>
	/// When the lensPack is invoked, highlight the selected mode and play the corresponding tweens. 
	/// </summary>
	private void OnInvoke ()
	{
		
	}

	/// <summary>
	/// When the lensPacks is disabled, remove the faden sprite and play back the Animations .
	/// </summary>
	private void OnHide ()
	{
		
	}

	/// <summary>
	/// Invokes the and disable routine, based on the queries on the event. 
	/// </summary>
	/// <returns>The and disable routine.</returns>
	/// <param name="state">If set to <c>true</c> state.</param>
	private IEnumerator InvokeAndDisableRoutine (bool state)
	{
		yield return StartCoroutine(PlayTweenAnimation (state));
		yield return StartCoroutine(HighlightLens (state));
	}

	/// <summary>
	/// Play the tween part.
	/// </summary>
	private IEnumerator PlayTweenAnimation (bool state)
	{
		backGroundPNG.gameObject.SetActive(true);
		yield return null;
	}

	/// <summary>
	/// Play the highlight part. 
	/// </summary>
	private IEnumerator HighlightLens (bool state)
	{
		yield return null;
	}

	public static void Initialize ()
	{
		
	}
}
