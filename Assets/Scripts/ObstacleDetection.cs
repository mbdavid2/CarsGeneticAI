using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour {

	private RaycastHit leftRayHit;
	private RaycastHit rightRayHit;

	private bool leftRayBool;
	private bool rightRayBool;

	public GameObject leftEmitter;
	public GameObject rightEmitter;

	private float rayCastDistance;

	private int layerMask;

	private void Start() {
		rayCastDistance = 20f;
		leftRayBool = false;
		rightRayBool = false;
		layerMask = 1 << 8;
		layerMask = ~layerMask;
	}

	public bool getLeftRayBool() {
		castRay(layerMask, ref leftRayBool, leftEmitter);
		return leftRayBool;
	}

	public bool getRightRayBool() {
		castRay(layerMask, ref rightRayBool, rightEmitter);
		return rightRayBool;
	}

	private void castRay(int layerMask, ref bool rayBool, GameObject emitter) {
		RaycastHit hit;
		if (Physics.Raycast(emitter.transform.position, emitter.transform.TransformDirection(Vector3.forward), out hit, rayCastDistance, layerMask)) {
            Debug.DrawRay(emitter.transform.position, emitter.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            rayBool = true;
        }
        else {
            Debug.DrawRay(emitter.transform.position, emitter.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            rayBool = false;
        }
	}
	
	/*private void FixedUpdate() {
	}*/
		
}
