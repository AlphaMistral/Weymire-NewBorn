using UnityEngine;
using System.Collections;

public class TestCameraRoaming : MonoBehaviour 
{
	public Transform[] waypoints;
	public float roamingSpeed = 5f;

	private int currentIndex;
	private Transform targetTransform;

	void Awake ()
	{
		currentIndex = 0;
		targetTransform = waypoints[0];
	}

	void Update ()
	{
		if (CheckDistance())
		{
			currentIndex++;
			if (currentIndex >= waypoints.Length)
				currentIndex = 0;
			targetTransform = waypoints[currentIndex];
		}
		transform.position = Vector3.Slerp(transform.position, targetTransform.position, roamingSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, roamingSpeed * Time.deltaTime / 5f);
	}

	private bool CheckDistance ()
	{
		return Vector3.Distance(transform.position, targetTransform.position) < 1f;
	}
}
