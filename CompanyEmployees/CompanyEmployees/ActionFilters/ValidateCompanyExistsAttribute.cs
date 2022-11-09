using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var id =(Guid)context.ActionArguments["id"];
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            //var id =Guid.Parse ( context.RouteData.Values["id"].ToString());
            var company = _repository.Company.GetCompany(id, trackChanges);
            if (company == null)
            {
                _logger.LogError($"{ action} Company with id: {id} doesn't exist in the database.");
                context.Result =new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("company", company);
            }
        }
    }
}
