using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using ventasSOAP.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

builder.Services.AddSingleton<IFACTURA, FacturaService>();
builder.Services.AddSingleton<FacturaService>();
builder.Services.AddSingleton<ServiceMetadataBehavior>();();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<FacturaService>();
    serviceBuilder.AddServiceEndpoint<FacturaService, IFACTURA>(
        new BasicHttpBinding(), 
        "/FacturaService.svc");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;
// serviceMetadataBehavior.HttpsGetEnabled = false;

app.Run();
