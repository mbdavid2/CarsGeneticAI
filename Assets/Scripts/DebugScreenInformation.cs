using UnityEngine;
using UnityEngine.UI;

public class DebugScreenInformation : MonoBehaviour
{
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
 		int currentN = GetComponent<GeneticManager>().getUnitNumber();
		displayTime.text = "Elapsed time: " + time;
		displayDistance.text = "Distance: " + distance;
		unitNumber.text = "Unit #" + currentN;
    }
}