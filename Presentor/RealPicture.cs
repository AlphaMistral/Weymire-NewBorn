using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class RealPicture : PhotoObject 
{
	/// <summary>
	/// The Detection Colliders of the photo Area. The player must stand inside the area to trigger the detection.
	/// </summary>
	public Collider[] colliders;

	/// <summary>
	/// The Objects that must be located in the essential area of the camera view.
	/// </summary>
	public Transform[] strongObjects;

	/// <summary>
	/// The objects that must appear inside the camera view. It is OK to locate in the essential area. 
	/// </summary>
	public Transform[] weakObjects;

	/// <summary>
	/// Attribute Value of DetectionState.
	/// </summary>
	/// <value><c>true</c> if detection state; otherwise, <c>false</c>.</value>
	public bool DetectionState
	{
		get
		{
			return detectionState;
		}
	}

	/// <summary>
	/// Attribute Value of ColliderCount.
	/// </summary>
	/// <value>The collider count.</value>
	public int ColliderCount
	{
		set
		{
			colliderCount = value;
			if (colliderCount >= 1)
				SetDetectionState(true);
			else
				SetDetectionState(false);
		}
	}

	private bool detectionState;
	private int colliderCount;

	private static float ESSENCE_X_MIN = 0.15f;
	private static float ESSENCE_X_MAX = 0.85f;
	private static float ESSENCE_Y_MIN = 0.15f;
	private static float ESSENCE_Y_MAX = 0.85f;

	/// <summary>
	/// Set the detection state of the realpicutre. 
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	private void SetDetectionState (bool state)
	{
		detectionState = state;
	}

	/// <summary>
	/// Determine whether this real picuture could be taken legally.
	/// </summary>
	/// <returns><c>true</c>, if real picture was detected, <c>false</c> otherwise.</returns>
	public override Consts.ObjectSituation DetermineAvailability (InventoryState state, Vector2 screenPosition, Vector2 thresold, float maxDistance)
	{
		if (!DetectionState)
			return Consts.ObjectSituation.WrongMode;
		Camera playerCamera = Camera.main;
		Vector3 cameraPos = playerCamera.transform.position;
		foreach (Transform col in strongObjects)
		{
			RaycastHit hit;
			Vector3 direction = Vector3.Normalize(col.position - cameraPos);
			if (Physics.Linecast(cameraPos, direction, out hit))
			{
				if (hit.collider != col.GetComponent<Collider> ())
					return Consts.ObjectSituation.NoMatch;
			}
			else
				return Consts.ObjectSituation.NoMatch;
			Vector2 screenPos = playerCamera.WorldToScreenPoint(col.position);
			if (screenPos.x <= ESSENCE_X_MIN * Screen.width || screenPos.x >= ESSENCE_X_MAX * Screen.width)
				return Consts.ObjectSituation.NoMatch;
			else if (screenPos.y <= ESSENCE_Y_MIN * Screen.height || screenPos.y >= ESSENCE_Y_MAX * Screen.height)
				return Consts.ObjectSituation.NoMatch;
		}
		foreach (Transform col in weakObjects)
		{
			RaycastHit hit;
			Vector3 direction = Vector3.Normalize(col.position - cameraPos);
			if (Physics.Linecast(cameraPos, direction, out hit))
			{
				if (hit.collider != col.GetComponent<Collider> ())
					return Consts.ObjectSituation.NoMatch;
			}
			else
				return Consts.ObjectSituation.NoMatch;
			Vector2 screenPos = playerCamera.WorldToScreenPoint(col.position);
			if (screenPos.x <= 0f || screenPos.x >= Screen.width)
				return Consts.ObjectSituation.NoMatch;
			else if (screenPos.y <= 0f || screenPos.y >= Screen.height)
				return Consts.ObjectSituation.NoMatch;
		}
		return Consts.ObjectSituation.OK;
	}

	/// <summary>
	/// The Override Function of the Abstract Class
	/// However the InventoryState could be useless. It depends on Jiawen WU's Design --- Could the player take a real Picture under Hack mode and nightView Mode?
	/// This could not be determined yet.
	/// </summary>
	/// <param name="state">State.</param>
	public override void PhotoTaken (InventoryState state)
	{
		//To be Implemented : 
		//Alert a message telling the player that "ObjectName" photo has been taken. 
		//Complete an assignment.
	}

	public override bool NotifyObjectOnScreen (InventoryState state, Vector2 screePos, float maxDistance)
	{
		return true;
	}
}
