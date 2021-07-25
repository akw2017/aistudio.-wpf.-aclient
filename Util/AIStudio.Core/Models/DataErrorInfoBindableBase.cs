﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AIStudio.Core.Models
{
    public class DataErrorInfoBindableBase : BindableBase, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                List<ValidationResult> validationResults = new List<ValidationResult>();

                bool result = Validator.TryValidateProperty(
                    GetType().GetProperty(columnName).GetValue(this),
                    new ValidationContext(this)
                    {
                        MemberName = columnName
                    },
                    validationResults);

                if (result)
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}
