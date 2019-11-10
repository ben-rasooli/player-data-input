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
            LeaderboardEntry entry = Instantiate(_entryPrefab, _entriesTransform);
            entry.Show(playerData);
            _entries.Add(entry.gameObject);
         }
      }
      [SerializeField] Transform _entriesTransform;

      public void Hide()
      {
         gameObject.SetActive(false);
      }
      #endregion

      #region ------------------------------details
      List<GameObject> _entries = new List<GameObject>();

      private void OnDisable()
      {
         for (int i = 0; i < _entries.Count; i++)
            Destroy(_entries[i]);

         _entries.Clear();
      }
      #endregion
   }
}
