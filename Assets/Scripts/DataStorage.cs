using System.Collections.Generic;

namespace PlayerDataInput
{
   public class DataStorage
   {
      /// <summary>
      /// It appends a player data to the list of player data which was saved previously.
      /// If no list is available, a new one is created.
      /// </summary>
      /// <param name="playerData"></param>
      public void Save(PlayerData playerData)
      {

      }

      /// <summary>
      /// It loads the list of player data stored on the disc.
      /// It returns an empty list if no list is found.
      /// </summary>
      /// <returns></returns>
      public List<PlayerData> Load()
      {
         return null;
      }
   }
}
