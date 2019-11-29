using System.Text.RegularExpressions;
using UnityEngine;

namespace PlayerDataInput
{
   [CreateAssetMenu(menuName = "Player Data Input/Player Detail", fileName = "Player Detail")]
   public class PlayerDetail : ScriptableObject
   {
      public string DisplayName;
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
      public string _validationRegExPattern;
      public string _validationPatternDescription;
   }
}
