using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class UnitAI : MonoBehaviour {

    
    private float steering;
    private float motor;
    private float brake;
    private int dominant;
    private int last; //0 left 1 right

    private void Start() {
        steering = 0;
        motor = 0;
        brake = 0;
    }

    public void resetUnit() {
    	steering = 0;
        motor = 0;
        brake = 0;
    }

    private void detectedRightTurnLeft() {
    	sendMovement(0,1,1,0);
    }

    private void detectedLeftTurnRight() {
    	sendMovement(1,0,1,0);
    }

    private void basicAI() {
    	bool rightRayBool = GetComponent<ObstacleDetection>().getRightRayBool();
        bool leftRayBool = GetComponent<ObstacleDetection>().getLeftRayBool();

        if (rightRayBool && leftRayBool) {
        	//checkDominant(leftRayBool ,rightRayBool);
        	//sendMovement(0,0,0,1);
        	if (last == 1) detectedRightTurnLeft();
        	else if (last == 0) detectedLeftTurnRight();
        	else sendMovement(0,0,1,0);
        }
        else if (rightRayBool && !leftRayBool) {
            detectedRightTurnLeft();
            last = 0;
        }
        else if (leftRayBool && !rightRayBool){
            detectedLeftTurnRight();
            last = 1;
        }
        else {
            sendMovement(0,0,1,0);
        }
    }

    private void FixedUpdate() {
        basicAI();
    }

    private void sendMovement(float rightSteerAmount, float leftSteerAmount, float moveAmount, float brakeAmount) {
        //Now simply steer/move/break
        //Based on the amounts it should do something like steer - wait amount - steer - wait. etc
        if (rightSteerAmount > leftSteerAmount) {
        	steering = rightSteerAmount;
        }
        else {
        	steering = -leftSteerAmount;
        }
        motor = moveAmount;
        //brake = brakeAmount; 
    }

    public float getSteering() {
        return steering;
    }

    public float getMotor() {
        return motor;
    }

    public float getBrake() {
        return brake;
    }

    public void stop() {
    	GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    	brake = 1; 
    	motor = 0;
    	steering = 0;
    }
}