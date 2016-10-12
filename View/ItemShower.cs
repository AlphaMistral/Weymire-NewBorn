using UnityEngine;
using System.Collections;

public class ItemShower : MonoBehaviour 
{
	#region Self-Properties

	public enum ShowType
	{
		Letter,
		Newspaper,
		Email,
		Special,
		Normal
	};

	#endregion

	#region Public Variables

	/// <summary>
	/// The type of the itemShower. 
	/// </summary>
	public ShowType type;

	/// <summary>
	/// The distance of the padding between adjacent Comments. It is set to be Integer because the NGUI does this.
	/// (Although I have no idea why)
	/// </summary>
	public int paddingDistance;

	/// <summary>
	/// The speed of the typewritting effect in the commetnts. Please note that the typingSpeed is 1 / how many characters per second. 
	/// </summary>
	public float typingSpeed;

	/// <summary>
	/// The comments. 
	/// </summary>
	public string[] comments;

	/// <summary>
	/// The index of the item. This is the primary key of the coroutine. 
	/// </summary>
	public int itemIndex;

	/// <summary>
	/// This is where the comments are stored and managed.
	/// </summary>
	public UITable commentTable;

	/// <summary>
	/// The scroll bar.
	/// </summary>
	public UIScrollBar scrollBar;

	/// <summary>
	/// The scroll view.
	/// </summary>
	public UIScrollView scrollView;

	/// <summary>
	/// The wait time for the dialogue to start. Only after this period could a dialogue begin. 
	/// Just made to avoid possible awkward blink on dialogue area. No other senses. 
	/// </summary>
	private static float waitTime = 1f;

	#endregion

	#region Serilized Fields

	/// <summary>
	/// The point where the comment is to be instantiated. 
	/// </summary>
	public Transform comment_inst_point;

	/// <summary>
	/// The sprite of the item. If it is not an entity, then it serves as the background. 
	/// </summary>
	public UISprite itemSprite;

	/// <summary>
	/// The content of the reminder. If it is an entity, simply left this empty. 
	/// </summary>
	public string reminder_content;

	/// <summary>
	/// The Escape Button. By default it is left inactive. 
	/// </summary>
	public UIButton escapeButton;

	#endregion

	#region Prefabs

	/// <summary>
	/// The bubbles of the comments. 
	/// </summary>
	public GameObject playerBubble, butlerBubble;

	#endregion

	#region Private Variables

	private int currentIndex = 0;
	private UISprite currentSprite = null;
	private UILabel currentLabel = null;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		escapeButton.isEnabled = false;
	}

	private void Start ()
	{
		StartCoroutine(Show(itemIndex));
	}

	#endregion

	#region Private Methods

	private IEnumerator Show (int item_idx)
	{
		// Alter the itemSprite and reminderContent here!

		// Alter the itemSprite and reminderContent here!
		yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds(waitTime);
		InstantiateNewComment(out currentSprite, out currentLabel, true);
		while (currentIndex < comments.Length)
		{
			if (currentLabel.text == comments[currentIndex])
			{
				currentIndex++;
				if (currentIndex >= comments.Length)
					break;
				UISprite newSprite = null;
				UILabel newLabel = null;
				InstantiateNewComment(out newSprite, out newLabel, currentIndex % 2 == 0);
				currentSprite = newSprite;
				currentLabel = newLabel;
				yield return null;
			}
			else
			{
				currentLabel.text += comments[currentIndex][currentLabel.text.Length];
				yield return new WaitForSeconds(typingSpeed);
			}
			commentTable.Reposition();
			scrollView.SetDragAmount(0f, 1.0f, false);
		}
		escapeButton.isEnabled = true;
	}

	/// <summary>
	/// Instantiates a new comment. 
	/// </summary>
	/// <param name="sprite">Sprite.</param>
	/// <param name="label">Label.</param>
	/// <param name="isPlayer">If set to <c>true</c> is player.</param>
	private void InstantiateNewComment(out UISprite sprite, out UILabel label, bool isPlayer)
	{
		label = NGUITools.AddChild(commentTable.gameObject, isPlayer? playerBubble : butlerBubble).GetComponent<UILabel> ();
		sprite = label.transform.FindChild("Sprite").GetComponent<UISprite> ();
		label.transform.localPosition = comment_inst_point.localPosition;
		//commentTable.Reposition ();
	}

	#endregion
}
