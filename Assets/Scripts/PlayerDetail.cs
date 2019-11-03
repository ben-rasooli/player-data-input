﻿using System.Text.RegularExpressions;
using UnityEngine;

namespace PlayerDataInput
{
   [CreateAssetMenu(menuName = "PlayerDataInput/PlayerDetail", fileName = "PlayerDetail")]
   public class PlayerDetail : ScriptableObject
   {
      public string Name;
      public string Value;
      public bool IsRequire;

      public string Validate()
      {
         return Regex.IsMatch(Value, _validationRegExPattern)
            ? string.Empty
            : $"{Name} is not valid";
      }
      [SerializeField] string _validationRegExPattern;
   }
}