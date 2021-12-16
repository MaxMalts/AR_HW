using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class PlayerController : MonoBehaviour {

	public float walkForceCoefficient = 100;
	public float maxSpeed = 10;
	public float jumpForce = 10;

	[SerializeField] TrackableEventHandler imageFinder;

	const string jumpStateName = "Jump In Place";

	new Rigidbody rigidbody;
	Animator animator;
	int jumpStateNameHash;

	bool isWalking = false;
	Vector3 walkDirection;
	bool canJump = true;


	public void OnMove(Vector2 direction) {
		if (!direction.Equals(Vector2.zero)) {
			isWalking = true;

			walkDirection = direction;
			walkDirection.z = walkDirection.y;
			walkDirection.y = 0;

			animator.SetBool("Walk", true);

		} else {
			isWalking = false;
			animator.SetBool("Walk", false);
		}
	}


	public void OnJump() {
		if (canJump &&
			(animator.GetCurrentAnimatorStateInfo(0).shortNameHash != jumpStateNameHash ||
			animator.IsInTransition(0))) {

			rigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			canJump = false;
			animator.SetTrigger("Jump");
		}
	}


	void OnCollisionEnter(Collision collistionInfo) {
		canJump = true;
	}


	void Awake() {
		animator = GetComponent<Animator>();
		Assert.IsNotNull(animator, "No Animator component on player.");

		rigidbody = GetComponent<Rigidbody>();
		Assert.IsNotNull(rigidbody, "No Rigidbody component on player.");

		Assert.IsNotNull(imageFinder, "Image Finder was not assigned in inspector.");

		jumpStateNameHash = Animator.StringToHash(jumpStateName);
	}

	void Start() {
		imageFinder.ImageFound.AddListener(() => rigidbody.isKinematic = false);
		imageFinder.ImageLost.AddListener(() => rigidbody.isKinematic = true);
		rigidbody.isKinematic = true;
	}


	void FixedUpdate() {
		if (isWalking) {
			transform.rotation = Quaternion.LookRotation(walkDirection);

			if (new Vector2(rigidbody.velocity.x, rigidbody.velocity.z).magnitude < maxSpeed) {
				rigidbody.AddForce(walkDirection * walkForceCoefficient);
				rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
			}
		}
	}
}