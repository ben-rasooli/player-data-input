using System.Collections.Generic;
using UnityEngine;

namespace PlayerDataInput
{
   public class GameManager : MonoBehaviour, IScoreReporter
   {
      [SerializeField] LeaderboardController _leaderboardController;
      [SerializeField] PlayerDetail _playerDetailTemplate_Name;
      [SerializeField] PlayerDetail _playerDetailTemplate_Phone;

      public Score Report()
      {
         return new Score { Points = _score};
      }
      [SerializeField] int _score;

      private void Awake()
      {
         FindObjectOfType<FormController>().ScoreReporter = this;
      }

      void Start()
      {
         // player 1
         FormPlayerData playerData_1 = new FormPlayerData { Details = new List<PlayerDetail>() };

         var playerDetail_Name_1 = Instantiate(_playerDetailTemplate_Name);
         playerDetail_Name_1.Value = "Behnam";
         playerData_1.Details.Add(playerDetail_Name_1);

         var playerDetail_Phone_1 = Instantiate(_playerDetailTemplate_Phone);
         playerDetail_Phone_1.Value = "123456";
         playerData_1.Details.Add(playerDetail_Phone_1);

         Score score_1 = new Score { Points = 20 };
         playerData_1.Score = score_1;

         // player 2
         FormPlayerData playerData_2 = new FormPlayerData { Details = new List<PlayerDetail>() };

         var playerDetail_Name_2 = Instantiate(_playerDetailTemplate_Name);
         playerDetail_Name_2.Value = "Theo";
         playerData_2.Details.Add(playerDetail_Name_2);

         var playerDetail_Phone_2 = Instantiate(_playerDetailTemplate_Phone);
         playerDetail_Phone_2.Value = "098765";
         playerData_2.Details.Add(playerDetail_Phone_2);

         Score score_2 = new Score { Points = 25 };
         playerData_2.Score = score_2;

         // player 3
         FormPlayerData playerData_3 = new FormPlayerData { Details = new List<PlayerDetail>() };

         var playerDetail_Name_3 = Instantiate(_playerDetailTemplate_Name);
         playerDetail_Name_3.Value = "Rayhan";
         playerData_3.Details.Add(playerDetail_Name_3);

         var playerDetail_Phone_3 = Instantiate(_playerDetailTemplate_Phone);
         playerDetail_Phone_3.Value = "000111";
         playerData_3.Details.Add(playerDetail_Phone_3);

         Score score_3 = new Score { Points = 10 };
         playerData_3.Score = score_3;

         List<FormPlayerData> playerDatas = new List<FormPlayerData>();
         playerDatas.Add(playerData_1);
         playerDatas.Add(playerData_2);
         playerDatas.Add(playerData_3);

         _leaderboardController.Show(playerDatas);
      }
   }
}
