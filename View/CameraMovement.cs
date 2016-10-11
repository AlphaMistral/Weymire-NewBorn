using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	#region Enumerator Definition

	public enum ControlType
	{
		FirstPerson,
		ThirdPerson,
		SpecialAngle
	};
		
	#endregion

	#region Serialized Variables

	/// <summary>
	/// The Currently Situation of the CameraFollower. 
	/// </summary>
	[SerializeField]
	private ControlType currentControlType;

	/// <summary>
	/// The Offset Value of the Follower. 
	/// </summary>
	[SerializeField]
	private Vector3 offset;

	/// <summary>
	/// Since the PlayerGameObject has its transform origin at his feet ... It would more appropriate to define a singular yOffset to control the obstacle avoidance. 
	/// </summary>
	[SerializeField]
	private float yOffset;

	/// <summary>
	/// The minimums of swipe ratio. 
	/// </summary>
	[SerializeField]
	private float min_swipe_x = 0.4f, min_swipe_y = 0.25f;

	/// <summary>
	/// The size of the virtual screen. Make sure that the script works fine on different devices. 
	/// </summary>
	[SerializeField]
	private Vector2 refScreenSize = new Vector2(800f, 450f);

	/// <summary>
	/// The minimum angle of looking down in FIRST-PERSON mode. 
	/// </summary>
	[SerializeField]
	private float min_lookDown = 50f;

	/// <summary>
	/// The rotating speed of the camera. 
	/// </summary>
	[SerializeField]
	private float cameraRotatingSpeed = 3f;

	/// <summary>
	/// The delta Y of visual angle under THIRD-PERSON. Please note that the first element must be positive while the second one must be negative. 
	/// </summary>
	[SerializeField]
	private Vector2 deltaYInterval = new Vector2 (15f, -8f);

	#endregion

	#region Public References

	public Transform playerTransform;

	#endregion

	#region Private Variables

	/// <summary>
	/// The delta position of the SWIPE Operation on the screen. Please note that it makes senses only if the situation is THIRD PERSON! 
	/// </summary>
	private Vector2 deltaPosition;

	/// <summary>
	/// Is the Player Swipe Operation Allowed. 
	/// </summary>
	private bool isLocked;

	/// <summary>
	/// The default compass direction. Using Magnetic Heading to indicate directions. Hence the value should be clamped to [0f, 360f]. 
	/// </summary>
	private float defaultCompassDirection;

	/// <summary>
	/// The Magnitude of the Offset. From 0f to 1f. Use for obstacle avoidance. 
	/// </summary>
	private float currentOffsetMagnitude;

	/// <summary>
	/// The original Mag of the offse. 
	/// </summary>
	private float originalDistance;

	#endregion

	#region MonoBehaviours

	private void Awake ()
	{
		//Compass Setup

		Input.location.Start();
		Input.compass.enabled = true;
		ResetCompass();

		//Compass Setup

		//Variable Initialization

		originalDistance = Vector3.Magnitude(offset);
		currentOffsetMagnitude = 1f;
		SetCurrentState (ControlType.ThirdPerson);

		//Variable Initialization
	}

	private void OnEnable ()
	{
		EasyTouch.On_Swipe += On_TouchStart;
		EasyTouch.On_SwipeEnd += On_TouchEnd;
	}

	private void OnDisable ()
	{
		EasyTouch.On_Swipe -= On_TouchStart;
		EasyTouch.On_SwipeEnd -= On_TouchEnd;
	}

	private void OnDestroy ()
	{
		EasyTouch.On_Swipe -= On_TouchStart;
		EasyTouch.On_SwipeEnd -= On_TouchEnd;
	}

	private void LateUpdate ()
	{
		ManageMovement();
	}

	#endregion


	#region EasyTouch

	private void On_TouchStart (Gesture gesture)
	{
		if (gesture.position.x <= Screen.width * min_swipe_x || gesture.position.y <= Screen.width * min_swipe_y)
			return;
		deltaPosition.x = gesture.deltaPosition.x * (refScreenSize.x / Screen.width);
		deltaPosition.y = gesture.deltaPosition.y * (refScreenSize.y / Screen.height);
	}

	private void On_TouchEnd (Gesture gesture)
	{
		isLocked = false;
		deltaPosition.x = deltaPosition.y = 0f;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Reset the direction of the compass. Called when the player should re-enter the first-person control mode. 
	/// </summary>
	private void ResetCompass ()
	{
		defaultCompassDirection = Input.compass.magneticHeading;
	}

	/// <summary>
	/// Gets the view direction based on Compass and Acceleration Meter. Only under FIRST-PERSON mode. 
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

	private void AvoidObstacles ()
	{
		Vector3 direction = Vector3.Normalize(transform.position - new Vector3(0f, yOffset, 0f) - playerTransform.position);
		RaycastHit hit;
		if (Physics.Raycast(new Ray(playerTransform.position + new Vector3(0f, yOffset, 0f), direction), out hit, originalDistance * currentOffsetMagnitude))
		{
			currentOffsetMagnitude = Vector3.Distance(hit.point, playerTransform.position + new Vector3(0f, yOffset, 0f)) / originalDistance * 0.8f;
		}
		else
		{
			if (Physics.Raycast(new Ray(playerTransform.position + new Vector3 (0f, yOffset, 0f), direction), out hit, originalDistance * currentOffsetMagnitude * 1.3f))
				return;
			else
				currentOffsetMagnitude = Mathf.Lerp(currentOffsetMagnitude, 1f, Time.deltaTime * cameraRotatingSpeed);
		}
	}

	/// <summary>
	/// Called in LateUpdate Only! Please note that if you call this in Update or FixedUpdate the Player May look Jurky! 
	/// </summary>
	private void ManageMovement ()
	{
		if (currentControlType == ControlType.FirstPerson)
		{
			Quaternion dir = GetRealTimeViewDirection();

			//Make sure that the player couldn't look strainght down. 

			Vector3 angles = dir.eulerAngles;
			if (angles.x < 180f)
				angles.x = Mathf.Min(angles.x, min_lookDown);
			dir = Quaternion.Euler(angles);

			//Make sure that the player couldn't look strainght down. 

			transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * cameraRotatingSpeed);
			transform.position = playerTransform.position + new Vector3(0f, yOffset, 0f);

		}
		else if (currentControlType == ControlType.ThirdPerson)
		{
			//Handle the position of the camera. 

			AvoidObstacles();
			transform.position = playerTransform.position + new Vector3(0f, yOffset, 0f) + offset * currentOffsetMagnitude;

			//Handle the position of the camera. 

			//Handle the rotation of the camera. 

			if (isLocked)
				return;
			offset = Quaternion.Euler(0f, deltaPosition.x, 0f) * offset;
			transform.RotateAround(playerTransform.position + new Vector3(0f, yOffset, 0f), Vector3.up, deltaPosition.x * Time.deltaTime * cameraRotatingSpeed);
			double rx = transform.rotation.eulerAngles.x;
			if (rx > 180f)
				rx -= 360f;
			if ((deltaPosition.y > 0f && rx < deltaYInterval.x) || (deltaPosition.y < 0f && rx > deltaYInterval.y))
				transform.rotation *= Quaternion.Euler(deltaPosition.y * Time.deltaTime * cameraRotatingSpeed, 0f, 0f);
			
			//Handle the rotation of the camera. 
		}
		else
		{
			// Does nothing if it is now under a speical angle. 
			return;
		}
	}

	/// <summary>
	/// Sets current Control type.
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetCurrentState (ControlType type)
	{
		currentControlType = type;
	}

	#endregion
}
