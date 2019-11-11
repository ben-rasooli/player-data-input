namespace PlayerDataInput
{
   /// <summary>
   /// This data structure depends on the game and may need modification to suit the game.
   /// </summary>
   public struct LeaderboardPlayerData
   {
      public string Name { get; set; }
      public int ScorePoints { get; set; }

      public LeaderboardPlayerData(FormPlayerData playerData)
      {
         Name = playerData.Details[0].Value;
         ScorePoints = playerData.Score.Points;
      }
   }
}
