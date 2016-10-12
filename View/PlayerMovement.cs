using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	#region Public Variables

	public float movingSpeed = 3f;
	public float rotatingSpeed = 3f;

	#endregion

	#region Public References

	/// <summary>
	/// The Player could either be first-person of third-person. Hence it needs a reference to the camera. 
	/// </summary>
	public GameObject playerCamera;

	#endregion

	#region Private Attributes

	private Transform m_camera_transform;
	private Animator m_animator;
	private Rigidbody m_rigidbody;

	#endregion

	#region Private Variables

	/// <summary>
	/// The Sensitivity of the Joystick. Values below the thresold would be IGNORED. 
	/// </summary>
	private static float sensitivityThresold = 0.04f;

	/// <summary>
	/// The input of the JoyStick. 
	/// </summary>
	private float joyStickValueX, joyStickValueY;

	#endregion

	#region MonoBehaviours

	private void OnEnable ()
	{
		EasyJoystick.On_JoystickMove += EasyJoystick_On_JoystickMove;
		EasyJoystick.On_JoystickTouchUp += EasyJoystick_On_JoystickTouchUp;
	}

	private void OnDisable ()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		EasyJoystick.On_JoystickTouchUp -= EasyJoystick_On_JoystickTouchUp;
	}

	private void OnDestory ()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		EasyJoystick.On_JoystickTouchUp -= EasyJoystick_On_JoystickTouchUp;
	}

	private void Awake ()
	{
		//playerCamera = Camera.main.gameObject;
		m_camera_transform = playerCamera.transform;
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate ()
	{
		ManageMovement();
	}

	#endregion

	#region JoystickMethods

	private void EasyJoystick_On_JoystickMove (MovingJoystick move)
	{
		joyStickValueX = move.joystickValue.x;
		joyStickValueY = move.joystickValue.y;
		if (Mathf.Abs(joyStickValueX) < sensitivityThresold)
			joyStickValueX = 0f;
		if (Mathf.Abs(joyStickValueY) < sensitivityThresold)
			joyStickValueY = 0f;
	}

	private void EasyJoystick_On_JoystickTouchUp (MovingJoystick move)
	{
		joyStickValueX = joyStickValueY = 0f;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Called in FixedUpdate only. 
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="vertical">Vertical.</param>
	private void Rotating (float horizontal, float vertical)
	{
		float yAxisRotation = transform.rotation.eulerAngles.y;
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		m_rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotatingSpeed));
	}

	private void ManageMovement ()
	{
		Vector3 originalMove = new Vector3(joyStickValueX, 0f, joyStickValueY);
		Vector3 cameraRotation = m_camera_transform.rotation.eulerAngles;
		Quaternion targetRotation = Quaternion.Euler(0f, cameraRotation.y, 0f);
		Quaternion toRotate = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
		Vector3 toMove = toRotate * originalMove;
		if (toMove.z != 0f || toMove.x != 0f)
		{
			Rotating(toMove.x, toMove.z);
			m_animator.SetFloat(AnimatorHash.speedFloat, movingSpeed);
		}
		else
		{
			m_animator.SetFloat(AnimatorHash.speedFloat, 0f);
		}
	}
	#endregion
}
