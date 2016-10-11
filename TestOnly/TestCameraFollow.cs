using UnityEngine;
using System.Collections;

public class TestCameraFollow : MonoBehaviour 
{
	public bool currentControlState; // true means first person while false means third person. 
	public Transform toFollow;

	public Vector3 offset;

	public float yOffset;

	private Vector2 deltaPosition;
	private bool isLocked;

	private float originalDistance;
	private float testMagnitude;
	private float defaultCompassDirection;

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

	void LateUpdate ()
	{
		if (currentControlState)
		{
			Quaternion dir = GetRealTimeViewDirection();
			Vector3 angles = dir.eulerAngles;
			if (angles.x < 180f)
			angles.x = Mathf.Min(50f, angles.x);
			dir = Quaternion.Euler(angles);
			transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 3f);
			transform.position = toFollow.position + new Vector3 (0f, yOffset + 0.5f, 0f);
			return;
		}
		AvoidObstacle1();
		transform.position = toFollow.position + new Vector3 (0f, yOffset, 0f) + offset * testMagnitude;
		if (isLocked)
			return;
		offset = Quaternion.Euler(0f, deltaPosition.x / 3, 0f) * offset;
		transform.RotateAround(toFollow.position + new Vector3 (0f, yOffset, 0f), Vector3.up, deltaPosition.x / 3);
		double rx = transform.rotation.eulerAngles.x > 180f ? transform.rotation.eulerAngles.x - 360f : transform.rotation.eulerAngles.x;
		if (deltaPosition.y != 0f)
		{
			if (deltaPosition.y > 0f && rx < 15f)
				transform.rotation *= Quaternion.Euler(deltaPosition.y / 5f, 0f, 0f);
			else if (deltaPosition.y < 0f && rx > -8f)
				transform.rotation *= Quaternion.Euler(deltaPosition.y / 5f, 0f, 0f);
		}
	}

	void OnEnable()
	{
		//EasyTouch.On_SwipeStart += CheckSwipePos;
		EasyTouch.On_Swipe += On_TouchStart;
		EasyTouch.On_SwipeEnd += On_TouchEnd;
	}

	void EasyJoystick_On_JoystickMoveEnd (MovingJoystick move)
	{
		//isLocked = false;
	}

	void EasyJoystick_On_JoystickMove (MovingJoystick move)
	{
		//isLocked = true;
	}

	void CheckSwipePos (Gesture gesture)
	{
		if (gesture.position.x <= Screen.width / 2.5 || gesture.position.y <= Screen.height / 4)
		{
			isLocked = true;
			return;
		} 
		else
			isLocked = false;
	}

	void On_TouchStart (Gesture gesture)
	{
		if (gesture.position.x <= Screen.width / 2.5 || gesture.position.y <= Screen.height / 4)
		{
			//isLocked = true;
			return;
		}
		deltaPosition.x = gesture.deltaPosition.x * (1920f / Screen.width);
		deltaPosition.y = gesture.deltaPosition.y * (1080f / Screen.height);
	}

	void On_TouchEnd (Gesture gesture)
	{
		isLocked = false;
		deltaPosition.x = deltaPosition.y = 0f;
	}

	void AvoidObstacle1 ()
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
}
