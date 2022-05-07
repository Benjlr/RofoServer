using System;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Core.Utils.Validators;

public class SizeCheckAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        try {
            var data = ((string)value).Split(',')[1];
            var byteLength = (Convert.FromBase64String(data)).LongLength;
            if (byteLength > 50000000)
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
        catch (Exception ex) {
            return new ValidationResult(this.FormatErrorMessage("Can't convert to base64 string"));
        }

        return null;
    }
}