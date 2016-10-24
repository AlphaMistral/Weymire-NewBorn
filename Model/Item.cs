using UnityEngine;
using System.Collections;

public abstract class Item
{
	#region Protected Variables

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

public abstract class DisplayItem : Item
{
	#region Protected Variables

	protected string spriteName;

	protected string modelName;

	#endregion

	#region Public Attributes

	/// <summary>
	/// The sprite name of the Item. Used in NGUI Displaying. 
	/// </summary>
	public abstract string SpriteName
	{
		get;
	}

	/// <summary>
	/// The model name of the Item. Used in BackPack. 
	/// </summary>
	public abstract string ModelName
	{
		get;
	}

	public abstract string Introduction
	{
		get;
	}

	public DisplayItem ()
	{
		
	}

	public DisplayItem (int idx, string n, string sn, string mn)
	{
		index = idx;
		name = n;
		spriteName = sn;
		modelName = mn;
	}
	#endregion
}
