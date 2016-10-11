using UnityEngine;
using System.Collections;

/// <summary>
/// Logic of the script: Whenever the player hits the button, start counting time. When the finger is lifted up, 
/// Determine what to do based on the time counted. 
/// </summary>
public class TestPhotoButton : MonoBehaviour 
{
	public float longPressTime;

	private bool isFingerDown;

	private float holdTime;

	public void OnPress (bool state)
	{
		isFingerDown = state;
		//If the Finger is lifted up from the screen, process the touch. 
		if (!state)
		{
			if (holdTime >= longPressTime)
			{
				LongPress();
			}
			else
				ShortPress();
			holdTime = 0f;
		}
	}

	void Update ()
	{
		if (isFingerDown)
			holdTime += Time.deltaTime;
	}

	private void LongPress ()
	{
		Debug.Log("This is a long Press!");
	}

	private void ShortPress ()
	{
		Debug.Log("This is a short Press!");
	}
}
