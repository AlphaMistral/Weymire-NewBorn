using UnityEngine;
using System.Collections;

/// <summary>
/// Tutor window is an abstradt class. Specific tutor systems have different definitions and operations.
/// For example, when it comes to the backPack, the player is not allowed to touch or drag the grid when he is tutored to equip an item. 
/// Now a new idea is generated ... why not integrate the tutor window in the corresponding systems?
/// Player may have to operate over different systems. e.g. : first in the UIRoot, then in th backpack. 
/// </summary>
public abstract class TutorWindow : MonoBehaviour 
{
	/// <summary>
	/// The main problem is how to access the transform in the scene from the prefab ... 
	/// A solution is through the tutorManager in the scene. After all the tutor Manager is always in the scene. 
	/// </summary>

	public enum Target
	{
		UIRoot,
		BackPack,
		AnalyzeSystem,
		Gallery,
		QuestLog
	};

	public Target targetSelection;

	public GameObject targetTutorCentor;

	/// <summary>
	/// Don't forget to base (); in inherited classes ... 
	/// </summary>
	protected virtual void Awake ()
	{
		switch (targetSelection)
		{
			case Target.UIRoot: 
				targetTutorCentor = UIController.UIRoot;
				break;
			case Target.BackPack:
				targetTutorCentor = UIController.BackPack;
				break;
			case Target.AnalyzeSystem:
				targetTutorCentor = UIController.AnalyzeSystem;
				break;
			case Target.Gallery:
				targetTutorCentor = UIController.Gallery;
				break;
			case Target.QuestLog:
				targetTutorCentor = UIController.QuestLog;
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// The event is triggered when the sprite it clicked. 
	/// </summary>
	public virtual void OnClick ()
	{
		
	}

	/// <summary>
	/// The event is triggered when the specified target is clicked. Rather than the sprite itself. 
	/// </summary>
	public virtual void OnTargetClick ()
	{
		
	}

}
