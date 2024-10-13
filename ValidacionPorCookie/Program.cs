using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Registro Servicio de Autenticacion Por Cookie
builder.Services
    .AddAuthentication("EsquemaDefault")
    .AddCookie("EsquemaDefault", o => 
    {
        o.Cookie.Name = "UATApiBeneficiarios";
        o.Cookie.HttpOnly = true;
        o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        o.Cookie.SameSite = SameSiteMode.Lax;
        o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        o.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseHttpsRedirection();

app.UseAuthentication();


app.Use(async (context, next) =>
{
    var c = context.User.FindFirst("usu_id");
    if (c != null && c.Value == "123456789")
    {
        var identidad = context.User.Identities.FirstOrDefault();
        if (identidad != null)
        {
            identidad.AddClaim(new Claim("NombreApellido", "Horacio"));
        }
    }
    await next.Invoke();
});

app.UseAuthorization();




app.MapControllers();

app.Run();
