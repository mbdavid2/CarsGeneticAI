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

	public void debugUnitInformationConsole(int[] unitCodification) {
    	int angle = unitCodification[0];
    	int dist = unitCodification[1];
    	int brake = unitCodification[2];
    	int brakeTime = unitCodification[3];
    	int velocity = unitCodification[4];

    	print("Code: " + intArrayToStr(unitCodification) + " || " + "Angle: " + angle + ", distance detection: " + dist + 
	  ", brake: " + brake*10 + ", vel: " + velocity +
	  ", brakeTime: " + brakeTime
	  );
    }

    public void debugUnitInformationScreen(int[] unitCodification) {
    	int angle = unitCodification[0];
    	int dist = unitCodification[1];
    	int brake = unitCodification[2];
    	int brakeTime = unitCodification[3];
    	int velocity = unitCodification[4];

    	print("Code: " + intArrayToStr(unitCodification) + " || " + " Angle: " + angle + ", distance detection: " + dist + 
	  ", brake: " + brake*10 + ", vel: " + velocity +
	  ", brakeTime: " + brakeTime
	  );
    }

    private string intArrayToStr(int[] intArray) {
    	string result = "";
    	for (int i = 0; i < intArray.Length; ++i) {
    		result += intArray[i];
    	}
    	return result;
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