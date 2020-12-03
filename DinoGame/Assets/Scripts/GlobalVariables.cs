﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    // This class contains any variable we would like to keep track off across different scenes.
    public static float coinsCollected = 0f;
    public static bool babyDinoAcquired = false;
    public static string FightingWith;
    public static bool TRexDefeated = false;
    public static bool HerbDefeated = false;
    public static bool BirdDefeated = false;
    public static bool inMainScene = true;
    public static float leversPressed = 0f;
}
