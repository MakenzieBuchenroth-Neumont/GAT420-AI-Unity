using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class AI_SpherecastPerception : AI_Perception {
	[SerializeField][Range(2, 50)] int numRaycast = 2;
	[SerializeField][Range(0.1f, 5)] float radius = 0.5f;

	public void OnDrawGizmos() {
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
		foreach (Vector3 direction in directions) {
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(ray.origin + ray.direction * distance, radius);
		}
	}

	public override GameObject[] getGameObjects() {
		List<GameObject> result = new List<GameObject>();

		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
		foreach (Vector3 direction in directions) {
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			if (Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance)) {
			Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
				// check if collision is self, skip if so
				if (raycastHit.collider.gameObject == gameObject) continue;
				if (tagName == "" || raycastHit.collider.CompareTag(tagName)) {
					result.Add(raycastHit.collider.gameObject);
				}
			}
			else {
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
			}
		}
		// remove duplicates
		return result.Distinct().ToArray();
	}

	public bool GetOpenDirection(ref Vector3 openDirection) {
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
		foreach (var direction in directions) {
			// cast ray from transform position towards direction (use game object orientation)
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			// if there is NO raycast hit then that is an open direction
			if (!Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance, layerMask)) {
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
				// set open direction
				openDirection = ray.direction;
				return true;
			}
		}

		// no open direction
		return false;
	}

	public bool CheckDirection(Vector3 direction) {
		// create ray in direction (use game object orientation)
		Ray ray = new Ray(transform.position, transform.rotation * direction);
		// check ray cast
		return Physics.SphereCast(ray, radius, distance, layerMask);

	}
}
