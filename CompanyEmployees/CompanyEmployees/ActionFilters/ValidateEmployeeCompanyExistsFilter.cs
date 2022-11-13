using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.ActionFilters
{
    public class ValidateEmployeeCompanyExistsFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateEmployeeCompanyExistsFilter(ILoggerManager loggeer, IRepositoryManager repository)
        {
            _logger = loggeer;
            _repository = repository;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();

            var companyId = (Guid)context.ActionArguments["companyId"];
            var id = (Guid)context.ActionArguments["id"];

            //var id = (controller.ToString() == "Employees") ?
            //(Guid)context.ActionArguments["companyId"] :
            //(Guid)context.ActionArguments["id"];

            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            //var id =Guid.Parse ( context.RouteData.Values["id"].ToString());
            var employee = _repository.Employee.GetEmployee(companyId,id, trackChanges);
            if (employee == null)
            {
                _logger.LogError($"{action} employee  with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("employee", employee);
            }

        }
    }
}
