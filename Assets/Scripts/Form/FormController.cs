using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
         removeTextInputGameObjects();

         foreach (var playerDetail in DataStructure)
         {
            if (!playerDetail.IsEnable)
               continue;

            var textInput = Instantiate(_textInputPrefab, _textInputsTransform).transform;
            textInput.Find("Label").GetComponent<TextMeshProUGUI>().text = playerDetail.Name;
            _textInputFietds.Add(textInput.GetComponent<TMP_InputField>());
         }

         gameObject.SetActive(true);
      }
      [SerializeField] GameObject _textInputPrefab;
      [SerializeField] Transform _textInputsTransform;

      /// <summary>
      /// It creates a new PlayerData and sends it to a data persistence solution.
      /// </summary>
      public void Submit()
      {

      }
      List<TMP_InputField> _textInputFietds = new List<TMP_InputField>();
      #endregion

      #region ------------------------------details
      void removeTextInputGameObjects()
      {
         var children = _textInputsTransform.GetComponentsInChildren<Transform>();

         for (int i = 1; i < children.Length; i++)
         {
            children[i].parent = null;
            Destroy(children[i].gameObject);
         }
      }
      #endregion
   }
}
