using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSpecialAngle : MonoBehaviour 
{
	#region SequenceElement

	public enum SequenceType
	{
		Position,
		Rotation,
		Simultaneous,
	};

	[System.Serializable]
	public class SequenceElement
	{
		public SequenceType type;
		public Vector3 targetPosition;
		public Vector3 targetEulerAngles;
		public float duration;
	}

	#endregion

	#region Public Attributes

	public List<SequenceElement> components = new List<SequenceElement> ();

	#endregion

	#region Private Variable

	private Camera main_cam;
	private int currentIndex = 0;

	#endregion


	#region MonoBehaviours

	private void Awake ()
	{
		main_cam = Camera.main;
	}

	#endregion
	#region Public Method

	public void Invoke ()
	{
		
	}

	#endregion

	#region Private Method

	private IEnumerator StartCameraRoaming ()
	{
		yield return new WaitForEndOfFrame();
		yield return StartCoroutine(UIController.FadeInOut (false));
		SequenceElement se = components[currentIndex];
		while (true)
		{
			if (currentIndex >= components.Count)
				break;
			if (se.type == SequenceType.Position || se.type == SequenceType.Simultaneous)
				main_cam.transform.position = Vector3.Lerp (main_cam.transform.position, se.targetPosition, 1.0f / se.duration * Time.deltaTime);
			if (se.type == SequenceType.Rotation || se.type == SequenceType.Simultaneous)
				main_cam.transform.rotation = Quaternion.Slerp(main_cam.transform.rotation, Quaternion.Euler(se.targetEulerAngles), 1.0f / se.duration * Time.deltaTime);
			if (se.type == SequenceType.Position)
			{
				if (Vector3.Distance(se.targetPosition, main_cam.transform.position) < 0.5f)
				{
					main_cam.transform.position = se.targetPosition;
					currentIndex++;
					yield return new WaitForSeconds(Time.deltaTime);
				}
			}
			else if (se.type == SequenceType.Rotation)
			{
				if (Vector3.Distance(se.targetEulerAngles, main_cam.transform.rotation.eulerAngles) < 5f)
				{
					main_cam.transform.rotation = Quaternion.Euler(se.targetEulerAngles);
					currentIndex++;
					yield return new WaitForSeconds(Time.deltaTime);
				}
			}
			else
			{
				if (Vector3.Distance(se.targetEulerAngles, main_cam.transform.rotation.eulerAngles) < 5f && Vector3.Distance(se.targetPosition, main_cam.transform.position) < 0.5f)
				{
					main_cam.transform.position = se.targetPosition;
					main_cam.transform.rotation = Quaternion.Euler(se.targetEulerAngles);
					currentIndex++;
					yield return new WaitForSeconds(Time.deltaTime);
				}
			}
		}
		yield return StartCoroutine (UIController.FadeInOut(true));
	}

	#endregion
}
