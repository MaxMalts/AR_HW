using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class PlayerController : MonoBehaviour {

	public float walkSpeed = 10;

	Animator animator;

	bool isWalking = false;
	Vector3 walkDirection;

	Vector3 camRotation;
	Vector3 camOffset;


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


	void Awake() {
		animator = GetComponent<Animator>();
		Assert.IsNotNull(animator, "No Animator component on player.");
	}


	void FixedUpdate() {
		if (isWalking) {
			transform.localPosition += walkDirection * walkSpeed;
		}
	}
}
