# DDDTW.PollyAttribute.Backward
This repo is for applying PollyAttribute to .Net Framework 4.6.1 and latest version.

### Provision
There are some differences in .net framework and .net core; If your project is based on .net core, congratulation you! you will feel easy to integrate PollyAttribute into your project. If you use older .Net Framewok(I mean .Net Framework version 4.6.1 early), please update your project's .Net Framework version to 4.6.1 or latest version.

You just need to install [DDDTW.PollyAttribute.Backward nuget package](https://www.nuget.org/packages/DDDTW.PollyAttribute.Backward/). After you install package success, then please add a line of code in Global.asax

```csharp
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var svcCollection = new PollyServiceCollection();
            svcCollection.AddTransient<IProductSvc, ProductSvc>();
            svcCollection.CompleteRegistration();
        }
    }
```

Then you can add PollyAttribute on specific method:

```csharp
    public interface IProductService
    {
        string GetProductName();

        string GetProductName_Failback();
    }

    public class ProductService : IProductService
    {
        private static int counter = 0;

        [PollyAsync(FallBackMethod = nameof(GetProductName_Failback), IsEnableCircuitBreaker = true)]
        public string GetProductName()
        {
            if (counter++ >= 3)
                throw new Exception();
            return "Prd";
        }

        public string GetProductName_Failback()
        {
            return "Prd Fall_back";
        }
     }
```
