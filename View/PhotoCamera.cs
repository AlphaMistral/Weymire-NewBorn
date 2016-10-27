using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoCamera : MonoBehaviour 
{
	/// <summary>
	/// Only if the screenPosition of the object drops in the area could it be taken. Note that both of the elements are between 0 - 1. 
	/// </summary>
	public Vector2 photoThresold;

	/// <summary>
	/// The current emitting distance of the camera. 
	/// </summary>
	public float currentDistance;

	/// <summary>
	/// The dictionary of the PhotoObject-PhotoObjectShower. It stores whether a certain photoObject is shown or not.
	/// </summary>
	private Dictionary <PhotoObject, PhotoObjectShower> showerDic = new Dictionary <PhotoObject, PhotoObjectShower> ();

	/// <summary>
	/// All pick up items.
	/// </summary>
	private List <PickUpItem> allPickUpItems = new List <PickUpItem> ();

	/// <summary>
	/// All interactables.
	/// </summary>
	private List <InteractableObject> allInteractables = new List <InteractableObject> ();

	/// <summary>
	/// All real pictures.
	/// </summary>
	private List <RealPicture> allRealPictures = new List <RealPicture> ();

	/// <summary>
	/// The Camera Component of the GameObject. 
	/// </summary>
	private Camera _camera;

	/// <summary>
	/// The state of the inventory.
	/// </summary>
	private InventoryState inventoryState;

	/// <summary>
	/// The highlighted object. It is also the object that would be interacted when the photo is taken. 
	/// </summary>
	private PhotoObject highlightedObject;

	/// <summary>
	/// The PhotoProcessor Component of the PhotoCamera.
	/// </summary>
	private PhotoProcessor processor;

	private void Awake ()
	{
		_camera = GetComponent <Camera>();
		processor = GetComponent <PhotoProcessor>();
	}

	/// <summary>
	/// Called when the GameObject is set enabled from disabled.
	/// </summary>
	private void OnEnable ()
	{
		PrepareTakingPhoto();
	}

	/// <summary>
	/// Called when the GameObject is set disabled from enabled. 
	/// </summary>
	private void OnDisable ()
	{
		
	}

	private void Update ()
	{
		DetermineItems();
	}

	/// <summary>
	/// Updates the "ALL" items.
	/// </summary>
	private void UpdateItems ()
	{
		allPickUpItems.Clear();
		GameObject[] tempObjects = GameObject.FindGameObjectsWithTag(Tags.PickUpItem);
		for (int i = 0; i < tempObjects.Length; i++)
		{
			if (tempObjects[i].activeInHierarchy)
				allPickUpItems.Add(tempObjects[i].GetComponent<PickUpItem>());
		}

		allInteractables.Clear();
		tempObjects = GameObject.FindGameObjectsWithTag(Tags.Interactable);
		for (int i = 0; i < tempObjects.Length; i++)
		{
			if (tempObjects[i].activeInHierarchy)
				allInteractables.Add(tempObjects[i].GetComponent<InteractableObject>());
		}

		allRealPictures.Clear();
		tempObjects = GameObject.FindGameObjectsWithTag(Tags.RealPicture);
		for (int i = 0; i < tempObjects.Length; i++)
		{
			if (tempObjects[i].activeInHierarchy)
				allRealPictures.Add(tempObjects[i].GetComponent<RealPicture>());
		}
	}

	/// <summary>
	/// Determines the objects that could be taken. 
	/// </summary>
	private void DetermineItems ()
	{
		float centerDis = 2f;
		PhotoObject tempHighlight = null;

		foreach (PickUpItem item in allPickUpItems)
		{
			Vector2 screenPosition = _camera.WorldToScreenPoint(item.transform.position);
			Consts.ObjectSituation situation = item.DetermineAvailability(inventoryState, screenPosition, photoThresold, currentDistance);
			if (situation != Consts.ObjectSituation.Irrelevant)
			{
				RaycastHit hit;
				if (Physics.Raycast(new Ray(transform.position, Vector3.Normalize(item.transform.position - transform.position)), out hit, currentDistance))
				{
					if (hit.collider.gameObject == item.gameObject)
						situation = Consts.ObjectSituation.OK;
					else
						situation = Consts.ObjectSituation.Irrelevant;
				}
				else
					situation = Consts.ObjectSituation.Irrelevant;
			}
			if (situation != Consts.ObjectSituation.Irrelevant)
			{
				TryNotifyObject(item, screenPosition);
				Vector2 itemPos = new Vector2(screenPosition.x / Screen.width - 0.5f, screenPosition.y / Screen.height - 0.5f);
				if (itemPos.magnitude < centerDis)
				{
					centerDis = itemPos.magnitude;
					tempHighlight = item;
				}
			}
			else
			{
				TryDismissObject(item);
				if (item == highlightedObject)
					highlightedObject = null;
			}
		}

		foreach (InteractableObject item in allInteractables)
		{
			Vector2 screenPosition = _camera.WorldToScreenPoint(item.transform.position);
			Consts.ObjectSituation situation = item.DetermineAvailability(inventoryState, screenPosition, photoThresold, currentDistance);
			if (situation != Consts.ObjectSituation.Irrelevant)
			{
				RaycastHit hit;
				if (Physics.Raycast(new Ray(transform.position, Vector3.Normalize(item.transform.position - transform.position)), out hit, currentDistance))
				{
					if (hit.collider.gameObject == item.gameObject)
						situation = Consts.ObjectSituation.OK;
					else
						situation = Consts.ObjectSituation.Irrelevant;
				}
				else
					situation = Consts.ObjectSituation.Irrelevant;
			}
			if (situation != Consts.ObjectSituation.Irrelevant)
			{
				TryNotifyObject(item, screenPosition);
				Vector2 itemPos = new Vector2(screenPosition.x / Screen.width - 0.5f, screenPosition.y / Screen.height - 0.5f);
				if (itemPos.magnitude < centerDis)
				{
					centerDis = itemPos.magnitude;
					tempHighlight = item;
				}
				else
				{
					TryDismissObject(item);
					if (item == highlightedObject)
						highlightedObject = null;
				}
			}
		}

		foreach (RealPicture pic in allRealPictures)
		{
			Consts.ObjectSituation detResult = pic.DetermineAvailability(inventoryState, new Vector2 (0f, 0f), photoThresold, currentDistance);
			if (detResult == Consts.ObjectSituation.OK)
			{
				foreach (KeyValuePair<PhotoObject, PhotoObjectShower> obj in showerDic)
					TryDismissObject(obj.Key);
				highlightedObject = pic;
				return;
			}
		}

		if (tempHighlight != null && tempHighlight != highlightedObject)
		{
			TryDownlight();
			highlightedObject = tempHighlight;
		}

		if (highlightedObject)
			TryHighlight();
	}

	/// <summary>
	/// Prepares to take an new Photo. 
	/// </summary>
	private void PrepareTakingPhoto ()
	{
		UpdateItems();
	}

	private void TryNotifyObject (PhotoObject obj, Vector2 screenPos)
	{
		if (showerDic.ContainsKey(obj))
		{
			PhotoObjectShower shower = showerDic[obj];
			shower.UpdateShower(screenPos);
		}
		else
		{
			showerDic.Add(obj, UIController.InstantiateNewObjectShower());
			showerDic[obj].UpdateShower(screenPos);
		}
	}

	/// <summary>
	/// Tries to stop showing the object. 
	/// </summary>
	/// <param name="obj">Object.</param>
	private void TryDismissObject (PhotoObject obj)
	{
		if (showerDic.ContainsKey(obj))
		{
			PhotoObjectShower shower = showerDic[obj];
			showerDic.Remove(obj);
			Destroy(shower.gameObject);
		}
	}

	private void TryHighlight ()	
	{
		if (!highlightedObject)
			return;
		if (highlightedObject.GetComponent <RealPicture>())
		{
			return;
		}
		else
		{
			showerDic[highlightedObject].Highlight();
		}
	}

	private void TryDownlight ()
	{
		if (!highlightedObject)
			return;
		if (highlightedObject.GetComponent <RealPicture>())
		{
			return;
		}
		else
		{
			showerDic[highlightedObject].Downlight();
		}
	}

	public void TakePhoto ()
	{
		if (highlightedObject == null)
		{
			return;
		}
		else
		{
			processor.StartCoroutine(processor.ProcessPhotoObject (highlightedObject.GetComponent<PhotoObject> ()));
		}
	}
}

