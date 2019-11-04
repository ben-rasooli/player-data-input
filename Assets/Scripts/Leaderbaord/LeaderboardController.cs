using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlayerDataInput
{
   public class LeaderboardController : MonoBehaviour
   {
      #region ------------------------------dependencies
      [SerializeField] LeaderboardEntry _entryPrefab;
      Func<PlayerData, int> _sortFunction = pd => pd.Score.Points;
      #endregion

      #region ------------------------------interface
      public void Show(List<PlayerData> playerDatas)
      {
         foreach (var playerData in playerDatas.OrderBy(_sortFunction))
         {
            LeaderboardEntry entry = Instantiate(_entryPrefab, transform);
            entry.Show(playerData);
         }
      }

      public void Hide()
      {

      }
      #endregion
   }
}
