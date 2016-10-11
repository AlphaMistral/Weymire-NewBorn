using UnityEngine;
using System.Collections;

public class TestPlayerMovement : MonoBehaviour 
{
	public float movingSpeed;
	public float rotatingSpeed;

	private static float sensitivityThresold = 0.04f;

	private float joyStickValueX;
	private float joyStickValueY;

	public GameObject playerCamera;

	private Transform playerTrans;
	private Animator animator;
	private int speedHash;

	void OnEnable()
	{
		EasyJoystick.On_JoystickMove += EasyJoystick_On_JoystickMove;
		EasyJoystick.On_JoystickTouchUp += EasyJoystick_On_JoystickMoveEnd;
	}

	void EasyJoystick_On_JoystickMoveEnd (MovingJoystick move)
	{
		joyStickValueX = joyStickValueY = 0;
	}

	void OnDisable()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		joyStickValueX = joyStickValueY = 0;
	}

	void OnDestroy()
	{
		EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
		joyStickValueX = joyStickValueY = 0;
	}

	void EasyJoystick_On_JoystickMove (MovingJoystick move)
	{
		joyStickValueX = move.joystickValue.x;
		joyStickValueY = move.joystickValue.y;
		if (Mathf.Abs(joyStickValueX) < sensitivityThresold)
			joyStickValueX = 0f;
		if (Mathf.Abs(joyStickValueY) < sensitivityThresold)
			joyStickValueY = 0f;
	}

	void Awake ()
	{
		playerTrans = playerCamera.transform;
		animator = GetComponent <Animator>();
		speedHash = Animator.StringToHash("Speed");
	}

	void Rotating(float horizontal,float vertical)
	{
		float yAxisRotation = transform.rotation.eulerAngles.y;
		Vector3 targetDirection = new Vector3 (horizontal,0f,vertical);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection,Vector3.up);
		GetComponent<Rigidbody> ().MoveRotation (Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 6f));
	}

	void FixedUpdate ()
	{
		//CharacterController controller = GetComponent <CharacterController>();
		Vector3 originalMove = new Vector3(joyStickValueX, 0f, joyStickValueY);
		Vector3 cameraRotation = playerTrans.rotation.eulerAngles;
		Quaternion targetRotation = Quaternion.Euler(0f, cameraRotation.y, 0f);
		Quaternion toRotate = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
		Vector3 toMove = toRotate * originalMove;
		if (toMove.z != 0f || toMove.x != 0f)
		{
			Rotating(toMove.x, toMove.z);
			animator.SetFloat(speedHash, movingSpeed);
			//controller.Move(new Vector3(toMove.x, 0f, toMove.z) * Time.deltaTime);
		}
		else
			animator.SetFloat(speedHash, 0f);
	}
}
