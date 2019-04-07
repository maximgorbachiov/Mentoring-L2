using System.Web.Http;
using System.Web.Http.Results;
using WebService.Models;
using LINQ2JSLibrary;

namespace WebService.Controllers
{
    public class ValidationController : ApiController
    {
        [HttpPost]
        public JsonResult<ValidationResponse> ValidateUser(ValidationRequest validationRequest)
        {
            var validatedModel = new ValidatedModel<UserInfo, string>(new JSValidationProvider<UserInfo>());
            string jsFunction = validatedModel
                .StartWith(m => m.Name, "Maksim")
                .StartWith(m => m.Surname, "Harbachou")
                .GreaterThan(m => m.Age, 20)
                .BuildValidation();

            var response = new ValidationResponse
            {
                IsValid = true,
                ValidationFunction = jsFunction
            };
            return Json(response);
        }
    }
}
