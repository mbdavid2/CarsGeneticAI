using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour {

	private struct UnitData {
        public int number;
        public int[] unitCodification;
        public float coveredDistance;
        public float elapsedTime;
    }

    private int currentN;

    ArrayList currentGeneration;

	private void Start() {
		currentN = 0;
		currentGeneration = new ArrayList();
	}

	public void addNewUnitResults(int[] codification, float dist, float eTime) {
		UnitData tmp;
		tmp.unitCodification = codification;
		tmp.coveredDistance = dist;
		tmp.elapsedTime = eTime;
		tmp.number = currentN++;
		currentGeneration.Add(tmp);
		debugUnit((UnitData) currentGeneration[currentGeneration.Count - 1]);
	}

	public int[] getNewUnitCodification() {
		//Test
		int[] unitCodification = new int[] {3, 2, 3, 3, 3, 4, 5 };
		return unitCodification;
	}

	public int getUnitNumber() {
		return currentN;
	}

	private void debugUnit(UnitData unit) {
		print("Number: " + unit.number);
		print("Time: " + unit.elapsedTime.ToString("#.00"));
		print("Distance: " + unit.coveredDistance.ToString("#.00"));
	}
}
