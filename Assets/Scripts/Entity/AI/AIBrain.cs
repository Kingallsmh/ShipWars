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
        Vector3 targetDir = GetDirectionOfTarget(target.transform);
        AdjustRollToTarget(targetDir);
        AdjustPitchToTarget(targetDir);
        DebugAngles();
	}

    bool AdjustRollToTarget(Vector3 _targetDir){
        float rollAngle = CheckCorrectRoll(_targetDir);
        if(rollAngle < 179 && rollAngle >= 1){
            Debug.Log("Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(-1, AIControl.StickInput.y);
        }
        else if(rollAngle > 181 && rollAngle <= 359){
            Debug.Log("Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(1, AIControl.StickInput.y);
        }
        else{
            Debug.Log("Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(0, AIControl.StickInput.y);
            return true;
        }
        return false;
    }

    bool AdjustRollToTarget2(Vector3 _targetDir)
    {
        float rollAngle = CheckCorrectRoll2(_targetDir);
        if (rollAngle < - 0.01f)
        {
            Debug.Log("Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(-1, AIControl.StickInput.y);
        }
        else if (rollAngle > 0.01f)
        {
            Debug.Log("Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(1, AIControl.StickInput.y);
        }
        else
        {
            Debug.Log("No Roll: " + rollAngle);
            AIControl.StickInput = new Vector2(0, AIControl.StickInput.y);
            return true;
        }
        return false;
    }

    void AdjustPitchToTarget(Vector3 _targetDir){
        float pitchAngle = CheckCorrectPitch(_targetDir);
        if (pitchAngle < 179 && pitchAngle >= 1)
        {
            Debug.Log("Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 1);
        }
        else if (pitchAngle > 181 && pitchAngle <= 359)
        {
            Debug.Log("Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, -1);
        }
        else
        {
            Debug.Log("No Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 0);
        }
    }

    void AdjustPitchToTarget2(Vector3 _targetDir)
    {
        float pitchAngle = CheckCorrectPitch2(_targetDir);
        if (pitchAngle < -0.01f)
        {
            Debug.Log("Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 1);
        }
        else if (pitchAngle > 0.01f)
        {
            Debug.Log("Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, -1);
        }
        else
        {
            Debug.Log("No Pitch: " + pitchAngle);
            AIControl.StickInput = new Vector2(AIControl.StickInput.x, 0);
        }
    }

    public Vector3 GetDirectionOfTarget(Transform _target){
        Vector3 newVec = (_target.transform.position - transform.position).normalized;
        rollAssist = newVec;
        return newVec;
    }

    public float CheckCorrectPitch(Vector3 targetDir){
        //targetDir.x = 0;
        Vector3 dif = Quaternion.FromToRotation(transform.forward, targetDir).eulerAngles;
        return dif.x;
    }

    public float CheckCorrectPitch2(Vector3 targetDir)
    {
        Debug.Log(targetDir);
        Vector3 newDir = transform.InverseTransformDirection(targetDir);
        Debug.Log(newDir);
        return newDir.y;
    }

    public float CheckCorrectRoll(Vector3 targetDir){
        //Ships use pitch to quickly turn up or down of their current facing
        //This function should be used to check if and what rotation is needed
        //targetDir.z = 0; 
        Debug.Log(targetDir);
        Vector3 newDir = transform.InverseTransformDirection(targetDir);
        Debug.Log(newDir);
        Vector3 dif = (Quaternion.FromToRotation(transform.up, newDir)).eulerAngles;
        Debug.Log("Local: " + dif);
        dif.x = 0;
        dif.y = 0;
        //transform.localRotation = Quaternion.Euler(dif) * transform.rotation;
        return dif.z;
    }

    public float CheckCorrectRoll2(Vector3 targetDir){
        Debug.Log(targetDir);
        Vector3 newDir = transform.InverseTransformDirection(targetDir);
        Debug.Log(newDir);
        return newDir.x;
    }

    Color up = new Color(1, 0, 0);
    Color forw = new Color(0, 1, 0);
    Color toTarget = new Color(0, 0, 1);
    Color combined = new Color(1, 0, 1);
    void DebugAngles(){
        
        Debug.DrawLine(transform.position, transform.position + (transform.up* 20), up);
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 20), forw);
        Debug.DrawLine(transform.position, transform.position + (rollAssist * 20), toTarget);
        Debug.DrawLine(transform.position, transform.position + ((transform.InverseTransformDirection(rollAssist)) * 20), combined);
    }
}
