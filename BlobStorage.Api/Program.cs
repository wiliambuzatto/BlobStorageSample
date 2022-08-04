using Azure.Storage.Blobs;
using BlobStorage.Api.Persistence.Queries;
using BlobStorage.Api.Services;
using BlobStorage.Api.Services.Notifier;
using BlobStorage.Api.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(x => 
        new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

builder.Services.AddSingleton<IBlobService, BlobService>();
builder.Services.AddScoped<CompanyQueries, CompanyQueries>();
builder.Services.AddScoped<UploadXmlUseCase, UploadXmlUseCase>();
builder.Services.AddScoped<INotifier, Notifier>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
