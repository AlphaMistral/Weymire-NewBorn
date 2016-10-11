using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PrefabController : MonoBehaviour 
{
	public static PrefabController instance;
	private Dictionary <string, GameObject> prefabs;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(this.gameObject);
		}
		DontDestroyOnLoad(this);
		prefabs = new Dictionary<string, GameObject> ();
	}

	/// <summary>
	/// Initialize the prefabs based on the string list in "Prefabs.txt";
	/// </summary>
	private void InitializePrefabs (string fileName)
	{
		string texts = Resources.Load(fileName).ToString();
		string[] names = texts.Split ('\n');
		foreach (string name in names)
		{
			GameObject go = Resources.Load(name, typeof(GameObject)) as GameObject;
			prefabs.Add(name, go);
		}
	}

	/// <summary>
	/// Retrieve the prefab gameObject based on the name provided. If a non-exist object is to be retrieved, a warning would be triggered and an empty gameObject be returned.
	/// </summary>
	/// <returns>The prefab.</returns>
	/// <param name="fileName">File name.</param>
	public GameObject GetPrefab (string fileName)
	{
		if (prefabs.ContainsKey(fileName))
		{
			return prefabs[fileName];
		}
		else
		{
			Debug.Log("No such prefab exists in the resources folder. Please check your spell and the folder. Instead an empty gameObject is returned!");
			return prefabs[Consts.PrefabName.EMPTY];
		}
	}
}
