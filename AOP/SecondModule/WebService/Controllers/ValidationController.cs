using System;
using System.Web.Http;
using System.Web.Http.Results;
using WebService.Models;
using Castle.DynamicProxy;
using LINQ2JSLibrary.Implementations;
using LINQ2JSLibrary.Loggers;

namespace WebService.Controllers
{
    public class ValidationController : ApiController
    {
        private Translator<UserInfo, Func<UserInfo, bool>> actionTranslator;
        private Translator<UserInfo, string> jsTranslator;

        public ValidationController()
        {
            var generator = new ProxyGenerator();
            this.actionTranslator =
                generator.CreateClassProxyWithTarget<Translator<UserInfo, Func<UserInfo, bool>>>(
                    new ActionValidationsTranslator<UserInfo>(), new DynamicProxyLogger());
            this.jsTranslator =
                generator.CreateClassProxyWithTarget<Translator<UserInfo, string>>(
                    new JSValidationsTranslator<UserInfo>(), new DynamicProxyLogger());
        }

        [HttpPost]
        public JsonResult<ValidationResponse> ValidateUser(ValidationRequest validationRequest)
        {
            var validationsStorage = new ValidationsStorage<UserInfo>();
            validationsStorage.AddValidation(user => user.Name, name => name.StartsWith("Maksim"));
            validationsStorage.AddValidation(user => user.Surname, surname => surname.Contains("Harb"));
            validationsStorage.AddValidation(user => user.Age, age => age > 20);
            validationsStorage.AddValidation(user => user.Age, age => age <= 100);

            var newResponse = new ValidationResponse
            {
                IsValid = this.actionTranslator.Translate(validationsStorage)(validationRequest.User),
                ValidationFunction = this.jsTranslator.Translate(validationsStorage)
            };

            return Json(newResponse);
        }
    }
}
