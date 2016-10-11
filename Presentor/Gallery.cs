using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Gallery : MonoBehaviour 
{
	//Arbitrarily set the static instance of this class to public, since the funcions implemented below often interact with the variables. 
	public static Gallery instance;

	[SerializeField]private UITexture mainTexure;
	[SerializeField]private UIButton lastButton;
	[SerializeField]private UIButton nextButton;
	[SerializeField]private UIButton deleteButton;
	[SerializeField]private UIButton saveButton;
	[SerializeField]private UIButton backButton;

	private int imageNumber;
	private int currentIndex;

	private int CurrentIndex
	{
		get
		{
			return currentIndex;
		}
		set
		{
			currentIndex = value;
			if (currentIndex == imageNumber)
				nextButton.isEnabled = false;
			if (currentIndex <= 1)
				lastButton.isEnabled = false;
		}
	}

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		if (DialogueLua.GetVariable(Consts.VariableName.galleryImageNum).AsInt == 0)
			CurrentIndex = 0;
		else
			CurrentIndex = 1;
	}

	public void DeleteCurrentImage ()
	{
		if (CurrentIndex == imageNumber)
		{
			CurrentIndex--;
		}
		imageNumber--;
		StartCoroutine(InfoSaver.GalleryDeleteImageByID(mainTexure, CurrentIndex));
	}

	public IEnumerator AddNewImageByScreenShot ()
	{
		yield return new WaitForEndOfFrame();
		InfoSaver.TakeScreenShot();
		imageNumber++;
		if (CurrentIndex == 0)
		{
			CurrentIndex = 1;
			InfoSaver.GetGalleryTextureByID(mainTexure, currentIndex);
		}
	}

	public void NextImage ()
	{
		if (CurrentIndex == imageNumber)
			return;
		CurrentIndex++;
		InfoSaver.GetGalleryTextureByID(mainTexure, CurrentIndex);
	}

	public void LastImage ()
	{
		if (CurrentIndex <= 1)
			return;
		CurrentIndex--;
		InfoSaver.GetGalleryTextureByID(mainTexure, CurrentIndex);
	}

	//To be implemented after successfully figuring out Unity-iPhone.xcodeProj!
	public void SaveToSystem ()
	{
		
	}
}
