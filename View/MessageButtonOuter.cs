using UnityEngine;
using System.Collections;

/// <summary>
/// This Class Specifies the Outline of the Message Button.
/// The Outline Blinks when a new Message is Available. 
/// </summary>
[RequireComponent(typeof (UISprite))]
[RequireComponent(typeof (UIPlayTween))]
[RequireComponent(typeof (TweenScale))]
[RequireComponent(typeof (TweenColor))]
public class MessageButtonOuter : MonoBehaviour 
{
	#region Public Variables

	/// <summary>
	/// The Frequency of the blinking effect. 
	/// </summary>
	public float blinkFreq;

	#endregion

	#region Private Components

	/// <summary>
	/// The tweener Component. 
	/// </summary>
	private UIPlayTween tweener;

	private TweenScale scaler;

	private TweenColor colorer;

	#endregion

	#region Private Variables

	/// <summary>
	/// The Sprite MUST BLINK EVEN times to make sure that it won't look awkward. 
	/// </summary>
	private bool isEven;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		isEven = true;
		InitializeTweener();
	}

	#endregion

	#region Private Methods

	private void InitializeTweener ()
	{
		scaler = GetComponent <TweenScale>();
		colorer = GetComponent <TweenColor>();
		tweener = GetComponent <UIPlayTween>();
		scaler.duration = blinkFreq;
		colorer.duration = blinkFreq;
		tweener.tweenGroup = (int)TweenGroup.SelfOnly;
		tweener.playDirection = AnimationOrTween.Direction.Toggle;
	}

	public void StartBlinking ()
	{
		tweener.onFinished.Clear();
		tweener.onFinished.Add(new EventDelegate(this, "PlayAgain"));
		isEven = !isEven;
		tweener.Play(true);
	}

	public void PlayAgain ()
	{
		isEven = !isEven;
		tweener.Play(true);
		tweener.onFinished.Clear();
		tweener.onFinished.Add(new EventDelegate(this, "PlayAgain"));
	}

	public void StopBlinking ()
	{
		tweener.onFinished.Clear();
		if (!isEven)
		{
			isEven = true;
			tweener.Play(true);
		}
	}

	#endregion
}
