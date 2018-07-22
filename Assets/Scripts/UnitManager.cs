using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	private int[] unitCodification;

	public Rigidbody rb;

	private void Start() {
    	initNewUnit();
    }

    private void initNewUnit() {
    	//Basic
    	prepareCarStartPosition();

    	//Genetic
    	unitCodification = GetComponent<GeneticManager>().getNewUnitCodification();
    	initParameters(unitCodification);
    	
    	GetComponent<UnitStats>().resetCounters(); //Resets time, distance, etc
    	GetComponent<UnitAI>().resetUnit(); //Resets steer, motor, etc
    }

    private void prepareCarStartPosition() {
    	transform.position = new Vector3(0f, -0.31f, 75.2f);
    	transform.eulerAngles = new Vector3(0, 0, 0);
    }

    /*
    Sets up permanent behaviours:
    - Raycast distance
    - Angle of the two rays
    */
    private void initParameters(int[] unitCodification) {
    	int angle = unitCodification[0];
    	setAngle(angle);
    	int dist = unitCodification[1];
    	setRayDistance(dist);
    	print("This unit has angle: " + angle + ", distance detection: " + dist);
    }
    
    /*
    Sets the angle between the two rays
    */
    private void setAngle(int code) {
    	GetComponent<ObstacleDetection>().setEmittersAngle(code*10);
    }

    /*
    Sets the maximum reach of the ray. Example:
    	Code = 3; -> RayCastDistance = 30f;
    */
    private void setRayDistance(int code) {
    	GetComponent<ObstacleDetection>().setRayCastDistance(code*10);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "GroundTrackVerges") {
        	GetComponent<UnitAI>().stop();
        	storeUnitData();
        	initNewUnit();
        }
	}

	private void storeUnitData() {
		float d = GetComponent<UnitStats>().getCoveredDistance();
		float t = GetComponent<UnitStats>().getElapsedTime();
        GetComponent<GeneticManager>().addNewUnitResults(d, t);
	}

    /*
	Collision detected, destroy the gameobject record the unit stats
    */
    public void gameOver() {
    	print("Dead");
    }
}
