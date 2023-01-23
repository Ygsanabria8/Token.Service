using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WaoVet.Token.Service.Domain;
using WaoVet.Token.Service.Infrastructure;
using WaoVet.Token.Service.Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
            , sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)
        );
})
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                        , sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)
                );
            })
.AddAspNetIdentity<UserModel>()
.AddDeveloperSigningCredential();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
Config.InitializeDatabase(app);
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();
app.Run();
