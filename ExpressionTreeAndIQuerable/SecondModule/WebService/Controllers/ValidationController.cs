using System.Web.Http;
using System.Web.Http.Results;
using WebService.Models;
using LINQ2JSLibrary.Implementations;
using LINQ2JSLibrary.Interfaces;
using LINQ2JSLibrary.OldImplementations;
using LINQ2JSLibrary.OldInterfaces;

namespace WebService.Controllers
{
    public class ValidationController : ApiController
    {
        [HttpPost]
        public JsonResult<ValidationResponse> ValidateUser(ValidationRequest validationRequest)
        {
            /*var validatedModel = new ValidatedModel<UserInfo, string>(new JSValidationProvider<UserInfo>());
            string jsFunction = validatedModel
                .StartWith(m => m.Name, "Maksim")
                .StartWith(m => m.Surname, "Harbachou")
                .GreaterThan(m => m.Age, 20)
                .BuildValidation();

            var response = new ValidationResponse
            {
                IsValid = true,
                ValidationFunction = jsFunction
            };*/

            var validationsStorage = new ValidationsStorage<UserInfo>();
            validationsStorage.AddValidation(user => user.Name, name => name.StartsWith("Maksim"));
            validationsStorage.AddValidation(user => user.Surname, surname => surname.Contains("Harb"));
            validationsStorage.AddValidation(user => user.Age, age => age > 20);
            validationsStorage.AddValidation(user => user.Age, age => age <= 100);

            var newResponse = new ValidationResponse
            {
                IsValid = new ActionValidationsTranslator<UserInfo>().Translate(validationsStorage)(validationRequest.User),
                ValidationFunction = new JSValidationsTranslator<UserInfo>().Translate(validationsStorage)
            };

            return Json(newResponse);
        }
    }
}
