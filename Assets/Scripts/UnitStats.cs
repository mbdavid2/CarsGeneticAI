﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {

	private Vector3 lastPosition;
	private float coveredDistance;
	private float startTime;
	private float elapsedTime;

	private void Start () {
		coveredDistance = -75;
		elapsedTime = 0;
		startTime = Time.time;
	}

	private void calculateCoveredDistance () {
		//float dir = Input.GetAxis("Vertical");
		Vector3 currPosition = transform.position;
		float inner = Mathf.Pow(currPosition.x - lastPosition.x, 2) + Mathf.Pow(currPosition.y - lastPosition.y, 2) + Mathf.Pow(currPosition.z - lastPosition.z, 2);
		coveredDistance += Mathf.Sqrt(inner);
		lastPosition = currPosition;
	}

	private void calculateElapsedTime () {
		elapsedTime = Time.time - startTime;
	}

	private void Update () {
		calculateCoveredDistance();
		calculateElapsedTime();
	}

	private void resetCounters () {
		coveredDistance = -75;
		elapsedTime = 0;
		startTime = Time.time;
	}

	public float getElapsedTime() {
		return elapsedTime;
	}

	public float getCoveredDistance() {
		return coveredDistance;
	}

}
