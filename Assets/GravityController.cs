using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravityController : MonoBehaviour {

	public float gravityForce = 9.8f;


	void Update() {
		Physics.gravity = -transform.up * gravityForce;
	}
}
