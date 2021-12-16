using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class EggsSpawner : MonoBehaviour {

	public GameObject chick;
	public GameObject eggPrefab;

	[SerializeField] TrackableEventHandler imageFinder;

	[SerializeField]
	[Tooltip("Time between two spawns in seconds.")]
	[Min(0.0f)]
	float spawnPeriod = 5;  // in seconds
	public float SpawnPeriod {
		get {
			return spawnPeriod;
		}

		set {
			spawnPeriod = value < 0.0f ? 0.0f : value;
		}
	}

	float prevSpawnTime = 0;
	bool skipOneSpawn = true;
	List<Rigidbody> eggsRigidbodies = new List<Rigidbody>();


	void Awake() {
		Assert.IsNotNull(chick, "Chick was not assigned in inspector.");
		Assert.IsNotNull(eggPrefab, "Egg Prefab was not assigned in inspector.");
		Assert.IsNotNull(imageFinder, "Image Finder was not assigned in inspector.");

		enabled = false;

		imageFinder.ImageFound.AddListener(() => {
			enabled = true;
			foreach (Rigidbody eggRigidbody in eggsRigidbodies) {
				eggRigidbody.isKinematic = false;
			}
			skipOneSpawn = true;
		});

		imageFinder.ImageLost.AddListener(() => {
			enabled = false;
			foreach (Rigidbody eggRigidbody in eggsRigidbodies) {
				eggRigidbody.isKinematic = true;
			}
		});
	}

	void Update() {
		if (Time.time - prevSpawnTime > spawnPeriod) {
			if (skipOneSpawn) {
				skipOneSpawn = false;
			} else {
				GameObject egg = Instantiate(eggPrefab, transform.parent);
				Vector3 position =
					new Vector3(chick.transform.position.x, egg.transform.position.y, chick.transform.position.z);
				egg.transform.position = position;

				Rigidbody rigidbody = egg.GetComponent<Rigidbody>();
				Assert.IsNotNull(rigidbody, "No Rigidbody on egg.");
				eggsRigidbodies.Add(rigidbody);
			}

			prevSpawnTime = Time.time;
		}
	}
}
