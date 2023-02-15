using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeto_vendaCarros.Models
{
    public static class ExtensionMethodsModel
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj)
            where TEnum:struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>().Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(TEnum), x),
                    Value = Convert.ToInt64(x).ToString()
                }), "Value", "Text");
        }

        public static SelectList ToSelectList<TEnum>(this TEnum obj, object SelectValue)
            where TEnum:struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(TEnum), x),
                    Value = Convert.ToInt64(x).ToString()
                }), "Value", "Text", SelectValue);
        }
    }
}