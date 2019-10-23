

public static class StaticClass 
{
   public static string LevelSelection{get; set;}
   public static string ThemeSelection{get; set;}
   public static string KeyBoardInput{get; set;}
   public static int MaxLevels{get; set;}
   public static bool freezeTimeEnabled { get; set; }
   public static int freezeTimeCost { get; } = 10;
   public static int freezeTimeFactor { get; } = 10;
   public static int freezeTimeDuration { get; } = 1;
   public static float currentFreezeTimeEnd { get; set; }
}
