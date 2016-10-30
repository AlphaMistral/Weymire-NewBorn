using UnityEngine;
using System.Collections;

public class AnalyzeButton : TweenButton
{
	#region Serialized Variables

	[SerializeField]
	/// <summary>
	/// The parent slider. 
	/// </summary>
	private SpriteSlider slider;

	#endregion

	#region Override Methods

	public override void OnButtonClick ()
	{
		if (!buttonWorkOrNot)
			return;
		buttonWorkOrNot = false;
		TweenerStartPlay();
		slider.ForceShrink();
	}

	#endregion
}
