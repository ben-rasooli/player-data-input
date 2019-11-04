namespace PlayerDataInput
{
   /// <summary>
   /// This data structure depends on the game and may need modification to suit the game.
   /// </summary>
   public struct LeaderboardPlayerData
   {
      public string Name { get; set; }
      public Score Score { get; set; }

      public LeaderboardPlayerData(PlayerData playerData)
      {
         Name = playerData.Details[0].Value;
         Score = playerData.Score;
      }
   }
}
