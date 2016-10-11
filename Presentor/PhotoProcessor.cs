using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoProcessor : MonoBehaviour 
{
	/// <summary>
	/// Analyze the list of photoObjects and try to interact with them. If certain circumstances are met, it will also call to take a screenshot and save it into the gallery.
	/// </summary>
	/// <returns>The photo objects.</returns>
	/// <param name="po">Po.</param>
	public IEnumerator ProcessPhotoObject (PhotoObject po)
	{
		yield return null;
	}

	/// <summary>
	/// Take screenShot and save the image to the Gallery.
	/// </summary>
	private void TakeScreenShot ()
	{
		
	}
}
