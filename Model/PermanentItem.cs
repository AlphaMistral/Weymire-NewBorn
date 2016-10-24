using UnityEngine;
using System.Collections;

public class PermanentItem : DisplayItem
{
	#region Public Attributes

	/// <summary>
	/// The sprite name of the Item. Used in NGUI Displaying. 
	/// </summary>
	public override string SpriteName
	{
		get
		{
			return spriteName;
		}
	}

	/// <summary>
	/// The model name of the Item. Used in BackPack. 
	/// </summary>
	public override string ModelName
	{
		get
		{
			return modelName;
		}
	}

	public override string Introduction
	{
		get
		{
			return introduction;
		}
	}

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the <see cref="PermanentItem"/> class.
	/// </summary>
	private PermanentItem ()
	{
		
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="PermanentItem"/> class.
	/// </summary>
	/// <param name="m_index">M index.</param>
	/// <param name="m_name">M name.</param>
	/// <param name="m_intro">M intro.</param>
	/// <param name="m_spriteName">M sprite name.</param>
	/// <param name="m_modelName">M model name.</param>
	private PermanentItem (int m_index, string m_name, string m_intro, string m_spriteName, string m_modelName)
	{
		index = m_index;
		name = m_name;
		introduction = m_intro;
		spriteName = m_spriteName;
		modelName = m_modelName;
	}

	#endregion

	#region Static Methods

	/// <summary>
	/// Gets the item by Index.
	/// </summary>
	/// <returns>The permanent item by the specified Index.</returns>
	/// <param name="idx">Index.</param>
	public static PermanentItem GetItemByID (int idx)
	{
		string itemString = InfoSaver.GetStringFromResource(FileName.PermamnentItem, idx);
		string[] splitted = itemString.Split(' ');
		PermanentItem retItem = new PermanentItem(idx, splitted[0], splitted[1], splitted[2], splitted[3]);
		return retItem;
	}

	#endregion
}
