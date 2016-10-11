using UnityEngine;
using System.Collections;

public class TestJoystick : MonoBehaviour 
{
	private static float sensitivityThresold = 0.04f;

	private float joyStickValueX;
	private float joyStickValueY;

	void OnEnable()
	{
		EasyJoystick.On_JoystickMove += EasyJoystick_On_JoystickMove;
		EasyJoystick.On_JoystickTouchUp += EasyJoystick_On_JoystickMoveEnd;
	}

	void EasyJoystick_On_JoystickMoveEnd (MovingJoystick move)
	{
		joyStickValueX = joyStickValueY = 0;
	}

	void OnDisable()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		joyStickValueX = joyStickValueY = 0;
	}

	void OnDestroy()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		joyStickValueX = joyStickValueY = 0;
	}

	void EasyJoystick_On_JoystickMove (MovingJoystick move)
	{
		Debug.Log(1);
		joyStickValueX = move.joystickValue.x;
		joyStickValueY = move.joystickValue.y;
		if (Mathf.Abs(joyStickValueX) < sensitivityThresold)
			joyStickValueX = 0f;
		if (Mathf.Abs(joyStickValueY) < sensitivityThresold)
			joyStickValueY = 0f;
	}

	void Update ()
	{
		Debug.Log(joyStickValueX);
	}
}
