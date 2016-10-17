using UnityEngine;
using System.Collections;

/// <summary>
/// Switcher Serves as the switcher of the First Person Mode and the Thrid Person Mode.
/// The Switch Method is not to be implemented in the UIController. 
/// </summary>
public class SwitchButton : MonoBehaviour 
{
	#region Public Attributes

	/// <summary>
	/// The CameraFollow Component of the PlayerCamera. To be changed when the button is clicked. 
	/// </summary>
	public CameraFollow playerCamera;

	/// <summary>
	/// The Set of the tween animation gameObjects. Toggle on or off PlayTween. 
	/// </summary>
	public GameObject tweenSet;

	#endregion

	#region Private Variables

	/// <summary>
	/// Is the animator working. If still working, not allowed to invoke any other options. 
	/// </summary>
	private bool isAnimating;

	#endregion

	#region Private Attributes

	/// <summary>
	/// The tween component of the SwitchButton. Please note that the component and the target are seperate. 
	/// </summary>
	private UIPlayTween tweenComponent;

	/// <summary>
	/// The button component of the gameObject. 
	/// </summary>
	private UIButton m_button;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		isAnimating = false;
		tweenComponent = GetComponent <UIPlayTween>();
		playerCamera = Camera.main.GetComponent <CameraFollow>();
		tweenComponent.tweenTarget = tweenSet;
		m_button = GetComponent <UIButton>();
	}

	#endregion

	#region Methods

	/// <summary>
	/// Somebody asked me why don't I put these two functions together and give a parameter to control the behaviour.
	/// Well, I think you had better ask NGUI instead. 
	/// </summary>
	public void StartMoving ()
	{
		isAnimating = true;
		m_button.isEnabled = false;
		tweenComponent.Play(true);
		Debug.Log("Started! ");
	}

	/// <summary>
	/// Fuck EventDelegate! I will definitely re-write it! 
	/// </summary>
	public void ReInitialize ()
	{
		isAnimating = false;
		m_button.isEnabled = true;
		Debug.Log("Ended! ");
	}

	public void ToggleSwitchAnimatioon ()
	{
		
	}

	#endregion

	#region NGUI Button Methods

	public void OnClick ()
	{
		if (!isAnimating)
			StartMoving();
		else
			Debug.Log("Fuck you asshole! ");
	}

	#endregion
}
