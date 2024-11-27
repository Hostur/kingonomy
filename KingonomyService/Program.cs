using KingonomyService.DB;
using KingonomyService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DBProvider>();
builder.Services.AddSingleton<ItemsService>();
builder.Services.AddSingleton<UnityAuthorizationService>();
builder.Services.AddSingleton<PurchaseService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = ""; // ToDo Get configuration connection string for redis.
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbProvider = scope.ServiceProvider.GetRequiredService<DBProvider>();
    await dbProvider.AssertDb();
}

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
