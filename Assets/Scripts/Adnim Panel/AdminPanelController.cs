using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerDataInput
{
   public class AdminPanelController : MonoBehaviour
   {
      [SerializeField] FormController _formController;
      [SerializeField] GameObject _detailOptionsPrefab;
      [SerializeField] Transform _detailsTransform;
      [SerializeField] List<PlayerDetail> _playerDetailTemplates;
      [SerializeField] Button _saveButton;

      void Show()
      {
         gameObject.SetActive(true);
      }

      void Start()
      {
         foreach (var playerDetailTemplate in _playerDetailTemplates)
         {
            _playerDetails.Add(Instantiate(playerDetailTemplate));
            var detailOptionsTransform = Instantiate(_detailOptionsPrefab, _detailsTransform).transform;
            detailOptionsTransform.Find("Label").GetComponent<TextMeshProUGUI>().text = playerDetailTemplate.Name;

            var toggls = new List<Toggle>(detailOptionsTransform.GetComponentsInChildren<Toggle>());
            _listOfDetailOptions.Add(playerDetailTemplate.Name, toggls);
         }

         _saveButton.onClick.AddListener(save);
      }

      void save()
      {
         foreach (var playerDetail in _playerDetails)
         {
            playerDetail.IsEnable = _listOfDetailOptions[playerDetail.Name][0].isOn;
            playerDetail.IsRequire = _listOfDetailOptions[playerDetail.Name][1].isOn;
         }
         _formController.DataStructure = _playerDetails;
         _formController.Show();
         gameObject.SetActive(false);
      }
      List<PlayerDetail> _playerDetails = new List<PlayerDetail>();
      Dictionary<string, List<Toggle>> _listOfDetailOptions = new Dictionary<string, List<Toggle>>();
   }
}
