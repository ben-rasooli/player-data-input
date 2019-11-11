using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlayerDataInput
{
   public class TextInputController : MonoBehaviour
   {
      #region ------------------------------dependencies
      PlayerDetail _playerDetail;
      [SerializeField] TextMeshProUGUI _labelUI;
      [SerializeField] TextMeshProUGUI _errorMessageUI;
      [SerializeField] TMP_InputField _inputField;
      #endregion

      #region ------------------------------interface
      /// <summary>
      /// This method should be called before any other method.
      /// </summary>
      /// <param name="playerDetail"></param>
      public void Setup(PlayerDetail playerDetail)
      {
         _playerDetail = playerDetail;
         _labelUI.text = _playerDetail.Name;
      }

      /// <summary>
      /// It validates the input value.
      /// </summary>
      /// <returns>It returns true if no error message found</returns>
      public bool Validate()
      {
         var errorMessage = _playerDetail.Validate();
         _errorMessageUI.text = errorMessage;

         if (errorMessage == string.Empty)
            return true;

         return false;
      }

      public PlayerDetail Value
      {
         get { return _playerDetail; }
      }
      #endregion

      #region ------------------------------details
      void Start()
      {
         _inputField.onValueChanged.AddListener(v => _playerDetail.Value = v);
      }
      #endregion
   }
}
