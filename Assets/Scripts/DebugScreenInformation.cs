using UnityEngine;
using UnityEngine.UI;

public class DebugScreenInformation : MonoBehaviour
{
    private int avgFrameRate;
    public Text displayTime;
	public Text displayDistance;
	public Text unitNumber;

	private void Start () {
		displayTime.text = "";
		displayDistance.text = "";
		unitNumber.text = "";
	}
 
 	private void Update () {
 		string time = GetComponent<UnitStats>().getElapsedTime().ToString("#.00");
 		string distance = GetComponent<UnitStats>().getCoveredDistance().ToString("#.00");
		displayTime.text = "Elapsed time: " + time;
		displayDistance.text = "Distance: " + distance;
		unitNumber.text = "Unit #1";
    }
}