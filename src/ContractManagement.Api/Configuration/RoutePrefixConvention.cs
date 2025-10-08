using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ContractManagement.Api.Configuration
{
    public class RoutePrefixConvention(IRouteTemplateProvider route) : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix = new AttributeRouteModel(route);

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    selector.AttributeRouteModel = _routePrefix;
                }

            }
        }
    }
}
