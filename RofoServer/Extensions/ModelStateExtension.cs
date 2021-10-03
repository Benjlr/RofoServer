using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RofoServer.Extensions
{
    public static class ModelStateExtension
    {
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            return string.Join(",", modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }
    }
}