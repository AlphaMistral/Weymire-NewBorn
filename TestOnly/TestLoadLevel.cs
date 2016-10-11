using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestLoadLevel : MonoBehaviour 
{
	public int levelNumber;

	public void LoadLevel ()
	{
		SceneManager.LoadScene(levelNumber);
	}
}
