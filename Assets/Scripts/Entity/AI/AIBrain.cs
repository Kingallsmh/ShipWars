using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour {

    public Collider environmentCol;
    public Collider ownCollider;

    public Controller AIControl;
    public ShipEntity ship;
    //Test
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckCorrectRotation();
	}

    public void CheckEnvironmentForCollision(){
        
    }

    public void CheckCorrectRotation(){
        //Ships use pitch to quickly turn up or down of their current facing
        //This function should be used to check if and what rotation is needed
        Quaternion currentRot = ship.transform.rotation;
        Vector3 targetPos = target.transform.position;
        Vector3 targetDir = target.transform.position - ship.transform.position;
        Vector3 difInRot = Vector3.RotateTowards(ship.transform.localEulerAngles, targetDir, 1, 0);
        Debug.Log("Rotate numbers: " + difInRot);
        //Quaternion difInRot = Quaternion.LookRotation(ship.transform.position, targetPos);
    }
}
