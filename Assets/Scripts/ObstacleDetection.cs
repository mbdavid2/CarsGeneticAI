using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour {

	private RaycastHit leftRayHit;
	private RaycastHit rightRayHit;

	private bool leftRayBool;
	private bool rightRayBool;

	private float rayCastDistance;

	private void Start() {
		rayCastDistance = 20f;
		leftRayBool = false;
		rightRayBool = false;
	}

	public bool getLeftRayBool() {
		return leftRayBool;
	}

	public bool getRightRayBool() {
		return rightRayBool;
	}

	private void castRay(int layerMask, ref bool rayBool) {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayCastDistance, layerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            rayBool = true;
        }
        else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            rayBool = false;
        }
	}
	
	private void Update() {
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		
		castRay(layerMask, ref leftRayBool);
		castRay(layerMask, ref rightRayBool);
	}
		
}
