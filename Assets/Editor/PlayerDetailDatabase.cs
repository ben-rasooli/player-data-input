using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlayerDataInput
{
   public class PlayerDetailDatabase : EditorWindow
   {
      #region ------------------------------dependencies
      TemplateContainer _mainWindow;
      ListView _listView;
      #endregion

      [MenuItem("Tools/Player Data Input")]
      public static void ShowWindow()
      {
         var window = GetWindow<PlayerDetailDatabase>();
         window.titleContent = new GUIContent("Player Data Input");
         window.minSize = new Vector2(800, 400);
      }

      void OnEnable()
      {
         _mainWindow = Resources.Load<VisualTreeAsset>("MainWindow").CloneTree();

         var addNewPlayerDetailButton = _mainWindow.Q<Button>("addNewPlayerDetail");
         addNewPlayerDetailButton.clickable.clicked += addNewPlayerDetail;

         var removeSelectedPlayerDetailButton = _mainWindow.Q<Button>("removeSelectedPlayerDetail");
         removeSelectedPlayerDetailButton.clickable.clicked += removeSelectedPlayerDetail;

         var saveButton = _mainWindow.Q<Button>("save");
         saveButton.clickable.clicked += save;

         setupListView();

         var root = rootVisualElement;
         root.styleSheets.Add(Resources.Load<StyleSheet>("Styles"));
         root.Add(_mainWindow);
      }

      #region ------------------------------details
      List<PlayerDetailInput> _playerDetailInputs = new List<PlayerDetailInput>();

      void setupListView()
      {
         var playerDetails = Resources.LoadAll<PlayerDetail>("PlayerDetailInstances");

         _playerDetailInputs.Clear();

         foreach (var playerDetail in playerDetails)
         {
            var clonedPlayerDetail = Instantiate(playerDetail);// it has to be a clone
            _playerDetailInputs.Add(new PlayerDetailInput(clonedPlayerDetail));
         }

         System.Func<VisualElement> playerDetailTemplate = () => Resources.Load<VisualTreeAsset>("PlayerDetailTemplate").CloneTree().contentContainer;

         _listView = _mainWindow.Q<ListView>();
         _listView.makeItem = () => playerDetailTemplate();
         _listView.bindItem = (e, i) => e = _playerDetailInputs[i].bindItem(e);
         _listView.itemsSource = _playerDetailInputs;
      }

      void addNewPlayerDetail()
      {
         _playerDetailInputs.Add(new PlayerDetailInput());
         _listView.Refresh();
      }

      void removeSelectedPlayerDetail()
      {
         if (_listView.selectedIndex < 0)
            return;

         _playerDetailInputs.RemoveAt(_listView.selectedIndex);
         _listView.Refresh();
      }

      // It deletes all the existing PlayerDetail instances
      // and then recreates the new ones.
      void save()
      {
         foreach (var instance in Resources.LoadAll<PlayerDetail>("PlayerDetailInstances"))
            AssetDatabase.DeleteAsset($"Assets/Resources/PlayerDetailInstances/{instance.name}.asset");

         foreach (var playerDetailInput in _playerDetailInputs)
         {
            if (playerDetailInput.PlayerDetail.DisplayName == string.Empty)
               continue;

            AssetDatabase.CreateAsset(
               playerDetailInput.PlayerDetail,
               $"Assets/Resources/PlayerDetailInstances/{playerDetailInput.PlayerDetail.DisplayName}.asset");
         }

         setupListView();
      }

      class PlayerDetailInput
      {
         public PlayerDetail PlayerDetail;

         public PlayerDetailInput()
         {
            PlayerDetail = CreateInstance<PlayerDetail>();
         }

         public PlayerDetailInput(PlayerDetail playerDetail)
         {
            PlayerDetail = playerDetail;
         }

         public VisualElement bindItem(VisualElement visualElement)
         {
            var displayName = visualElement.Q<TextField>("displayName");
            displayName.SetValueWithoutNotify(PlayerDetail.DisplayName);
            displayName.RegisterValueChangedCallback(e => PlayerDetail.DisplayName = e.newValue);

            var validationRegExPattern = visualElement.Q<TextField>("validationRegExPattern");
            validationRegExPattern.SetValueWithoutNotify(PlayerDetail._validationRegExPattern);
            validationRegExPattern.RegisterValueChangedCallback(e => PlayerDetail._validationRegExPattern = e.newValue);

            var validationDescription = visualElement.Q<TextField>("validationDescription");
            validationDescription.SetValueWithoutNotify(PlayerDetail._validationPatternDescription);
            validationDescription.RegisterValueChangedCallback(e => PlayerDetail._validationPatternDescription = e.newValue);

            return visualElement;
         }
      }
      #endregion
   }
}