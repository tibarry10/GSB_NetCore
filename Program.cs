var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // ✅ Cache en mémoire pour la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600); // Durée d'inactivité : 10 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ✅ Activation de la session AVANT Authorization

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Exemple de route personnalisée (modifiable selon ton projet)
app.MapControllerRoute(
    name: "gsb",
    pattern: "gsb",
    defaults: new
    {
        controller = "Praticien",
        action = "Index"
    }
);

app.Run();
