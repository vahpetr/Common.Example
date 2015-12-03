using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using Common.Exceptions;

namespace OlegTask.Web.Attributes
{
    /// <summary>
    /// Перехватчик исключений
    /// </summary>
    public class ExceptionInterceptorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var e = context.Exception;
            while (e.InnerException != null) e = e.InnerException;

            var type = e.GetType();
            if (type == typeof(ValidationServiceException))
            {
                var exception = (ValidationServiceException)e;
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exception.Message),
                    ReasonPhrase = "Validation Service Exception"
                };
            }
            else if (type == typeof(DbEntityValidationException))
            {
                var exception = (DbEntityValidationException)e;
                var sb = new StringBuilder();
                foreach (var validationResult in exception.EntityValidationErrors)
                {
                    var errors = validationResult.ValidationErrors.ToArray();
                    for (int i = 0, l = errors.Length; i < l; i++)
                    {
                        sb.AppendLine($"{i}. {errors[i].ErrorMessage}{Environment.NewLine}");
                    }
                }

                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(sb.ToString()),
                    ReasonPhrase = "DbEntity Validation Exception"
                };

                context.Exception = null;
            }

            base.OnException(context);
        }
    }
}