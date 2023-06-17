using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Nop.Core.Configuration;
using System.Configuration;
using Nop.Web.Framework.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Configuration.AddJsonFile(NopConfigurationDefaults.AppSettingsFilePath, true, true);
if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
{
    var path = string.Format(NopConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
    builder.Configuration.AddJsonFile(path, true, true);
}

builder.Configuration.AddEnvironmentVariables();


//Add services to the application and configure service provider

// Register the ServiceClient as a service
//string clientId = builder.Configuration.GetSection("appSettings").GetValue("ClientSecret")
//string mySettingValue = System.Configuration.ConfigurationManager.AppSettings["ClientSecret"] ;
//string clientSecret = builder.Configuration.GetSection("ClientSecret").Value;
//string urlInstance = builder.Configuration.GetSection("UrlInstance").Value;

//string connectionString = $"AuthType=ClientSecret;Url={urlInstance};ClientId={clientId};ClientSecret={clientSecret}";

string connectionString = $"AuthType=ClientSecret;Url=https://tllinterviews.crm4.dynamics.com/apps/CS;ClientId=23047e6f-c359-4cb3-8dcd-4f08373fead5;ClientSecret=pyH8Q~fLdAZzp5S9LI46W-Ab9eSZxWl7pZHbnaay";

builder.Services.AddSingleton<IOrganizationService>(provider =>
{
    ServiceClient serviceClient = new ServiceClient(connectionString);
    return serviceClient;
});


builder.Services.ConfigureApplicationServices(builder);


var app = builder.Build();

//Configure the application HTTP request pipeline
app.ConfigureRequestPipeline();
app.StartEngine();

app.Run();
