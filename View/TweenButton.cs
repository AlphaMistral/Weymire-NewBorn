using UnityEngine;
using System.Collections;

/// <summary>
/// All the Buttons that includes a Tween Component should be inherited from this class. 
/// Please note that TweenButtons will also tween back when the tween is finished. 
/// In other words, tween two times. 
/// </summary>
[RequireComponent(typeof(UIPlayTween))]
[RequireComponent(typeof(UISprite))]
[RequireComponent(typeof(UIButton))]
public abstract class TweenButton : MonoBehaviour 
{
	#region Private Components

	protected UIPlayTween tweener;
	protected UISprite sprite;
	protected UIButton button;

	#endregion

	#region Private Variable

	/// <summary>
	/// Whether should the button work or not. 
	/// </summary>
	protected bool buttonWorkOrNot;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		buttonWorkOrNot = true;
		sprite = GetComponent <UISprite>();
		button = GetComponent <UIButton>();
		InitializeTweener();
	}

	#endregion

	#region Tweener Methods

	private void InitializeTweener ()
	{
		tweener = GetComponent <UIPlayTween>();
		tweener.tweenGroup = (int)TweenGroup.SelfOnly;
		tweener.playDirection = AnimationOrTween.Direction.Toggle;
	}

	public void TweenerStartPlay ()
	{
		tweener.onFinished.Clear();
		tweener.onFinished.Add(new EventDelegate(this, "PlayAgain"));
		tweener.Play(true);
	}

	public void PlayAgain ()
	{
		tweener.Play(true);
		tweener.onFinished.Clear();
		tweener.onFinished.Add(new EventDelegate(this, "ResetButton"));
	}

	public void ResetButton ()
	{
		buttonWorkOrNot = true;
		tweener.onFinished.Clear();
	}

	#endregion

	#region Protected Mothods

	public abstract void OnButtonClick();

	#endregion
}
