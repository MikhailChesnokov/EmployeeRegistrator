namespace Web.Application.Services.HtmlLayoutGenerator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;



    public class RazorHtmlLayoutGenerator : IRazorHtmlLayoutGenerator
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;



        public RazorHtmlLayoutGenerator(
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IRazorViewEngine razorViewEngine)
        {
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _razorViewEngine = razorViewEngine;
        }



        public async Task<string> RenderAsync(string viewName, dynamic model)
        {
            using (var writer = new StringWriter())
            {
                var actionContext = new ActionContext(new DefaultHttpContext { RequestServices = _serviceProvider }, new RouteData(), new ActionDescriptor());

                if (_razorViewEngine.FindView(actionContext, $"{viewName}", false) is ViewEngineResult viewEngineResult)
                {
                    if (viewEngineResult.View == null)
                        throw new FileNotFoundException($"View \"{viewName}\" not found.");

                    var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };

                    var viewContext = new ViewContext(
                        actionContext,
                        viewEngineResult.View,
                        viewDataDictionary,
                        new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                        writer,
                        new HtmlHelperOptions());

                    await viewEngineResult.View.RenderAsync(viewContext);

                    return writer.ToString();
                }

                throw new FileNotFoundException($"View \"{viewName}\" not found.");
            }
        }
    }
}