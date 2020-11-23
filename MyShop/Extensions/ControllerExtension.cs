using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MyShop.Extensions
{
    public static class ControllerExtension
    {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName,
            TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices
                    .GetService(typeof(ICompositeViewEngine)) as CompositeViewEngine;

                var viewEngineResult =
                    viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewEngineResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }
                
                var viewContext = new ViewContext(controller.ControllerContext, viewEngineResult.View,
                    controller.ViewData, controller.TempData, writer, new HtmlHelperOptions());

                await viewEngineResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}