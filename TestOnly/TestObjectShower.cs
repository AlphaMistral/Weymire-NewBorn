using UnityEngine;
using System.Collections;

public class TestObjectShower : MonoBehaviour 
{
	public Transform worldTarget;

	public Transform uiTarget;

	public UISprite sprite;

	public Camera camera;

	private GameObject instance;

	void Awake ()
	{
		instance = NGUITools.AddChild(uiTarget.gameObject, sprite.gameObject);
	}

	void Update ()
	{
		Vector3 targetPosition = camera.WorldToScreenPoint(worldTarget.position);
		Vector3 nguiPosition = new Vector3((targetPosition.x / Screen.width) * 1280.0f - 640f, (targetPosition.y / Screen.height) * 720.0f - 360f, 0f);
		instance.transform.localPosition = nguiPosition;
	}
}
