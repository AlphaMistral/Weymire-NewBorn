using UnityEngine;
using System.Collections;

public abstract class Item
{
	#region Protected Vairbles

	/// <summary>
	/// The index of the Item in its OWN CATEGORY. Two different items may have a same index. However they are tracked in different databases. 
	/// </summary>
	protected int index;

	/// <summary>
	/// The name of the Item. 
	/// </summary>
	protected string name;

	/// <summary>
	/// The introduction of the item. Please Note that introduction is the content of Reminders. Reminders don't need introductions. 
	/// </summary>
	protected string introduction;

	#endregion

	#region Public Attributes

	public int Index
	{
		get
		{
			return index;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	//There is no specific Attribute for Introduction because Introduction is treated differently in items and reminders. 

	#endregion
}
