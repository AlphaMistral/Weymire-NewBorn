using UnityEngine;
using System.Collections;

/// <summary>
/// Please note that "CameraButton" is not only the button ... it also includes the background sprite (used to capture event) and 
/// camera mode options.
/// </summary>
public class CameraButton : MonoBehaviour 
{
	public UISprite backGroundSprite;

	/// <summary>
	/// It is still controversial whether we should generate all 4 buttons or only those that the player has unlocked.
	/// </summary>
	public UIButton[] modeButtons;

	private void Awake ()
	{
		
	}

	/// <summary>
	/// Activates the mode selection. However an effect could be attached. It could either be UI Tween or Aniamtion.
	/// </summary>
	public void ActivateModeSelection()
	{
		
	}

	/// <summary>
	/// Takes the Photo. Please Remember to implement the corresponding materials in the PhotoCamera Script.
	/// </summary>
	public void TakePhoto ()
	{
		
	}
}
