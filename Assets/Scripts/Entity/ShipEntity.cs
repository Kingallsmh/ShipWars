﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipEntity : MonoBehaviour {
    
    protected Vector3 velocity, rotation;
    public float maxSpeed = 20;

    protected Rigidbody rb;
    protected Controller control;
    protected NetworkEntityInterpret NEI;
    protected StatusScript stats;

    public Transform camPoint;

	public virtual void Init()
    {
        rb = GetComponent<Rigidbody>();
        control = GetComponent<Controller>();
        //NEI = GetComponent<NetworkEntityInterpret>();
        stats = GetComponent<StatusScript>();
    }

	public virtual void ControlLoop(){
    }

    public virtual void FixedControlLoop(){
    }

    public virtual void PrimaryActionFire(){
    }

    public virtual void SecondaryActionFire(){
    }

    public virtual void Movement(){
    }
}
