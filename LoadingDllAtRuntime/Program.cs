using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var DLL = Assembly.LoadFile(@"..\LoadingDllAtRuntime\Module1\bin\Debug\net6.0\Module1.dll");
foreach (Type type in DLL.GetExportedTypes().Where(x => x.Name.Equals("ModuleStartup")))
{
    var c = Activator.CreateInstance(type);
    //type.InvokeMember("Output", BindingFlags.InvokeMethod, null, c, new object[] { @"Hello" });
    type.InvokeMember("AddModule", BindingFlags.InvokeMethod, null, c, new object[] { app });
}

app.Run();