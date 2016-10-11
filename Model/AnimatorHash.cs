using UnityEngine;
using System.Collections;

/// <summary>
/// The AnimatorHash Class is an extension to Consts Class. It should be executed during runtime to make sure that the values are properly calculated.
/// Currently we will pick all the relevant values up together. Hence it requires a superiror execution order. 
/// In later versions we could determine the variables manually and store them into Consts. 
/// </summary>
public class AnimatorHash : MonoBehaviour 
{
	#region Public Static Variables

	public static int speedFloat;

	#endregion

	#region MonoBehaviour

	private void Awake ()
	{
		speedFloat = Animator.StringToHash("Speed");
	}

	#endregion
}
