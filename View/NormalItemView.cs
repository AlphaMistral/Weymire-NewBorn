using UnityEngine;
using System.Collections;

public class NormalItemView : ItemShower 
{
	public delegate void onConfirmed ();
	public event onConfirmed OnConfirmed;

	public UILabel nameLabel;
	public UILabel introductionLabel;
	public UISprite sprite;
	public UIButton confirmButton;

	public Transform target;

	private UIPlayTween tweener;
	private TweenPosition tweenTrans;

	private void Awake ()
	{
		type = ShowType.Normal;
		confirmButton.onClick.Add(new EventDelegate (this, "OnButtonClick"));
		tweener = GetComponent <UIPlayTween>();
	}

	private void OnEnable ()
	{
		tweener.Play(true);
	}

	private void OnButtonClick ()
	{
		OnConfirmed += dd;
		OnConfirmed();
		tweener.Play(false);
		tweenTrans = gameObject.AddComponent <TweenPosition>();
		tweenTrans.from = transform.localPosition;
		tweenTrans.to = target.localPosition;
		tweenTrans.duration = 0.3f;
		Destroy(gameObject, 0.33f);
	}

	public void InitializeWithItem (Item item)
	{
		
	}

	private void dd ()
	{
		//Debug.Log("sdafasdf");
	}
}
