using UnityEngine;

namespace PlayerDataInput
{
   public class FormController : MonoBehaviour
   {
      #region ------------------------------dependencies
      [SerializeField] IScoreReporter _scoreReporter;
      [SerializeField] DataStorage _dataStorage;
      #endregion

      #region ------------------------------interface
      public PlayerData DataStructure { get; set; }

      /// <summary>
      /// It creates a new PlayerData and sends it to a data persistence solution.
      /// </summary>
      public void Submit()
      {

      }
      #endregion
   }
}
