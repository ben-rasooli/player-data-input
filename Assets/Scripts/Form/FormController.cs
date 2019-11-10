using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace PlayerDataInput
{
   public class FormController : MonoBehaviour
   {
      #region ------------------------------dependencies
      [SerializeField] IScoreReporter _scoreReporter;
      [SerializeField] DataStorage _dataStorage;
      #endregion

      #region ------------------------------interface
      public List<PlayerDetail> DataStructure { get; set; }

      public void Show()
      {
         DataStructure.Where(playerDetail => playerDetail.IsEnable).ToList().ForEach(playerDetail =>
         {
            var textInputController = Instantiate(_textInputPrefab, _textInputsTransform).GetComponent<TextInputController>();
            textInputController.Setup(playerDetail);
            _textInputControllers.Add(textInputController);
         });

         gameObject.SetActive(true);
      }
      [SerializeField] GameObject _textInputPrefab;
      [SerializeField] Transform _textInputsTransform;

      public void Hide()
      {
         gameObject.SetActive(false);
      }
      #endregion

      #region ------------------------------details
      void Start()
      {
         _submitButton = transform.Find("Content/Buttons/Submit - Button").GetComponent<Button>();
         _submitButton.onClick.AddListener(submit);
      }
      Button _submitButton;

      void OnDisable()
      {
         removeTextInputs();
      }

      /// <summary>
      /// It creates a new PlayerData and sends it to a data persistence solution.
      /// </summary>
      void submit()
      {
         int validTextInputsCount = _textInputControllers
            .Select(c => c.Validate())
            .Where(x => x)
            .Count();

         if (validTextInputsCount == _textInputControllers.Count)
         {
            List<string> inputValues = _textInputControllers.Select(c => c.Value).ToList();

            //save player data
            inputValues.ForEach(v => print(v));
         }
      }
      List<TextInputController> _textInputControllers = new List<TextInputController>();

      void removeTextInputs()
      {
         for (int i = 0; i < _textInputControllers.Count; i++)
         {
            _textInputControllers[i].transform.parent = null;
            Destroy(_textInputControllers[i].gameObject);
         }

         _textInputControllers.Clear();
      }
      #endregion
   }
}
