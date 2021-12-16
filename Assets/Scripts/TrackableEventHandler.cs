using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class TrackableEventHandler : DefaultTrackableEventHandler {

	public UnityEvent ImageFound { get; private set; } = new UnityEvent();
	public UnityEvent ImageLost { get; private set; } = new UnityEvent();


	protected override void OnTrackingFound() {
		base.OnTrackingFound();

		ImageFound.Invoke();
	}


	protected override void OnTrackingLost() {
		base.OnTrackingLost();

		ImageLost.Invoke();
	}
}
