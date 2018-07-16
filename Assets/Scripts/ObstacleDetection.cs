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

	private bool ready;

	private void Start() {
		rayCastDistance = 10f;
		leftRayBool = false;
		rightRayBool = false;
		layerMask = 1 << 8;
		layerMask = ~layerMask;
	}
	
	public void setRayCastDistance(float newDist) {
		print("Current: " + rayCastDistance);
		rayCastDistance = newDist; //TODO: fix this!!!!
		print("new: " + rayCastDistance);
	}

	public void setEmittersAngle(float angle) {
		float each = angle/2;
		leftEmitter.transform.eulerAngles = new Vector3(0, -each, 0);
		rightEmitter.transform.eulerAngles = new Vector3(0, each, 0);
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
