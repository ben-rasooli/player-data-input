using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PlayerDataInput
{
    public class LeaderboardEntry : MonoBehaviour
    {
        #region ------------------------------dependencies
        // The order of the text references should match the order of LeaderboardPlayerData fields.
        [SerializeField] List<TextMeshProUGUI> _textUIs;
        #endregion

        #region ------------------------------interface
        public void Show(PlayerData playerData)
        {
            var leaderboardPlayerData = new LeaderboardPlayerData(playerData);

            var dataProperties = leaderboardPlayerData.GetType().GetProperties();
            var scoreProperties = leaderboardPlayerData.ScorePoints.GetType().GetProperties();

            for (int i = 0; i < dataProperties.Length; i++)
                _textUIs[i].text = dataProperties[i].GetValue(leaderboardPlayerData).ToString();
        }
    }
    #endregion
}
