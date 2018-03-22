using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {

    public static LoginManager Instance;
    bool isPlayer = false;
    int playerSelect = 0;
    public GameObject obj;
    public Text playerTypeText;

	private void Awake()
	{
        Instance = this;
	}

	private void Start()
	{
        if(Application.platform != RuntimePlatform.IPhonePlayer){
            obj.SetActive(true);
            SwitchPlayerType();
        }
	}

    public void SwitchPlayerType(){
        isPlayer = !isPlayer;
        if(isPlayer){
            playerTypeText.text = "Current Player Type: Player";
            playerSelect = 1;
        }
        else{
            playerTypeText.text = "Current Player Type: Viewer";
            playerSelect = 0;
        }
    }

    public void LoadMain(){
        //Load main scene with GameManager bool loaded from this script
    }

    public int GetPlayerSelect(){
        return playerSelect;
    }
}
