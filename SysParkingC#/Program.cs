using Microsoft.EntityFrameworkCore;
using SysParkingC_.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext com a string de conexão
builder.Services.AddDbContext<SysParkingC_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SysParkingC_Context") ??
        throw new InvalidOperationException("Connection string 'SysParkingC_Context' not found.")));

// Adicionar suporte para Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar tratamento de erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configurar tratamento de 404 (página não encontrada)
app.UseStatusCodePagesWithReExecute("/Home/PageNotFound");

// Configurar redirecionamentos seguros e inicialização estática
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configurar rotas padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Executar a aplicação
app.Run();
