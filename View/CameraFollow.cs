using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	#region Public Variables and Attributes

	/// <summary>
	/// True means FIRST person while false means THRID person.
	/// </summary>
	public bool currentControlState = false;

	/// <summary>
	/// The transform component of the player GameObject.
	/// </summary>
	public Transform toFollow;

	/// <summary>
	/// The offset of the position of the camera relative to the player Transform.
	/// This is not hard-coding. Rather it is an appropriate parameter explored by me. 
	/// </summary>
	public Vector3 offset = new Vector3(1f, 0.5f, -1.56f);

	/// <summary>
	/// Since the pivot of the model of the player GameObject is located at the foot, in order to make sure that the Obstacle Avoidance could appropritately work out, 
	/// a yOffset is designed independently to make the fabricated pivot located at the center of the player. By default it is 1. 
	/// </summary>
	public float yOffset = 1f;

	#endregion

	#region Private Variables

	/// <summary>
	/// The delta finger position of the swiping pos. Controlled by EasyTouch.
	/// </summary>
	private Vector2 deltaPosition;

	/// <summary>
	/// Whether the player is allowed to swipe or not. 
	/// </summary>
	private bool isLocked;

	/// <summary>
	/// The original distance from the camera to the center of the player. 
	/// </summary>
	private float originalDistance;

	/// <summary>
	/// The magnitude of the distance. 1 for maximum distance and 0 for infinitely close. 
	/// </summary>
	private float testMagnitude;

	/// <summary>
	/// The default compass direction. 
	/// </summary>
	private float defaultCompassDirection;

	#endregion

	#region DeltaPosition Calculation Helpers

	private static Vector2 relativeScreen = new Vector2 (1920f, 1080f);
	private static Vector2 swipeEffectiveArea = new Vector2(0.4f, 0.25f);
	private static float FPRotatingSpeed = 1.5f;
	private static float swipeRotatingSpeed = 8f;
	private static float followOffset = 0.5f;
	private static float zRotationMax = 15f;
	private static float xRotationMin = -8f;
	private static float lookupScaler = 0.1f;

	#endregion

	/// <summary>
	/// Get the viewing direction based on current gyroscope and compass. 
	/// </summary>
	/// <returns>The real time view direction.</returns>
	private Quaternion GetRealTimeViewDirection ()
	{
		float xx, yy, zz;
		yy = Input.compass.magneticHeading - defaultCompassDirection;
		xx = -Input.acceleration.z * 90f;
		zz = -Input.acceleration.x * 90f;
		if (xx > 90f)
			xx = 90f;
		else if (xx < -90f)
			xx = -90f;
		return Quaternion.Euler (xx,yy,zz);
	}

	#region MonoBehaviours

	void Awake ()
	{
		Input.location.Start ();
		Input.compass.enabled = true;
		testMagnitude = 1f;
		defaultCompassDirection = Input.compass.magneticHeading;
		originalDistance = Vector3.Magnitude(offset);
	}

	void Start ()
	{

	}

	/// <summary>
	/// Make sure it is LateUpdate to Avoid Possible Shaking problem. Flag: Why would it shake anyway? 
	/// </summary>
	void LateUpdate ()
	{
		if (currentControlState)
		{
			Quaternion dir = GetRealTimeViewDirection();
			Vector3 angles = dir.eulerAngles;
			if (angles.x < 180f)
				angles.x = Mathf.Min(50f, angles.x);
			dir = Quaternion.Euler(angles);
			transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * FPRotatingSpeed);
			transform.position = toFollow.position + new Vector3(0f, yOffset + followOffset, 0f);
		}
		else
		{
			AvoidObstacle();
			transform.position = toFollow.position + new Vector3 (0f, yOffset, 0f) + offset * testMagnitude;
			if (isLocked)
				return;
			offset = Quaternion.Euler(0f, deltaPosition.x * Time.deltaTime * swipeRotatingSpeed, 0f) * offset;
			transform.RotateAround(toFollow.position + new Vector3 (0f, yOffset, 0f), Vector3.up, deltaPosition.x * Time.deltaTime * swipeRotatingSpeed);
			double rx = transform.rotation.eulerAngles.x > 180f ? transform.rotation.eulerAngles.x - 360f : transform.rotation.eulerAngles.x;
			if (deltaPosition.y != 0f)
			{
				if (deltaPosition.y > 0f && rx < zRotationMax)
					transform.rotation *= Quaternion.Euler(deltaPosition.y * Time.deltaTime * swipeRotatingSpeed * lookupScaler, 0f, 0f);
				else if (deltaPosition.y < 0f && rx > xRotationMin)
					transform.rotation *= Quaternion.Euler(deltaPosition.y * Time.deltaTime * swipeRotatingSpeed * lookupScaler, 0f, 0f);
			}
		}
	}

	void OnEnable()
	{
		//EasyTouch.On_SwipeStart += CheckSwipePos;
		EasyTouch.On_Swipe += On_TouchStart;
		EasyTouch.On_SwipeEnd += On_TouchEnd;
	}

	#endregion

	#region JoystickMethods

	void EasyJoystick_On_JoystickMoveEnd (MovingJoystick move)
	{
		//isLocked = false;
	}

	void EasyJoystick_On_JoystickMove (MovingJoystick move)
	{
		//isLocked = true;
	}

	void On_TouchStart (Gesture gesture)
	{
		if (gesture.position.x <= Screen.width * swipeEffectiveArea.x || gesture.position.y <= Screen.height * swipeEffectiveArea.y)
		{
			//isLocked = true;
			return;
		}
		deltaPosition.x = gesture.deltaPosition.x * (relativeScreen.x / Screen.width);
		deltaPosition.y = gesture.deltaPosition.y * (relativeScreen.y / Screen.height);
	}

	void On_TouchEnd (Gesture gesture)
	{
		isLocked = false;
		deltaPosition.x = deltaPosition.y = 0f;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Check the start position of the swipe operation. 
	/// </summary>
	/// <param name="gesture">Gesture.</param>
	void CheckSwipePos (Gesture gesture)
	{
		if (gesture.position.x <= Screen.width * swipeEffectiveArea.x || gesture.position.y <= Screen.height * swipeEffectiveArea.y)
		{
			isLocked = true;
			return;
		} 
		else
			isLocked = false;
	}

	/// <summary>
	/// Avoids the Obstacle. Uses Raycast as central algorithm. 
	/// </summary>
	void AvoidObstacle ()
	{
		Vector3 direction = Vector3.Normalize(transform.position - new Vector3 (0f, yOffset, 0f) - toFollow.position);
		RaycastHit hit;
		if (Physics.Raycast(new Ray(toFollow.position + new Vector3 (0f, yOffset, 0f), direction), out hit, originalDistance * testMagnitude))
		{
			testMagnitude = Vector3.Distance(hit.point, toFollow.position + new Vector3 (0f, yOffset, 0f)) / originalDistance;
		}
		else
		{
			if (Physics.Raycast(new Ray(toFollow.position + new Vector3 (0f, yOffset, 0f), direction), out hit, originalDistance))
				return;
			else
				testMagnitude = Mathf.Lerp(testMagnitude, 1f, Time.deltaTime * 3f);
		}
	}

	public void SetCompass (bool state)
	{
		if (state)
			defaultCompassDirection = Input.compass.magneticHeading;
	}

	#endregion
}
