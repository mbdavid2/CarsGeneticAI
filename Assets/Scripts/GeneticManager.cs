using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour {

	private struct UnitData {
        public int number;
        public int[] unitCodification;
        public float coveredDistance;
        public float elapsedTime;
        public float heuristic;
    }

    private int currentN;
    private int generationN;
    public int maxUnitsPerGeneration;

    ArrayList currentGeneration = new ArrayList();

	private void Start() {
	
	}

	private void initGenetic() {
		currentN = 0;
		generationN = 0;
		maxUnitsPerGeneration = 5;
		createInitialGeneration();
	}

	private float computeUnitHeuristic(UnitData unit) {
		return (0.75f*unit.coveredDistance + 0.25f*unit.elapsedTime)/100;
	}

	public void addNewUnitResults(float dist, float eTime) {
		UnitData tmp = (UnitData)currentGeneration[currentN];
		tmp.coveredDistance = dist;
		tmp.elapsedTime = eTime;
		//tmp.heuristic = computeUnitHeuristic(tmp);
		tmp.heuristic = Random.Range(0,400);
		//tmp.number = currentN;
		currentGeneration[currentN] = tmp;
		printUnit((UnitData) currentGeneration[currentN]);
		currentN++;
	}

	private int[] generateRandomInitialUnit() {
		//Provisional
		int dist = Random.Range(0,4);
		int angle = Random.Range(0,4);
		//////////
		int[] codification = new int[] {dist, angle, dist, angle, dist, angle, dist};
		return codification;
	}

	public void createInitialGeneration() {
		for (int i = 0; i < maxUnitsPerGeneration; i++) {
			UnitData tmp = createNewUnit(generateRandomInitialUnit(), i);
			currentGeneration.Add(tmp);
		}
	}

	private UnitData createNewUnit(int[] codification, int numberID) {
		UnitData tmp;
		tmp.unitCodification = codification;
		tmp.coveredDistance = -1;
		tmp.elapsedTime = -1;
		tmp.heuristic = -1f;
		tmp.number = numberID;
		return tmp;
	}

	private ArrayList sortUnits() {
		/*for (int j = 0; j < currentGeneration.Count; j++) {
			print(j + " -> Score: " + ((UnitData)currentGeneration[j]).heuristic);
		}*/
		//Insertion sort for a small input
		ArrayList orderedGeneration = new ArrayList();
		orderedGeneration.Add((UnitData)currentGeneration[0]);
		for (int i = 1; i < currentGeneration.Count; i++) {
			UnitData unit = ((UnitData)currentGeneration[i]);
			//Insert
			bool found = false;
			for (int j = 0; j < orderedGeneration.Count; j++) {
				UnitData aux = ((UnitData)orderedGeneration[j]);
				if (unit.heuristic > aux.heuristic) {
					orderedGeneration.Insert(j, unit);
					found = true;
					break;
				}
			}
			if (!found) {
				orderedGeneration.Insert(orderedGeneration.Count, unit);
			}
		}
		/*for (int j = 0; j < orderedGeneration.Count; j++) {
			print(j + " -> Score: " + ((UnitData)orderedGeneration[j]).heuristic);
		}*/
		return orderedGeneration;
	}

	private UnitData crossUnits(int[] codificationA, int[] codificationB, int numberID) {
		int[] codification = new int[codificationA.Length];
		for (int i = 0; i < codificationA.Length; i++) {
			if (i < codificationA.Length/2) codification[i] = codificationA[i];
			else codification[i] = codificationB[i];
		}
		//int[] codification = new int[] {0, 0, 0, 0, 0, 0, 0};
		UnitData tmp = createNewUnit(codification, numberID);
		return tmp;
	}

	private void finishGeneration() {
		ArrayList orderedGeneration = sortUnits();
		printGeneration(orderedGeneration);

		//Only the first half is kept -> both as it is and crossed with another one
		currentGeneration = new ArrayList();
		int nUnits = orderedGeneration.Count/2;
		for (int i = 0; i < nUnits; i++) {
			//Untouched
			int[] codificationA = ((UnitData)orderedGeneration[i]).unitCodification;
			UnitData unitA = createNewUnit(codificationA, i);
			currentGeneration.Add(unitA);

			int[] codificationB = ((UnitData)orderedGeneration[i+1]).unitCodification;
			UnitData newUnit = crossUnits(codificationA, codificationB, orderedGeneration.Count - i - 1);
			currentGeneration.Add(newUnit);
			
			if (orderedGeneration.Count%2 != 0 && i == nUnits - 1) {
				int[] codificationExtra = ((UnitData)orderedGeneration[i+1]).unitCodification;
				UnitData unitE = createNewUnit(codificationExtra, orderedGeneration.Count/2);
				currentGeneration.Add(unitE);
			}
		}
		printGeneration(currentGeneration);
	}

	public int[] getNewUnitCodification() {
		//Test
		if (currentN > currentGeneration.Count-1 && currentGeneration.Count > 0) {
			finishGeneration();
			currentN = 0;
			generationN++;
			return getNewUnitCodification();
		}
		else if (currentGeneration.Count == 0) {
			print("No generation, creating one");
			initGenetic();
			return getNewUnitCodification();
		}
		else {
			UnitData unit = ((UnitData)currentGeneration[currentN]);
			return unit.unitCodification;
		}
	}

	public int getUnitNumber() {
		return currentN;
	}

	private void printUnit(UnitData unit) {
		/*print("Number: " + unit.number);
		print("Time: " + unit.elapsedTime.ToString("#.00"));
		print("Distance: " + unit.coveredDistance.ToString("#.00"));*/
	}

	private void printGeneration(ArrayList generation) {
		print("Printing generation:" + currentGeneration.Count + " units");
		for (int i = 0; i < generation.Count; i++) {
			print("Unit #" + ((UnitData)generation[i]).number +
				" C: " + getCodification(((UnitData)generation[i]).unitCodification) + 
				" P: " + ((UnitData)generation[i]).heuristic + 
				" D: " + ((UnitData)generation[i]).coveredDistance +
				" T: " + ((UnitData)generation[i]).elapsedTime);
		}
	} 

	private string getCodification(int[] unitCodification) {
		string cod = "";
		for (int i = 0; i < unitCodification.Length; i++) {
			cod += unitCodification[i];
		}
		return cod;
	}
}