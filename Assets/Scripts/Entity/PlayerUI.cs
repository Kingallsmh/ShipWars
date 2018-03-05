﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject UIHolder;
    public Text healthGUI;
    public Text bossHealthGUI;

    public void UpdatePlayerHealth(int health){
        healthGUI.text = "Player Health: " + health;
    }

    public void UpdateBossHealth(int health){
        bossHealthGUI.text = "Boss Health: " + health;
    }

    public void ShowUI(bool show){
        UIHolder.SetActive(show);
    }
}