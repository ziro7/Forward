using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Core.Validation
{
    class StartDateValidator : ValidationAttribute
    {
        public string MyBirthday { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            // The CLR don't accept datetime so have to get a string as validation paramater and change it.
            DateTime myBirthdayInDateTime = DateTime.ParseExact(MyBirthday,"dd-mm-yyyy", CultureInfo.InvariantCulture);
            DateTime startDate;

            if(DateTime.TryParse(value.ToString(),out startDate)) {

                if (startDate > myBirthdayInDateTime) {
                    return null; // Validation is ok.
                } else {
                    return new ValidationResult("The startdate can't be before I was born!", new[] { validationContext.MemberName });
                }
            }
            // If value can't be parse to a date.
            return new ValidationResult("Invalide date ", new[] { validationContext.MemberName });

            
        }
    }
}
