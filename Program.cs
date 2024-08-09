using IdentityAuthentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //AddDefaultIdentity configura os Servi�os do Identity. Voc� pode usar optionspara configurar o comportamento da API do
            //Identity.
            //O AddEntityFrameworkStores configura o Identity para usar o Entity Framework Core . Tamb�m precisamos especificar o tipo do Context que vamos usar para os Stores.
            builder.Services.AddDefaultIdentity<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // O Middleware de Autentica��o usa o Default Authentication Handler (Cookie Authentication Handler) para ler os Cookies da resposta e construir o ClaimsPrincipal. Tambem atualiza o User object no HttpContext.
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
