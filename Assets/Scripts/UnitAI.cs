using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class UnitAI : MonoBehaviour {

    private int[] unitCodification;
    private float steering;
    private float motor;
    private float brake;

    private void Start() {
        steering = 0;
        motor = 0;
        brake = 0;
    }
        
    private void FixedUpdate() {
        bool rightRayBool = GetComponent<ObstacleDetection>().getRightRayBool();
        bool leftRayBool = GetComponent<ObstacleDetection>().getRightRayBool();

        if (rightRayBool) {
            sendMovement(0,1,1,0);
        }
        else if (leftRayBool){
            sendMovement(1,0,1,0);
        }
        else {
            sendMovement(0,0,1,0);
        }
    }

    private void sendMovement(float rightSteerAmount, float leftSteerAmount, float moveAmount, float brakeAmount) {
        //Now simply steer/move/break
        //Based on the amounts it should do something like steer - wait amount - steer - wait. etc
        if (rightSteerAmount > leftSteerAmount) steering = rightSteerAmount;
        else steering = leftSteerAmount;
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
}