using TMPro;
using UnityEngine;

namespace PlayerDataInput
{
   public class TextInputController : MonoBehaviour
   {
      #region ------------------------------dependencies
      PlayerDetail _playerDetail;
      TextMeshProUGUI _labelUI;
      TextMeshProUGUI _errorMessageUI;
      TMP_InputField _inputField;
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

      public string Value { get { return _playerDetail.Value; } }
      #endregion

      #region ------------------------------details
      void Awake()
      {
         _labelUI = transform.Find("Label").GetComponent<TextMeshProUGUI>();
         _errorMessageUI = transform.Find("ErrorMessage").GetComponent<TextMeshProUGUI>();
         _inputField = transform.GetComponentInChildren<TMP_InputField>();
      }

      private void Start()
      {
         _inputField.onValueChanged.AddListener(v => _playerDetail.Value = v);
      }
      #endregion
   }
}
