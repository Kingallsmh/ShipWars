using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerShip : ShipEntity {

    public float rollFactor = 1, pitchFactor = 1;
    float currentSpeed = 0;

    public GameObject obj;

	public override void Init()
	{
        base.Init();
        Debug.Log("Islocal!");
        obj.SetActive(true);
	}

	public override void ControlLoop()
    {
        base.ControlLoop();
        GatherInput();
    }

    public override void FixedControlLoop()
    {
        base.FixedControlLoop();
        Movement();
    }

    public void GatherInput()
    {
        control.GetInput();

        //Controls
        rotation = new Vector3(control.StickInput.y, 0, -control.StickInput.x);
        rotation.z = rotation.z * rollFactor;
        if (rotation.x > 0)
        {
            rotation.x = rotation.x * (pitchFactor / 1.5f);
        }
        else
        {
            rotation.x = rotation.x * (pitchFactor);
        }

        //Shoot
        if (control.Btn1)
        {
            currentSpeed = maxSpeed;
        }
        else{
            currentSpeed = 0;
        }
    }

    public override void Movement()
    {
        //Push ship forward in current facing direction at current maxSpeed
        rb.velocity = transform.forward * currentSpeed * 100 * Time.deltaTime;
        //Rotate local transform
        transform.localRotation *= Quaternion.Euler(rotation);
    }

    public void ActivateTurrets(){
        GameManagerScript.Instance.CmdSetTurretToTargetPlayer(1);
    }
}
