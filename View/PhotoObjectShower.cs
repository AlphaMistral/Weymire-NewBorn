using UnityEngine;
using System.Collections;

public class PhotoObjectShower : MonoBehaviour 
{
	/// <summary>
	/// The situation of the shower. It controls whether the shower should exist or not and the content of the label. 
	/// </summary>
	public Consts.ObjectSituation situation;

	/// <summary>
	/// The label which displays the sitatuation of the objectShower.
	/// </summary>
	[SerializeField]
	private UILabel nameLabel;

	/// <summary>
	/// The sprite which indicates the availability of the object. 
	/// </summary>
	[SerializeField]
	private UISprite sprite;

	private void Awake ()
	{
		nameLabel.enabled = false;
		tweener = GetComponent <UIPlayTween>();
	}

	/// <summary>
	/// The NGUI Tweener which controls the NGUI animation of the shower. 
	/// </summary>
	private UIPlayTween tweener;

	/// <summary>
	/// Called when the shower is to be highlighted. 
	/// </summary>
	public void Highlight ()
	{
		tweener.Play(true);
		nameLabel.enabled = true;
	}

	/// <summary>
	/// Called when the shower is no longer the focus of the camera. 
	/// </summary>
	public void Downlight ()
	{
		tweener.Play(false);
		nameLabel.enabled = false;

	}

	/// <summary>
	/// Called to update the label of the shower. 
	/// </summary>
	public void UpdateShower (Vector2 screenPosition)
	{
		//Make sure the UIRoot is Constrained or Constrained on Mobile! 
		transform.localPosition = new Vector3(screenPosition.x / Screen.width * Constant.NGUIResolution.x - Constant.NGUIResolution.x / 2, screenPosition.y / Screen.height * Constant.NGUIResolution.y - Constant.NGUIResolution.y / 2);
	}
}
