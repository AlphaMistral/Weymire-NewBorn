using UnityEngine;
using System.Collections;

/// <summary>
/// Initializer Serves as the game Initializer when changing scenes. It applies the saved data to the game. 
/// It also Creates all neecssary gameData when the game is reset. 
/// </summary>
public class Initializer : MonoBehaviour 
{
	#region Private Attributes

	private GameObject playerGameObject;

	#endregion

	#region Private static constants

	/// <summary>
	/// The time interal after which the Initializer will start working. 
	/// Avoid Possible Script Execution Delay ... No need to change in Project Settings. 
	/// </summary>
	private static float loadTime = 0.2f;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		GetReferences();
	}

	private void Start ()
	{
		if (PlayerPrefsX.GetBool(SaveVarName.IsGameDataAvailable))
			StartCoroutine(Load());
		else
		{
			StartCoroutine(Create());
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// A Save Data is found. Hence apply it to all the other components in the game. 
	/// </summary>
	private IEnumerator Load ()
	{
		yield return new WaitForSeconds(loadTime);
		PlayerInventory.Initialize ();
		BackPack.Initialize();
		LensPack.Initialize();
		yield return StartCoroutine(UIController.FadeInOut (FadeOperation.FadeOut));
	}

	/// <summary>
	/// The Save Data is not Found, or the player decides to play a new turn. Create the variables to avoid a possible Object Reference Bla Bla Bla ... 
	/// </summary>
	private IEnumerator Create ()
	{
		yield return new WaitForEndOfFrame();
		InfoSaver.CreateNewSaveData();
		yield return StartCoroutine(UIController.FadeInOut (FadeOperation.FadeOut));
	}

	private void GetReferences ()
	{
		playerGameObject = GameObject.FindGameObjectWithTag(Tags.Player);
	}

	#endregion
}
