using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace EasyCommerce.Server.Shared.Common.Attributes;

public class CustomApiControllerAttribute : Attribute, IRouteTemplateProvider
{
    public string Template => "/api/[controller]/";

    public int? Order => 0;

    public string Name { get; set; } = string.Empty;
}
