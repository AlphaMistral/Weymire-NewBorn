using UnityEngine;
using System.Collections;

public class TestQiangxingHoeverEvent : MonoBehaviour 
{
	private UISprite sprite;

	void Awake ()
	{
		sprite = GetComponent <UISprite>();
	}

	void Start ()
	{
		Debug.Log(Screen.width);
		UICamera.OnPosChange += ORZ;
	}

	public void ORZ (Vector2 pos)
	{
		float xmin = transform.localPosition.x - sprite.width / 2;
		float xmax = transform.localPosition.x + sprite.width / 2;
		xmin = (xmin + 640f) * Screen.width / 1280f;
		xmax = (xmax + 640f) * Screen.width / 1280f;
		if (pos.x <= xmax && pos.x >= xmin)
			Debug.Log("Fucked!");
	}
}
