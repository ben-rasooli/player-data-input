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
            var scoreProperties = leaderboardPlayerData.Score.GetType().GetProperties();

            for (int i = 0; i < dataProperties.Length - 1; i++)
                _textUIs[i].text = dataProperties[i].GetValue(leaderboardPlayerData).ToString();

            for (int i = 0; i < scoreProperties.Length; i++)
                _textUIs[i + (dataProperties.Length - 1)].text = scoreProperties[i].GetValue(leaderboardPlayerData.Score).ToString();
        }
    }
    #endregion
}
