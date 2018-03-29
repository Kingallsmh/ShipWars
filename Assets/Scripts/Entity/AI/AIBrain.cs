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
    public Vector3 pitchAssist, rollAssist;

	// Use this for initialization
	void Start () {
        if(GetComponent<Controller>()){
            AIControl = GetComponent<Controller>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        AdjustRollToTarget(target.transform);
        //AdjustPitchToTarget(target.transform);
        DebugAngles();
	}

    bool AdjustRollToTarget(Transform _target){
        
        if(CheckCorrectRoll(_target) <= 2 && CheckCorrectRoll(_target) >= -2){
            Debug.Log("No control: " + CheckCorrectRoll(_target));
            AIControl.StickInput = new Vector2(0, AIControl.StickInput.y);
            return true;
        }
        else if(CheckCorrectRoll(_target) < -2){
            Debug.Log("Turn: " + CheckCorrectRoll(_target));
            AIControl.StickInput = new Vector2(-1, AIControl.StickInput.y);
        }
        else if(CheckCorrectRoll(_target) > 2){
            Debug.Log("Turn: " + CheckCorrectRoll(_target));
            AIControl.StickInput = new Vector2(1, AIControl.StickInput.y);
        }
        return false;
    }

    void AdjustPitchToTarget(Transform _target){
        float zVal = CheckCorrectPitch(_target);
        if (CheckCorrectPitch(_target) <= 2 && CheckCorrectPitch(_target) >= -2)
        {
            Debug.Log("No control: " + CheckCorrectPitch(_target));
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 0);
        }
        else if (CheckCorrectPitch(_target) < 180)
        {
            Debug.Log("Pitch: " + CheckCorrectPitch(_target));
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 1);
        }
        else if (CheckCorrectPitch(_target) > 180)
        {
            Debug.Log("Pitch: " + CheckCorrectPitch(_target));
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, -1);
        }
    }

    public void CheckEnvironmentForCollision(){
        
    }

    public float CheckCorrectPitch(Transform _target){
        Vector3 newVec = (_target.transform.position - transform.position).normalized;
        pitchAssist = newVec;
        newVec.x = 0;
        Vector3 dif = Quaternion.FromToRotation(transform.forward, newVec).eulerAngles;
        return dif.x;
    }

    public float CheckCorrectRoll(Transform _target){
        //Ships use pitch to quickly turn up or down of their current facing
        //This function should be used to check if and what rotation is needed
        Vector3 newVec = (_target.transform.position - transform.position).normalized;
        rollAssist = newVec;
        newVec.z = 0;
        Vector3 dif = Quaternion.FromToRotation(transform.up, newVec).eulerAngles;
        Vector3 newDif = dif - transform.rotation.eulerAngles;
        //transform.rotation *= Quaternion.Euler(dif);
        return newDif.z;
    }



    Color up = new Color(1, 0, 0);
    Color forw = new Color(0, 1, 0);
    Color toTarget = new Color(0, 0, 1);
    void DebugAngles(){
        
        Debug.DrawLine(transform.position, transform.position + (transform.up* 5), up);
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 5), forw);
        Debug.DrawLine(transform.position, transform.position + (rollAssist * 5), toTarget);

    }
}
