var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure reCAPTCHA
builder.Services.AddRecaptcha(options =>
{
    options.SiteKey = builder.Configuration["RecaptchaSettings:SiteKey"];
    options.SecretKey = builder.Configuration["RecaptchaSettings:SecretKey"];
});

builder.Services.Configure<GraphSettings>(builder.Configuration.GetSection("GraphSettings"));

builder.Services.AddSingleton<TokenCredential>(sp =>
    new ClientSecretCredential(
        builder.Configuration["GraphSettings:TenantId"],
        builder.Configuration["GraphSettings:ClientId"],
        builder.Configuration["GraphSettings:ClientSecret"]
    )
);

builder.Services.AddScoped<EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePagesWithReExecute("/NotFound");

app.Run();