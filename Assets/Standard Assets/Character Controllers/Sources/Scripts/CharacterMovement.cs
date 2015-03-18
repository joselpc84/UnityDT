using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	private CharacterController controller;
	public float speed = 6.0f;
	public float turnSpeed = 60.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		
		if(controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed; //Player Input
			anim.SetFloat ("speed", controller.velocity.magnitude);
			//moveDirection = transform.TransformDirection(moveDirection);
		}
		controller.Move(moveDirection * Time.deltaTime);
		
		//Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.tag == "boulder") {
			hit.rigidbody.AddForce (transform.forward * speed);
		}
	}

}
