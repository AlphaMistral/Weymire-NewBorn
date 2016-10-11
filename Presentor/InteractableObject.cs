using UnityEngine;
using System.Collections;

public class InteractableObject : PhotoObject 
{
	public int keyItemIndex;

	public override void PhotoTaken(InventoryState state)
	{
		if (state.item.Index == keyItemIndex)
		{
			PhotoSuccess();
		}
		else
		{
			PhotoFail();
		}
	}

	public virtual void PhotoSuccess ()
	{
		
	}

	public virtual void PhotoFail ()
	{
		
	}

	public override bool NotifyObjectOnScreen (InventoryState state, Vector2 screenPos, float maxDistance)
	{
		return true;
	}

	public override Consts.ObjectSituation DetermineAvailability (InventoryState state, Vector2 screenPos, Vector2 thresold, float maxDistance)
	{
		return Consts.ObjectSituation.OK;
	}
}
