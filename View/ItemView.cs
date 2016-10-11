using UnityEngine;
using System.Collections;

public class ItemView : MonoBehaviour 
{
	/// <summary>
	/// The item information of the view Component.
	/// </summary>
	public Item item;

	/// <summary>
	/// The sprite of the itemView. Very crucial to handle.
	/// </summary>
	private UISprite sprite;

	/// <summary>
	/// Highlights the sprite of the itemView. This is used to represent the selection of the player.
	/// </summary>
	public void HighLight ()
	{
		
	}

	/// <summary>
	/// When the player selects another item in the backpack, dim it. 
	/// </summary>
	public void DimLight ()
	{
		
	}

	private void Awake ()
	{
		item = new Item();
	}
}
