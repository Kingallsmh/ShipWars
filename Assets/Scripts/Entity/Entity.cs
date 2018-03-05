using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    Rigidbody rb;
    Vector3 direction;
    public float maxSpeed = 20;

    public NetworkEntityInterpret NEI;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    void PrimaryActionFire(){
    }

    void SecondaryActionFire(){
    }
}
