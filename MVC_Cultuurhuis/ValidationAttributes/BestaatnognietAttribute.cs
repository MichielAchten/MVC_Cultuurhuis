using MVC_Cultuurhuis.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.ValidationAttributes
{
    public class BestaatnognietAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (!(value is string))
            {
                return false;
            }
            else
            {
                CultuurService db = new CultuurService();
                return !db.BestaatKlant((string)value);
            }
        }
    }
}