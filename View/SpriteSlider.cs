using UnityEngine;
using System.Collections;

public class SpriteSlider : MonoBehaviour 
{
	public enum SliderState
	{
		Extended = 1,
		Shrinked = 2
	}

	#region Public Variables

	/// <summary>
	/// The SPRITE name when the button is avtive. 
	/// </summary>
	public string name_active;

	/// <summary>
	/// You get its meaning. 
	/// </summary>
	public string name_inactive;

	public SliderState State
	{
		get
		{
			if (currentState)
				return SliderState.Extended;
			else
				return SliderState.Shrinked;
		}
	}

	#endregion

	#region Private Variables

	/// <summary>
	/// The UIButton Component of the gameObject. 
	/// </summary>
	private UIButton button;

	/// <summary>
	/// The UISprite Component of the gameObject.
	/// </summary>
	private UISprite sprite;

	/// <summary>
	/// The UIPlayTween Component of the gameObject;
	/// </summary>
	private UIPlayTween tweener;

	/// <summary>
	/// The current Extension state of the slider. 
	/// By default the slider is shrinked. 
	/// </summary>
	private bool currentState = false;

	#endregion

	#region MonoBehaviours 

	private void Awake ()
	{
		button = GetComponent <UIButton>();
		sprite = GetComponent <UISprite>();
		InitializeTweener();
		currentState = false;
	}

	#endregion

	#region Button Methods

	/// <summary>
	/// The Event Triggered when the button is clicked.
	/// Please note that the name of the Method is not OnClick, which is the method invoked automatically by NGUI.
	/// </summary>
	public void OnButtonClick ()
	{
		button.isEnabled = false;
		tweener.Play(true);
	}

	public void ChangeButtonState ()
	{
		button.isEnabled = !button.isEnabled;
		currentState = !currentState;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// It is literally not a good habit to code like this ... 
	/// Anyway its fucking convenient and I know I don't have to consider further implementation on it -- no more implementation HAHAHAHAHH! 
	/// </summary>
	public void ChangeSprite ()
	{
		if (sprite.spriteName == name_active)
			sprite.spriteName = name_inactive;
		else
			sprite.spriteName = name_active;
	}

	#endregion

	#region Private Methods

	private void InitializeTweener ()
	{
		//Prevent from setting playDirection over and over again. Simply set it to toggle is ok. 
		tweener = GetComponent <UIPlayTween>();
		tweener.tweenGroup = (int)TweenGroup.MainSet;
		tweener.playDirection = AnimationOrTween.Direction.Toggle;
		tweener.includeChildren = true;
		//Don't question me. It is no way hard-coding! 
		EventDelegate del = new EventDelegate(this, "ChangeButtonState");
		tweener.onFinished.Clear();
		tweener.onFinished.Add (del);
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Force the slider to extend out. 
	/// </summary>
	public void ForceExtend ()
	{
		//Isn't it ... a little bit ... I mean, weird?! 
		//Anyway, the name of the method is not a problem ... 
		if (!currentState)
			OnButtonClick();
	}

	/// <summary>
	/// Force the slider to shrink away. 
	/// </summary>
	public void ForceShrink ()
	{
		if (currentState)
			OnButtonClick();
	}

	#endregion
}
