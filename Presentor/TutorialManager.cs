using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	private static TutorialManager instance;

	private void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
			DestroyImmediate(gameObject);
	}

	/// <summary>
	/// Invoke a tutorial based on the corresponding index;
	/// </summary>
	/// <param name="index">Index.</param>
	public void InvokeTutorial (int index)
	{
		
	}
		
	/// <summary>
	/// Loads the tutorWindow resource.
	/// </summary>
	/// <returns>The resource.</returns>
	/// <param name="index">Index.</param>
	private IEnumerator LoadResource (int index)
	{
		/// Please note that this time I don't differentiate editor and iOS release. 
		/// Since alternating to loading directly is pretty fucky ... 
		ResourceRequest state = Resources.LoadAsync(Consts.VariableName.tutorialName + index, typeof (GameObject));
		yield return state;
		GameObject tutorWindow = state.asset as GameObject;
	}
}
