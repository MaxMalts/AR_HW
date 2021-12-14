using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class CluckController : MonoBehaviour {

	AudioSource audioSource;


	public void OnCluck() {
		if (audioSource != null) {
			audioSource.Play();
		}
	}


	void Awake() {
		audioSource = GetComponent<AudioSource>();
		Assert.IsNotNull(audioSource, "No AudioSource on cluck GameObject.");
	}
}
