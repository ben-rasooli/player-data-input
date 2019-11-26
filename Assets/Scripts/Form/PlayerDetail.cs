using System.Text.RegularExpressions;
using UnityEngine;

namespace PlayerDataInput
{
   [CreateAssetMenu(menuName = "Player Data Input/Player Detail", fileName = "Player Detail")]
   public class PlayerDetail : ScriptableObject
   {
      public string Name;
      [HideInInspector] public string Value;
      [HideInInspector] public bool IsEnable;
      [HideInInspector] public bool IsRequire;

      public string Validate()
      {
         if (Value == string.Empty && !IsRequire)
            return string.Empty;

         return Regex.IsMatch(Value, _validationRegExPattern)
            ? string.Empty
            : _validationPatternDescription;
      }
      [SerializeField] string _validationRegExPattern;
      [SerializeField] string _validationPatternDescription;
   }
}
