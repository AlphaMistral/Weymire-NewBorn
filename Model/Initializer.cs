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

	private IEnumerator Load ()
	{
		yield return new WaitForSeconds(loadTime);
		PlayerInventory.Initialize ();
		BackPack.Initialize();
		LensPack.Initialize();
		yield return StartCoroutine(UIController.FadeInOut ());
	}

	private IEnumerator Create ()
	{
		yield return new WaitForEndOfFrame();
		InfoSaver.CreateNewSaveData(playerGameObject.transform);
		yield return StartCoroutine(UIController.FadeInOut ());
	}

	private void IntilizePlayerPosition ()
	{
		playerGameObject.transform.position = PlayerPrefsX.GetVector3(SaveVarName.PlayerPosition);
	}

	private void GetReferences ()
	{
		playerGameObject = GameObject.FindGameObjectWithTag(Tags.Player);
	}

	#endregion
}
