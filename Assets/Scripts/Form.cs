using UnityEngine;

namespace PlayerDataInput
{
   public class Form : MonoBehaviour
   {
      #region ------------------------------dependencies
      [SerializeField] IScoreReporter _scoreReporter;
      [SerializeField] DataStorage _dataStorage;
      #endregion

      #region ------------------------------interface
      /// <summary>
      /// It creates a new PlayerData and sends it to a data persistence solution.
      /// </summary>
      public void Submit()
      {

      }
      #endregion
   }
}
