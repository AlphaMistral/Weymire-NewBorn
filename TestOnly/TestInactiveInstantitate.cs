using UnityEngine;
using System.Collections;

public class TestInactiveInstantitate : MonoBehaviour 
{
	//Result of the test : 
	//The function of an inactive gameObject Could NOT be called. 
	public GameObject prefab;

	void Awake ()
	{
		gameObject.SetActive(false);
	}

	public void Instantiate ()
	{
		GameObject go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
		go.transform.parent = transform;
	}
}
