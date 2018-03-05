using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UnattachCamera(){
        Camera.main.transform.parent = null;
    }
}
