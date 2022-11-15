using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CompanyEmployees.ActionFilters
{
    public class ValidateCompanyExistsAttribute : IActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateCompanyExistsAttribute(ILoggerManager loggeer, IRepositoryManager repository)
        {
            _logger = loggeer;
            _repository = repository;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();

            Guid id;
            if (controller.ToString() == "Employees")
            {
                id = (Guid)context.ActionArguments["companyId"];
            }
            else
            {
                id = (Guid)context.ActionArguments["id"];
            }

            //var id = (controller.ToString() == "Employees") ?
            //(Guid)context.ActionArguments["companyId"] :
            //(Guid)context.ActionArguments["id"];

            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            //var id =Guid.Parse ( context.RouteData.Values["id"].ToString());
            var company = _repository.Company.GetCompany(id, trackChanges);
            if (company == null)
            {
                _logger.LogError($"{action}  with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("company", company);
            }
        }
    }
}