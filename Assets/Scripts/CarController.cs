using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class CarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float maxBrakeTorque; // maximum steer angle the wheel can have

    private void Start () {
        maxBrakeTorque = 2200;
        maxMotorTorque = 800;
        maxSteeringAngle = 40;
    }
        
    private void FixedUpdate () {
        /*float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float brake = maxBrakeTorque * Input.GetAxis("Jump");*/
        
        float motor = maxMotorTorque * GetComponent<UnitAI>().getMotor();
        float steering = maxSteeringAngle * GetComponent<UnitAI>().getSteering();
        float brake = maxBrakeTorque * GetComponent<UnitAI>().getBrake();
        
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            axleInfo.leftWheel.brakeTorque = brake;
            axleInfo.rightWheel.brakeTorque = brake;
        }
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
    public float maxBrakeTorque;
}