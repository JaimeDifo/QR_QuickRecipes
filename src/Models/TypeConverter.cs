using System;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using CsvHelper.TypeConversion;

namespace QuickRecipes.WebSite.Models
{
    public class StringListConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var list = (List<string>)value;
            return string.Join(",", list);
        }


        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<string>();
            }

            List<string> listText = new List<string>(text.Replace("[", "").Replace("]", "").Split("\","));
            for(int i =0; i< listText.Count; i++)
            {
                listText[i] = listText[i].Replace("\"", "");
            }
            return listText;
        }

    }
}


