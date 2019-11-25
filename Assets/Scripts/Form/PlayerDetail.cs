using System.Text.RegularExpressions;
using UnityEngine;

namespace PlayerDataInput
{
    public struct DetailData
    {
        private string detailID;
        public string Name;
        public string validationRegExPattern;
        public string validationPatternDescription;

        public void SetDetailID(string name) => detailID = name;
    }

    [CreateAssetMenu(menuName = "PlayerDataInput/PlayerDetail", fileName = "PlayerDetail")]
    public class PlayerDetail : ScriptableObject
    {
        public string Name;
        public bool IsEnable;
        public bool IsRequire;
        public string validationRegExPattern;
        public string validationPatternDescription;

        public string Value { get; set; }

        public DetailData GetDetailData()
        {
            DetailData data = new DetailData();
            data.SetDetailID(this.name);
            data.Name = Name;
            data.validationRegExPattern = validationRegExPattern;
            data.validationPatternDescription = validationPatternDescription;

            return data;
        }

        public string Validate()
        {
            if (Value == string.Empty && !IsRequire)
                return string.Empty;

            return Regex.IsMatch(Value, validationRegExPattern)
               ? string.Empty
               : validationPatternDescription;
        }

    }
}
