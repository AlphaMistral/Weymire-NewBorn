using UnityEngine;
using System.Collections;

public class TestAddWord : MonoBehaviour 
{
	private int count = 0;

	void Update()
	{
		if (count >= 100)
			return;
		GetComponent<UILabel> ().text += "a";
		count++;
	}
}
