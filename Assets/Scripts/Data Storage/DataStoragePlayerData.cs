using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerDataInput
{
   [Serializable]
   public struct DataStoragePlayerData
   {
      public Dictionary<string, string> Details;
      public Score Score;

      public DataStoragePlayerData(FormPlayerData playerData)
      {
         Details = playerData.Details
            .Where(pd => pd.IsEnable)
            .ToDictionary(pd => pd.Name, pd => pd.Value);
         Score = playerData.Score;
      }
   }
}
