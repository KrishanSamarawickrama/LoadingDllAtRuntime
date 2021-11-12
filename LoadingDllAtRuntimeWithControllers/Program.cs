using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

var pluginModules = builder.Configuration.GetSection("Plugin-Modules").Get<ModuleInfo[]>();
foreach (var module in pluginModules ?? Enumerable.Empty<ModuleInfo>())
{
    var DLL = Assembly.LoadFile(module.Path);
    Type? type = DLL.GetExportedTypes().SingleOrDefault(x => x.Name.Equals("ModuleExtensions"));
    if (type != null)
    {
        var c = Activator.CreateInstance(type);
        type.InvokeMember("AddModule", BindingFlags.InvokeMethod, null, c, new object[] { builder.Services });
    }
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(type => type.ToString());
});

#endregion Add services to the container

var app = builder.Build();

#region Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion Configure the HTTP request pipeline

app.Run();

public class ModuleInfo
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}