using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

//UIController.cs serves as the controller of all the sprites, images, buttons and interactable 2D elements in the game view. Hence it includes UI part and notifier part.
public class UIController : MonoBehaviour 
{
	/// <summary>
	/// The PREFAB of itemShower. Instantiated whenever the gameObject is to be shown. 
	/// </summary>
	public PhotoObjectShower itemShowerPrefab;

	private GameObject uiRoot;
	private GameObject backPack;
	private GameObject gallery;
	private GameObject analyzeSystem;
	private GameObject questLog;

	private static UIController instance;

	[SerializeField]private static GameObject pickUpItemNotifier;
	[SerializeField]private static GameObject interactableNotifier;
	[SerializeField]private static GameObject reminderNotifier;
	[SerializeField]private static GameObject incapableOfInteractNotifier;
	[SerializeField]private static GameObject tooFarNotifier;

	[SerializeField]private static GameObject UGUIRoot;
	[SerializeField]private static GameObject NGUIRoot;

	[SerializeField]private static Camera mainCamera;

	[SerializeField]private UISprite fadeSprite;

	private static float fadeDuration = 1f;
	private static float currentFadeState = 0f;

	public delegate void onFade();
	public static event onFade OnFadeIn;
	public static event onFade OnFadeOut;


	public static GameObject UIRoot
	{
		get
		{
			return instance.uiRoot;
		}
	}

	public static GameObject BackPack 
	{
		get
		{
			return instance.backPack;
		}
	}

	public static GameObject Gallery
	{
		get
		{
			return instance.gallery;
		}
	}

	public static GameObject AnalyzeSystem
	{
		get
		{
			return instance.analyzeSystem;
		}
	}

	public static GameObject QuestLog
	{
		get
		{
			return instance.questLog;
		}
	}

	void Awake ()
	{
		mainCamera = Camera.main;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(this.gameObject);
		}
		DontDestroyOnLoad(this);
	}

	//To be implemented after the UI Controls have been completely determined.
	public static void SetUISituation (Consts.DisplaySetting setting)
	{
		
	}

	/// <summary>
	/// Adds the sprite to the UI Root and provide it with a layer number to make sure that it works fine. 
	/// </summary>
	/// <param name="targetSprite">Target sprite.</param>
	/// <param name="layerNumber">Layer number.</param>
	public static void AddTutorSpriteAndSetLayer (GameObject targetSprite, int layerNumber)
	{
		/// Note that it is still controversy whether I should set the layer number in the prefab, or define it here. 
		/// The tutor window 
	}

	public static PhotoObjectShower InstantiateNewObjectShower ()
	{
		GameObject temp = NGUITools.AddChild(instance.gameObject, instance.itemShowerPrefab.gameObject);
		return temp.GetComponent<PhotoObjectShower>();
	}

	public static void SetFadeState (bool state)
	{
		instance.StartCoroutine(FadeInOut(state));
	}

	public static IEnumerator FadeInOut (bool state)
	{
		yield return new WaitForEndOfFrame();
		currentFadeState = Mathf.Lerp(currentFadeState, state? 0f : 1f, fadeDuration);
		instance.fadeSprite.alpha = currentFadeState;
		if ((state && currentFadeState < 0.05f) || (!state && currentFadeState > 0.95f))
		{
			currentFadeState = state ? 0f : 1f;
			if (state)
				OnFadeOut();
			else
				OnFadeIn();
		}
		else
			yield return new WaitForSeconds(Time.deltaTime);
	}

	public static IEnumerator FP2TPSwitchAnimation()
	{
		yield return new WaitForEndOfFrame();
	}

	public static IEnumerator FadeInOut ()
	{
		yield return new WaitForEndOfFrame();
	}
}
