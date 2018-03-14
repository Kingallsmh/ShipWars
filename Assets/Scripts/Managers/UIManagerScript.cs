using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {
    
    PlayerUI pUI;
    public Image HPImg;

    public void UpdateBossHealthOnPlayer(int health){
        pUI.UpdateBossHealth(health);
    }

    public void SetPlayerUI(PlayerUI ui){
        pUI = ui;
    }

    
}
