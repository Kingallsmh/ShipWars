using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour {

    public static UIManagerScript Instance;
    PlayerUI pUI;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateBossHealthOnPlayer(int health){
        pUI.UpdateBossHealth(health);
    }

    public void SetPlayerUI(PlayerUI ui){
        pUI = ui;
    }
}
