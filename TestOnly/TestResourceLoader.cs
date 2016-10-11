using UnityEngine;
using System.Collections;


//Test Success. Please make sure that the name of the prefab is dependent on "Resources" Folder in Unity.
public class TestResourceLoader : MonoBehaviour 
{
	void Awake ()
	{
		GameObject go = Instantiate (Resources.Load ("Cube (1)", typeof(GameObject)), new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
	}
}
