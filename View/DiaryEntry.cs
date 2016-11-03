using UnityEngine;
using System.Collections;

public class DiaryEntry : MonoBehaviour 
{
	#region Public Variables

	/// <summary>
	/// Stores the name of the diaryEntry. 
	/// </summary>
	public UILabel nameLabel;

	public ReminderItem ThisItem
	{
		get
		{
			return thisItem;
		}
	}

	#endregion

	#region Private Variables

	/// <summary>
	/// The ReminderItem instance. 
	/// </summary>
	private ReminderItem thisItem;

	/// <summary>
	/// The Button Component. 
	/// </summary>
	private UIButton button;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		
	}

	#endregion

	#region Initializers

	public void Initialize (ReminderItem ri)
	{
		thisItem = ri;
		button = GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(this, "OnButtonClick"));
		nameLabel.text = ri.Name;
	}

	#endregion

	#region ClickEvent

	public void OnButtonClick ()
	{
		DiaryWindow.ShowItem(ThisItem);
	}

	#endregion
}
