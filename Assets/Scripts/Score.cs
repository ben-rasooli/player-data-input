using System;

namespace PlayerDataInput
{
   /// <summary>
   /// This data structure depends on the game and may need modification to suit the game.
   /// </summary>
   [Serializable]
   public struct Score
   {
      public int Points { get; set; }
   }
}
