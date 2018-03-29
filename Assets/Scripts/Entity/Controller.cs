using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    Vector2 stickInput;
    bool btn1, btn2, btn3, btn4;
    bool start;

    public void GetInput(){
        Debug.Log("Oops");
        StickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        btn1 = Input.GetAxis("Button1") > 0;
        btn2 = Input.GetAxis("Button2") > 0;
        btn3 = Input.GetAxis("Button3") > 0;
        btn4 = Input.GetAxis("Button4") > 0;
        start = Input.GetAxis("Start") > 0;
    }

    public bool Start
    {
        get
        {
            return start;
        }
    }

    public bool Btn4
    {
        get
        {
            return btn4;
        }
    }

    public bool Btn3
    {
        get
        {
            return btn3;
        }
    }

    public bool Btn2
    {
        get
        {
            return btn2;
        }
    }

    public bool Btn1
    {
        get
        {
            return btn1;
        }
    }

    public Vector2 StickInput
    {
        get
        {
            return stickInput;
        }

        set
        {
            stickInput = value;
        }
    }
}
