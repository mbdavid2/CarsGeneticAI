using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class UnitAI : MonoBehaviour {

    
    private float steering;
    private float motor;
    private float brake;
    private int dominant;
    private int last; //0 left 1 right -1 none

    //Unit parameters
    private int unitBrake;
    private int velocity;
    private bool alwaysBrake;
    private int brakeTime;
    private int maxBrakeTime;

    private void Start() {
        steering = 0;
        motor = 0;
        brake = 0;
        velocity = 1;
        last = -1;
        alwaysBrake = false;
        brakeTime = 0;
        maxBrakeTime = 0;
    }

    public void resetUnit() {
    	steering = 0;
        motor = 0;
        brake = 0;
        velocity = 1;
        last = -1;
        brakeTime = 0;
    }

    private void detectedRightTurnLeft() {
    	//print("t: " + brakeTime + " max: " + maxBrakeTime);
    	if (brakeTime < maxBrakeTime) {
    		sendMovement(0,1,0,unitBrake);
    		brakeTime++;
    		print("Braking!!!!");
    	}
    	else sendMovement(0,1,velocity,0);
    }

    private void detectedLeftTurnRight() {
    	if (brakeTime < maxBrakeTime) {
    		sendMovement(1,0,0,unitBrake);
    		brakeTime++;
    		print("Braking!!!!");
    	}
    	else sendMovement(1,0,velocity,0);
    }

    private void basicAI() {
    	bool rightRayBool = GetComponent<ObstacleDetection>().getRightRayBool();
        bool leftRayBool = GetComponent<ObstacleDetection>().getLeftRayBool();

        if (rightRayBool && leftRayBool) {
        	//checkDominant(leftRayBool ,rightRayBool);
        	//sendMovement(0,0,0,1);
        	if (last == 1) detectedRightTurnLeft();
        	else if (last == 0) detectedLeftTurnRight();
        	else {
        		sendMovement(0,0,velocity,0);
        		brakeTime = 0;
        	}
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
        	brakeTime = 0;
            sendMovement(0,0,velocity,0);
            last = -1;
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
        brake = brakeAmount; 
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

    //Unit parameters/behaviour
    public void setUnitBrake(int unitBrake, int maxBrakeTime) {
    	this.unitBrake = unitBrake;
    	this.maxBrakeTime = maxBrakeTime;
    }

    public void setUnitVelocity(int vel) {
    	velocity = vel;
    }

}