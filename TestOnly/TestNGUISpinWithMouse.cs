using UnityEngine;
using System.Collections;

public class TestNGUISpinWithMouse : MonoBehaviour 
{

	public float rotateSpeed = 0.7f;
	public float autoSpeed = 0.1f;

	private Transform trans;

	private Vector2 lastDelta;

	void Start ()
	{
		trans = transform;
		lastDelta = new Vector2(0.8f, 0.8f);
	}

	void Update ()
	{
		trans.localRotation = Quaternion.Euler(0.5f * lastDelta.y * autoSpeed, -0.5f * lastDelta.x * autoSpeed, 0f) * trans.localRotation;
		if (lastDelta.magnitude >= 5f)
		{
			Vector2 targetDelta = lastDelta.normalized * 3;
			lastDelta = Vector2.Lerp(lastDelta, targetDelta, 1f * Time.deltaTime);
		}
	}

	void OnDrag (Vector2 delta)
	{
		lastDelta = delta;
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		trans.localRotation = Quaternion.Euler(0.5f * delta.y * rotateSpeed, -0.5f * delta.x * rotateSpeed, 0f) * trans.localRotation;
	}
}
