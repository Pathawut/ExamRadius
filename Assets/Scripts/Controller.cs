using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public GameObject Dialog;
    public Button Restart;

    public Action OnDeath;

    void Start()
    {
        Restart.onClick.AddListener(GameReset);

    }

   
    void GameReset()
    {
        player.Reset();
        enemy.Reset();
    }
}
