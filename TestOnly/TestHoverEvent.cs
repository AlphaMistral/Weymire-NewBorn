using UnityEngine;
using System.Collections;

public class TestHoverEvent : MonoBehaviour 
{
	public void OnHover (bool isOver)
	{
		if (isOver)
			Debug.Log("Hovered!");
		else
			Debug.Log("Gali Geigei!");
	}
}
