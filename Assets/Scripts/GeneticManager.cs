﻿using System.Collections;
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
    private int generationN;
    public int maxUnitsPerGeneration;

    ArrayList currentGeneration = new ArrayList();

	private void Start() {
	
	}

	private void initGenetic() {
		currentN = 0;
		generationN = 0;
		maxUnitsPerGeneration = 3;
		createInitialGeneration();
	}

	public void addNewUnitResults(float dist, float eTime) {
		UnitData tmp = (UnitData)currentGeneration[currentN];
		tmp.coveredDistance = dist;
		tmp.elapsedTime = eTime;
		//tmp.number = currentN;
		currentGeneration[currentN] = tmp;
		debugUnit((UnitData) currentGeneration[currentN]);
		currentN++;
	}

	public void createInitialGeneration() {
		for (int i = 0; i < maxUnitsPerGeneration; i++) {
			UnitData tmp;
			//Provisional
			int dist = Random.Range(0,4);
			int angle = Random.Range(0,4);
			//////////
			int[] codification = new int[] {dist, angle, 3, 3, 3, 4, 5 };
			tmp.unitCodification = codification;
			tmp.coveredDistance = -1;
			tmp.elapsedTime = -1;
			tmp.number = i;
			currentGeneration.Add(tmp);
		}
	}

	private void finishGeneration() {

	}

	public int[] getNewUnitCodification() {
		//Test
		/*if (currentN > currentGeneration.Count) {
			finishGeneration();
			currentN = 0;
			generationN++;
			return getNewUnitCodification(); //Uyyyyy
		}
		else {*/
		if (currentGeneration.Count == 0) {
			print("No generation, creating one");
			initGenetic();
			return getNewUnitCodification();
		}
		else {
			UnitData unit = ((UnitData)currentGeneration[currentN]);
			return unit.unitCodification;
		}
		//}
		
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
