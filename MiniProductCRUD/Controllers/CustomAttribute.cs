using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiniProductCRUD.Models;

namespace MiniProductCRUD.Controllers
{
    public class CustomAttribute : Attribute, IActionFilter
    {

        //Токен из задания
        //Токен может получаться из БД или с сервиса аутентификации
        private readonly string _token = "TS3qVh70xrM59VC9OxqK3UZV";

       public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

       
        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            context.ActionArguments.TryGetValue("token", out token);
            //Токен не указан
            if (token==null || string.IsNullOrEmpty(token.ToString()))
            {
                context.Result=new JsonResult(new OperationResult<string>()
                {
                    Code = ResultCodes.TokenMissing,
                    ErrorMessage = "Token is missing",
                    Data = "Token has to be pointed"
                });
                //Оставлен на случай доработки метода
                return;
            }
            //Указан не корректный токен
            else if (token.ToString() != _token)
            {
                context.Result= new JsonResult(new OperationResult<string>()
                {
                    Code = ResultCodes.TokenError,
                    ErrorMessage = "Token is incorrect",
                    Data="Token is incorrect"

                });
                //Оставлен на случай доработки метода
                return;
            }

        }
    }
}