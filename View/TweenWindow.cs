using UnityEngine;
using System.Collections;

/// <summary>
/// All the tweenable windows, including but not limited to: 
/// Dialogue Window
/// Reminder Window
/// Gallery Window
/// Analyze Window
/// All should inherit from this class. 
/// </summary>
public abstract class TweenWindow : MonoBehaviour 
{
	#region Public Variables

	/// <summary>
	/// The screenPosition Relative to the father GameObject Under UIRoot. 
	/// </summary>
	public Vector2 local_screen_pos = new Vector2(0f, 0f);

	#endregion
}
