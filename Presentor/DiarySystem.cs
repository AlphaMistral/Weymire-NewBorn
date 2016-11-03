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
	public static List<ReminderItem> allReminders;

	/// <summary>
	/// The view Component of the diary system. 
	/// </summary>
	public DiaryWindow view_window;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		allReminders = new List<ReminderItem>();
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate (gameObject);
		//LoadReminders ();
		///WARNING: TEST ONLY AREA!!!!!!
		TESTLoadMethods ();
		///WARNING: TEST ONLY AREA!!!!!!
	}

	#endregion

	#region Public static Methods



	#endregion

	#region Private static Methods

	private static void LoadReminders ()
	{
		int[] allReminderIdx = PlayerPrefsX.GetIntArray(SaveVarName.ReminderArray);
		foreach (int idx in allReminderIdx)
		{
			ReminderItem reminderItem = ReminderItem.GetItemByID(idx);
			allReminders.Add(reminderItem);
			instance.view_window.AddEntry(reminderItem);
		}
	}

	#endregion

	#region TestOnlyMethods

	public static void TESTLoadMethods()
	{
		ReminderItem t1 = ReminderItem.GetItemByID(0);
		ReminderItem t2 = ReminderItem.GetItemByID(1);
		allReminders.Add(t1);
		allReminders.Add(t2);
		instance.view_window.AddEntry(t1);
		instance.view_window.AddEntry(t2);
	}

	#endregion
}
