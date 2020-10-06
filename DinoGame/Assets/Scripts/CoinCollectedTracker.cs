using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollectedTracker : MonoBehaviour
{
    Text coinsCollected;
    void Start()
    {
        coinsCollected = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsCollected.text = "Coins Collected: " + GlobalVariables.coinsCollected + "x";
    }
}
