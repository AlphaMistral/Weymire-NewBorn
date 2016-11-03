using UnityEngine;
using System.Collections;

public class MessageSystem : MonoBehaviour 
{
	#region Delegate Area

	public delegate void Delegate_Void ();

	/// <summary>
	/// Event Arised When a new Message is Unlocked. 
	/// </summary>
	public event Delegate_Void OnMessageGot;

	#endregion

	#region Public Variables



	#endregion

	#region Private Components



	#endregion

	#region MonoBehaviours



	#endregion
}
