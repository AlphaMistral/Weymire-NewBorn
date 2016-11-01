using UnityEngine;
using System.Collections;

public class MessageButton : TweenButton 
{
	#region SeilizedVariables

	[SerializeField]
	/// <summary>
	/// The outline of the MessageButton. When a new message is available the outline will blink. 
	/// </summary>
	private MessageButtonOuter outline;

	#endregion

	#region Override Methods

	public override void OnButtonClick ()
	{
		if (!buttonWorkOrNot)
			return;
		buttonWorkOrNot = false;
		TweenerStartPlay();
		outline.StopBlinking();
	}

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		
	}

	#endregion

	#region Public Methods

	private void StartBlinking ()
	{
		outline.StartBlinking();
	}

	#endregion
}
