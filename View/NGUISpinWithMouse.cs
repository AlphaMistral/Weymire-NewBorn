using UnityEngine;
using System.Collections;

public class NGUISpinWithMouse : MonoBehaviour 
{
	#region Public Variables

	/// <summary>
	/// The speed at which the object could rotate if dragged. 
	/// </summary>
	public float rotateSpeed = 0.35f;

	/// <summary>
	/// If the drag ends, the object will continue to rotate in a relatively low speed. 
	/// </summary>
	public float autoSpeed = 0.05f;

	#endregion

	#region Private Attributes

	/// <summary>
	/// The reference to the transform component. 
	/// Someone Says it could be more Efficient that calling for transform ... 
	/// </summary>
	private Transform m_transform;

	/// <summary>
	/// The last delta Position. 
	/// </summary>
	private Vector2 lastDelta;

	/// <summary>
	/// The relative scale for dragging movement. 
	/// </summary>
	private static float relativeScale = 5f / (1920f * 1080f);

	/// <summary>
	/// The delta thresold.
	/// </summary>
	private float deltaThresold;

	/// <summary>
	/// The start up rotating of the selected gameObject. 
	/// </summary>
	private static Vector2 startUpRotating = new Vector2 (0.8f, 0.8f);

	#endregion

	void Start ()
	{
		m_transform = transform; 
		lastDelta = startUpRotating;
		deltaThresold = Screen.width * Screen.height * relativeScale;
	}

	void Update ()
	{
		m_transform.localRotation = Quaternion.Euler(lastDelta.y * autoSpeed, -1f * lastDelta.x * autoSpeed, 0f) * m_transform.localRotation;
		if (lastDelta.magnitude >= deltaThresold)
		{
			Vector2 targetDelta = lastDelta.normalized;
			lastDelta = Vector2.Lerp(lastDelta, targetDelta, 1f * Time.deltaTime);
		}
	}

	void OnDrag (Vector2 delta)
	{
		lastDelta = delta;
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		m_transform.localRotation = Quaternion.Euler(delta.y * rotateSpeed, -1f * delta.x * rotateSpeed, 0f) * m_transform.localRotation;
	}
}
