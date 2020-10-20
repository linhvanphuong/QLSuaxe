using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Portal.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace APP.MODELS
{
    public class CustomAuthenAttribute: Attribute, IAuthorizationFilter
    {
        private string _actionCode;
        public CustomAuthenAttribute(string actionCode = "")
        {
            this._actionCode = actionCode;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            //this.GetType().CustomAttributes
            //.FirstOrDefault(r => r.AttributeType == typeof(RouteAttribute))
            //.ConstructorArguments[0].Value.ToString()
            //.Replace("[controller]", (string)this.RouteData.Values["controller"]);
            var session = context.HttpContext.Session;
            if (context != null)
            {
                Accounts account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(session, Portal.Utils.SessionExtensions.SesscionPermission);
                if (permission == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "tai-khoan",
                                action = "dang-nhap"
                            }));
                }

                else
                {
                    if (_actionCode == "NoCheck")
                    {

                    }
                    else
                    {
                        var path = context.HttpContext.Request.Path.Value.ToString();

                        //var controller = new ControllerContext().ActionDescriptor.ControllerName;
                        var controller = actionDescriptor.ControllerTypeInfo.CustomAttributes.FirstOrDefault().ConstructorArguments[0].Value.ToString();//lấy  tên controll
                        var exist = permission.Where(c => c.MenuUrl.ToString().ToLower().Contains(controller)).ToList();

                        if (exist.Count == 0)
                        {
                            context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Error",
                                action = "NoPermission"
                            }
                            ));
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_actionCode))
                            {
                                var control = exist.Find(c => c.ActionCode == _actionCode);
                                if (control == null)
                                {
                                    context.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(
                                    new
                                    {
                                        controller = "Error",
                                        action = "NoPermission"
                                    }
                                    ));
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }

    }
}
