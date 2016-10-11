using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class TestCoroutine : MonoBehaviour 
{
	//Test has been passed. Conclusion : Coroutine is never controlled by the script, but rather by the gameObject only. 
	//Coroutine and Update are both disabled when the script is disabled. 
	void Start ()
	{
		StartCoroutine(Test());
	}

	void OnEnable ()
	{
		StopAllCoroutines();
		StartCoroutine(Test());
	}

	void OnDisable ()
	{
		StopCoroutine(Test());
	}

	void Update ()
	{
		Debug.Log("Update!");
	}

	IEnumerator Test()
	{
		while (true)
		{
			Debug.Log("test!");
			yield return null;
		}
	}
}
