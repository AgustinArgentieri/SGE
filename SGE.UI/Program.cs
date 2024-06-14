using SGE.Aplicacion;
using SGE.UI.Components;

//agrego directivas using
using SGE.Repositorios;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//

builder.Services.AddTransient<CasoDeUsoUsuarioConsultaRegistrado>();
builder.Services.AddTransient<CasoDeUsoUsuarioAlta>();
builder.Services.AddScoped<IUsuarioRepositorio, SGESqlite>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
