using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;



public class WalkButtonsEventsGenerator : MonoBehaviour {

	[SerializeField] CorrectedButton buttonForward;
	[SerializeField] CorrectedButton buttonBack;
	[SerializeField] CorrectedButton buttonLeft;
	[SerializeField] CorrectedButton buttonRight;

	[Serializable]
	public class AxisChangedEvent : UnityEvent<Vector2> { }
	public AxisChangedEvent axisChanged = new AxisChangedEvent();

	Vector2 curAxis;


	void Awake() {
		Assert.IsNotNull(buttonForward, "Button Forward was not assigned in inspector.");
		Assert.IsNotNull(buttonBack, "Button Back was not assigned in inspector.");
		Assert.IsNotNull(buttonLeft, "Button Left was not assigned in inspector.");
		Assert.IsNotNull(buttonRight, "Button Right was not assigned in inspector.");
	}

	void Start() {
		buttonForward.buttonDown.AddListener(() => {
			curAxis.y += 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonBack.buttonDown.AddListener(() => {
			curAxis.y -= 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonLeft.buttonDown.AddListener(() => {
			curAxis.x -= 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonRight.buttonDown.AddListener(() => {
			curAxis.x += 1.0f;
			axisChanged.Invoke(curAxis);
		});

		buttonForward.buttonUp.AddListener(() => {
			curAxis.y -= 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonBack.buttonUp.AddListener(() => {
			curAxis.y += 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonLeft.buttonUp.AddListener(() => {
			curAxis.x += 1.0f;
			axisChanged.Invoke(curAxis);
		});
		buttonRight.buttonUp.AddListener(() => {
			curAxis.x -= 1.0f;
			axisChanged.Invoke(curAxis);
		});
	}
}
