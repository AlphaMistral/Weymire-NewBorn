using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PickUpItem : PhotoObject 
{
	/// <summary>
	/// The Corresponding index of the item it represents. Essential to determine an item in the database.
	/// </summary>
	public int itemIndex;

	/// <summary>
	/// The camera mode must matches the expected mode for a shot. 
	/// </summary>
	public CameraMode expectedMode;

	/// <summary>
	/// The Override Function of the PhotoTaken in the Base Abstract Class.
	/// </summary>
	public override void PhotoTaken (InventoryState state)
	{
		//PlayerInventory.AddItemByIndex(itemIndex);
		//To Be Continued: 
		//Listen to the event raised by photoObjectShower and destroy. 
	}

	/// <summary>
	/// The Notification must be based on the lens that is currently equipped. 
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="state">State.</param>
	public override bool NotifyObjectOnScreen (InventoryState state, Vector2 screenPosition, float maxDistance)
	{
		if (state.cameraMode != expectedMode)
			return false;
		if (screenPosition.x >= Constant.PhotoObjectShowArea.x && screenPosition.x <= Constant.PhotoObjectShowArea.y &&
		    screenPosition.y >= Constant.PhotoObjectShowArea.z && screenPosition.y <= Constant.PhotoObjectShowArea.w)
			return true;
		else
			return false;
	}


	public override Consts.ObjectSituation DetermineAvailability (InventoryState state, Vector2 screenPosition, Vector2 thresold, float maxDistance)
	{
		float xx = screenPosition.x / Screen.width;
		float yy = screenPosition.y / Screen.height;
		if (xx >= thresold.x && xx <= 1f - thresold.x && yy >= thresold.y && yy <= 1f - thresold.y)
			return Consts.ObjectSituation.OK;
		else
			return Consts.ObjectSituation.Irrelevant;
	}

	/// <summary>
	/// Called when the item is shown and the shower is still active. Stop listening the event and destroy the gameObject. 
	/// </summary>
	private void OnShown ()
	{
		
	}
}

