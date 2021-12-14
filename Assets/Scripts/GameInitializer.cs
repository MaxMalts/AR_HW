using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class GameInitializer : MonoBehaviour {

	[SerializeField] GameObject canvas;
	[SerializeField] GameObject walkScreenButtons;
	[SerializeField] GameObject cluckButton;

	[SerializeField] PlayerController playerController;
	[SerializeField] CluckController cluckController;


	void Awake() {
		Assert.IsNotNull(canvas, "Canvas was not assigned in inspector.");
		Assert.IsNotNull(walkScreenButtons, "Walk Screen Buttons prefab was not assigned in inspector.");
		Assert.IsNotNull(cluckButton, "Cluck Button prefab was not assigned in inspector.");
		Assert.IsNotNull(playerController, "Player Controller was not assigned in inspector.");

		//#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		WalkButtonsEventsGenerator generator =
			Instantiate(walkScreenButtons, canvas.transform).GetComponent<WalkButtonsEventsGenerator>();
		generator.axisChanged.AddListener(playerController.OnMove);

		CorrectedButton cluck =
			Instantiate(cluckButton, canvas.transform).GetComponent<CorrectedButton>();
		cluck.onClick.AddListener(cluckController.OnCluck);
//#endif
	}
}
