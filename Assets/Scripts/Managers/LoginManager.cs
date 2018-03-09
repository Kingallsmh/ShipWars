using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour {

    bool isPlayer = false;

    public void ClickPlayShip(){
        isPlayer = true;
    }

    public void ClickViewer(){
        isPlayer = false;
    }

    public void LoadMain(){
        //Load main scene with GameManager bool loaded from this script
    }
}
