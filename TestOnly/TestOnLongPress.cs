using UnityEngine;
using System.Collections;

public class TestOnLongPress : MonoBehaviour 
{
	public UICamera uiCamera;

	public TestEventHoster hoster;

	private Vector2 storedPosition;

	private bool isOnLongPress;
	private bool isLongPressHolding;
	private float duration = 0f;

	public void OnLongPress1 ()
	{
		Debug.Log("sadfgasf!");
	}

	void Awake ()
	{
		storedPosition = new Vector2(0f, 0f);
	}

	void OnEnable ()
	{
		UICamera.OnPosChange += Log1;
	}

	void OnDisable ()
	{
		UICamera.OnPosChange -= Log1;
	}

	void Start ()
	{
		
	}

	public void OnPress (bool state)
	{
		if (isLongPressHolding)
			isOnLongPress = false;
		else if (state)
			isOnLongPress = true;
		else
		{
			isOnLongPress = false;
			duration = 0f;
			isLongPressHolding = false;
		}
	}

	void Update ()
	{
		//hoster.FuckEvent += Log;
		if (isOnLongPress)
			duration += Time.deltaTime;
		if (duration >= 0.3f && isOnLongPress)
		{
			OnLongPress1();
			isLongPressHolding = true;
		}
	}

	public void Log (Vector2 pos)
	{
		if (Vector2.Equals(pos, storedPosition))
			return;
		storedPosition = pos;
		Debug.Log(pos);
	}

	public void Log1 (Vector2 pos)
	{
		
	}

	public void Log2 (Vector2 pos)
	{
		Debug.Log(pos.magnitude);
	}
}
