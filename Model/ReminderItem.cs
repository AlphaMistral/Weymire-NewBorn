using UnityEngine;
using System.Collections;

public class ReminderItem : Item 
{
	#region Private Vairables

	/// <summary>
	/// The type of the Reminder. 
	/// 1 means letter;
	/// 2 means E-mail;
	/// 3 means newspaper;
	/// 4 means research report. 
	/// </summary>
	private ReminderType type;

	/// <summary>
	/// The index of the clue to be unlocked after gaining this reminder. 
	/// Originally such a job is achieved through another script. The logic has been altered to meet the new requirements. 
	/// </summary>
	private int clueIndex;

	#endregion

	#region Public Attributes

	public string Content
	{
		get
		{
			return introduction;
		}
	}

	public ReminderType Type
	{
		get
		{
			return type;
		}
	}

	public int ClueIndex
	{
		get
		{
			return clueIndex;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ReminderItem"/> class.
	/// </summary>
	private ReminderItem ()
	{
		
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ReminderItem"/> class.
	/// </summary>
	/// <param name="m_index">M index.</param>
	/// <param name="m_type">M type.</param>
	/// <param name="m_clueIndex">M clue index.</param>
	/// <param name="m_name">M name.</param>
	/// <param name="m_content">M content.</param>
	private ReminderItem (int m_index, int m_type, int m_clueIndex, string m_name, string m_content)
	{
		index = m_index;
		type = (ReminderType)m_type;
		clueIndex = m_clueIndex;
		name = m_name;
		introduction = m_content;
	}

	#endregion

	#region Static Methods

	/// <summary>
	/// Gets the item by ID.
	/// </summary>
	/// <returns>The reminder item by ID.</returns>
	/// <param name="idx">Index.</param>
	public static ReminderItem GetItemByID (int idx)
	{
		string itemString = InfoSaver.GetStringFromResource(FileName.ReminderItem, idx);
		string[] splitted = itemString.Split('#');
		return new ReminderItem(idx, int.Parse(splitted[0]), int.Parse(splitted[1]), splitted[2], splitted[3]);
	}

	#endregion
}
