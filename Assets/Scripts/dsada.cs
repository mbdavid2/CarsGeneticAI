using UnityEngine;

public class CarCondsadatroller : MonoBehaviour {
	
	public WheelCollider wheelFrontLeft;
	public WheelCollider wheelBackLeft;
	public WheelCollider wheelFrontRight;
	public WheelCollider wheelBackRight;

	public GameObject frontLeft;
	public GameObject backLeft;
	public GameObject frontRight;
	public GameObject backRight;

	public float topSpeed = 250f;
	public float maxTorque = 200f;
	public float maxSteerAngle = 45f;
	public float currentSpeed;
	public float maxBrakeTorque = 2200;

	private float forward;
	private float turn;
	private float brake;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		forward = 0;
		brake = 0;
		turn = 0;
	}

	void FixedUpdate () {
		forward = Input.GetAxis("Vertical");
		turn = Input.GetAxis("Horizontal");
		brake = Input.GetAxis("Jump");

		wheelFrontLeft.steerAngle = maxSteerAngle * turn;
		wheelFrontRight.steerAngle = maxSteerAngle * turn;

		currentSpeed = 2 * 22 / 7 * wheelBackLeft.radius * wheelBackLeft.rpm * 60 / 1000;
		
		if (currentSpeed < topSpeed) {
			wheelBackLeft.motorTorque = maxTorque * forward;
			wheelBackRight.motorTorque = maxTorque * forward;
		}

		wheelBackLeft.brakeTorque = maxBrakeTorque * brake;
		wheelBackRight.brakeTorque = maxBrakeTorque * brake;
		wheelFrontLeft.brakeTorque = maxBrakeTorque * brake;
		wheelFrontRight.brakeTorque = maxBrakeTorque * brake;
	}

	void Update() {
		Vector3 flV;
		Quaternion flQ;
		wheelFrontLeft.GetWorldPose(out flV, out flQ);
		frontLeft.transform.position = flV;

		Vector3 blV;
		Quaternion blQ;
		wheelBackLeft.GetWorldPose(out blV, out blQ);
		backLeft.transform.position = blV;
		backLeft.transform.rotation = blQ;

		Vector3 frV;
		Quaternion frQ;
		wheelFrontRight.GetWorldPose(out frV, out frQ);
		frontRight.transform.position = frV;
		frontRight.transform.rotation = frQ;

		Vector3 brV;
		Quaternion brQ;
		wheelBackRight.GetWorldPose(out brV, out brQ);
		backRight.transform.position = brV;
		backRight.transform.rotation = brQ;
	}

}