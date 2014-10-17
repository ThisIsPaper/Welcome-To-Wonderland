
namespace Wonderland.Logic.Extensions
{
    using System.Collections.Generic;
    using System.Linq;    
    using Wonderland.Logic.Models.Messages;

    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// MVC form validation messages for Angular
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<FormResponseFieldErrors> GetErrors(this System.Web.Mvc.ModelStateDictionary modelState)
        {
            List<FormResponseFieldErrors> errors = new List<FormResponseFieldErrors>();

            foreach (var fieldErrors in modelState.Where(x => x.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors }))
            {
                errors.Add(
                    new FormResponseFieldErrors()
                    {
                        Field = fieldErrors.Key,
                        ErrorMessages = fieldErrors.Errors.Select(x => x.ErrorMessage)
                    });
            }

            return errors;
        }

        public static IEnumerable<FormResponseFieldErrors> GetErrors(this System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            List<FormResponseFieldErrors> errors = new List<FormResponseFieldErrors>();

            foreach (var fieldErrors in modelState.Where(x => x.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors }))
            {
                errors.Add(
                    new FormResponseFieldErrors()
                    {
                        Field = fieldErrors.Key,
                        ErrorMessages = fieldErrors.Errors.Select(x => x.ErrorMessage)
                    });
            }

            return errors;
        }
    }
}
