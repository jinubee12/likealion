using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructComp : MonoBehaviour
{
    private int[] playerScores = new int[5];

    private string[] itemNames = { "검", "방패", "포션", "활", "마법서" };

    public GameObject[] enemyPrefabs;

    private int[,] mapTiles = new int[10, 10];

    private void Start()
    {
        for (var i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = i;
        }

        for (var i = 0; i < playerScores.Length; i++)
        {
            Debug.Log(playerScores[i]);
        }
    }
}

