using UnityEngine;
using System.Collections;

public class DiaryWindow : MonoBehaviour 
{
	#region Public Variables

	/// <summary>
	/// The background of the diaries. 
	/// </summary>
	public UISprite background;

	/// <summary>
	/// Where the contents are written. 
	/// </summary>
	public UILabel contentLabel;

	/// <summary>
	/// The Reminder that is currently being shown. 
	/// </summary>
	public ReminderItem ShowingItem;

	/// <summary>
	/// Where the names of the reminders are stored. 
	/// </summary>
	public UIGrid reminderGrid;

	/// <summary>
	/// The Reminder that is currently Showing. 
	/// </summary>
	public ReminderItem ShowingReminder
	{
		get
		{
			return showingReminder;
		}
	}

	#endregion

	#region Prefabs

	/// <summary>
	/// The prefab of the Diary Entry. 
	/// </summary>
	public DiaryEntry entry_prefab;

	#endregion

	#region Private Variables

	private static DiaryWindow instance; 
	private static ReminderItem showingReminder;

	private static Vector3 defaultBackgroundPosition = new Vector3(150f, -60f, 0f);
	private static Vector3 backgroundStartPosition = new Vector3(0f, -160f, 0f);
	private static Vector3 targetBackgroundPosition = new Vector3(150f, -690f, 0f);
	private static Vector3 targetBackgroundScale = new Vector3(0.7f, 0.7f, 0.7f);

	private static float tweenDuration = 0.4f;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		GenerateSingleton();
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Adds the ReminderItem entry into the system. 
	/// </summary>
	/// <param name="ri">Ri.</param>
	public void AddEntry (ReminderItem ri)
	{
		DiaryEntry newEntry = NGUITools.AddChild(reminderGrid.gameObject, entry_prefab.gameObject).GetComponent<DiaryEntry>();
		newEntry.Initialize(ri);
		reminderGrid.Reposition();
	}

	#endregion

	#region Public Static Methods

	public static void ShowItem (ReminderItem ri)
	{
		if (ri == showingReminder)
			return;
		showingReminder = ri;
		string content = ri.Content;
		instance.contentLabel.text = content;
		ReminderType type = ri.Type;
		PlayTween();
		ChangeBackground(type);
	}

	#endregion

	#region Private Methods

	private void GenerateSingleton ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
			DestroyImmediate(gameObject);
	}

	private static void ChangeBackground(ReminderType type)
	{
		switch (type)
		{
			case ReminderType.Letter:
				instance.background.spriteName = SpriteName.Reminder.letter;
				break;
			case ReminderType.EMail:
				instance.background.spriteName = SpriteName.Reminder.email;
				break;
			case ReminderType.Newspaper:
				instance.background.spriteName = SpriteName.Reminder.news;
				break;
			case ReminderType.ResearchReport:
			default:
				instance.background.spriteName = SpriteName.Reminder.report;
				break;
		}
	}

	private static void PlayTween ()
	{
		GameObject fadeObject = NGUITools.AddChild(instance.background.transform.parent.gameObject, instance.background.gameObject);
		Destroy(fadeObject, tweenDuration + 0.1f);//It would be pretty awkward to destroy the gameObject at the very end of the tween ... 
		fadeObject.transform.localPosition = instance.background.transform.localPosition;
		fadeObject.transform.localScale = instance.background.transform.localScale;
		instance.background.transform.localScale = new Vector3(0f, 0f, 0f);
		instance.background.alpha = 0f;
		instance.background.transform.localPosition = backgroundStartPosition;
		TweenPosition tp = TweenPosition.Begin(fadeObject, tweenDuration, targetBackgroundPosition);
		TweenAlpha ta = TweenAlpha.Begin(instance.background.gameObject, 3 * tweenDuration, 1f);//Not hard-coding! Alpha value is fucking annoying ...
		TweenPosition tp1 = TweenPosition.Begin(instance.background.gameObject, tweenDuration, defaultBackgroundPosition);
		TweenScale ts = TweenScale.Begin(instance.background.gameObject, tweenDuration, new Vector3(1f, 1f, 1f));
		TweenAlpha ta1 = TweenAlpha.Begin(fadeObject, 2 * tweenDuration, 0f);//Same as above. 
		TweenScale ts1 = TweenScale.Begin(fadeObject, tweenDuration, targetBackgroundScale);
		tp.method = UITweener.Method.EaseIn;
		tp1.method = UITweener.Method.EaseIn;
		ta.method = UITweener.Method.EaseIn;
		ts.method = UITweener.Method.EaseIn;
		ts1.method = UITweener.Method.EaseIn;
		ta.ignoreTimeScale = false;
		tp1.ignoreTimeScale = false;
		ts.ignoreTimeScale = false;
		tp.ignoreTimeScale = false;
		ta1.ignoreTimeScale = false;
		ts1.ignoreTimeScale = false;
	}

	#endregion
}
