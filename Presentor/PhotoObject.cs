using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public abstract class PhotoObject : MonoBehaviour 
{
	public string objectName;
	public string objectIntroduction;

	public string luaVarchar;
	public List<string> lua2TrueOnSucc = new List<string>();
	public List<string> lua2TrueOnFail = new List<string>();
	public List<string> questStartOnSucc = new List<string>();
	public List<string> questSuccOnSucc = new List<string>();
	public List<GameObject> activateGOOnSucc = new List<GameObject>();
	public List<GameObject> destructGOOnSucc = new List<GameObject>();
	public List<EventDelegate> triggerMethods = new List<EventDelegate>();
	public string alertMessage;

	public virtual void Awake ()
	{
		if (DialogueLua.GetVariable(luaVarchar).AsBool)
		{
			foreach (GameObject go in activateGOOnSucc)
				go.SetActive(true);
			foreach (GameObject go in destructGOOnSucc)
				Destroy(go);
			EventDelegate.Execute(triggerMethods);
		}
	}

	/// <summary>
	/// Defines the behaviour after the photo is successfully taken.
	/// </summary>
	/// <param name="state">State.</param>
	public abstract void PhotoTaken (InventoryState state);

	/// <summary>
	/// Notifies the object on screen based on the screen position transferred to the function. Return a bool value
	/// To Represent whether this object could be taken or not.
	/// Issue: Consider Deletion! 
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	public abstract bool NotifyObjectOnScreen (InventoryState state, Vector2 screenPosition, float maxDistance);
		
	/// <summary>
	/// Determines whether this object could be taken under the current inventoryState and the angle of the camera.
	/// </summary>
	/// <returns><c>true</c>, if availability was determined, <c>false</c> otherwise.</returns>
	/// <param name="">.</param>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="maxDistance">Max distance.</param>
	public abstract Consts.ObjectSituation DetermineAvailability (InventoryState state, Vector2 screenPosition, Vector2 thresold, float maxDistance);
}
