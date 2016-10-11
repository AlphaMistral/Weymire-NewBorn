using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

/// <summary>
/// This system is prensentor part of the diary system. It is capable of manipulating the specific NGUI components, add new reminder entries and so forth. 
/// </summary>
public class DiarySystem : MonoBehaviour 
{
	#region Public static Variables

	/// <summary>
	/// Singleton. 
	/// </summary>
	public static DiarySystem instance;

	/// <summary>
	/// All of the reminders in the diary system. 
	/// </summary>
	public static List<Item> allReminders;

	/// <summary>
	/// The item that is currently showing. 
	/// </summary>
	public static Item currentShowingReminder;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate (gameObject);
	}

	#endregion

	#region Public static Methods



	#endregion
}
