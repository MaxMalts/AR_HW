using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class CameraPositioner : MonoBehaviour {

	[SerializeField] new Camera camera;


	void Awake() {
		Assert.IsNotNull(camera, "Camera not assigned in inspector.");
	}


	void Update() {
		transform.position = -camera.transform.position;
		transform.rotation = Quaternion.Inverse(camera.transform.rotation);

		camera.transform.position = new Vector3(0, 0, 0);
		camera.transform.rotation = new Quaternion();
	}
}
