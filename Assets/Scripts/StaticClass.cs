﻿using UnityEngine;

public static class StaticClass 
{
   public static string LevelSelection{get; set;}
   public static string ThemeSelection{get; set;}
   public static string KeyBoardInput{get; set;}
   public static int MaxLevels{get; set;}
   public static bool freezeTimeEnabled { get; set; }
   public static int freezeTimeCost { get; } = 100;
   public static int freezeTimeFactor { get; } = 10;
   public static int freezeTimeDuration { get; } = 1;
   public static float currentFreezeTimeEnd { get; set; }
   public static bool removeKey {get; set;}
   public static int removeKeyCost{ get; } = 200;
   public static bool tokenHint {get; set;}
   public static int tokenHintCost {get;} = 200;

   public static string analyticsFilePath= Application.persistentDataPath + "/analytics.json";
   public static bool firstTimePlayer = false;
   public static bool gameStarted = false;
}
