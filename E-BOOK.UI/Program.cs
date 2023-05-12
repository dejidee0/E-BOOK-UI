global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;

using E_BOOK.UI;
using E_BOOK.UI.Data;
using E_BOOK.UI.Service.Interface;
using E_BOOK.UI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IAccountHttpService, AccountHttpService>(client => client.BaseAddress = new Uri("https://localhost:7003/"));
builder.Services.AddHttpClient<IBookHttpService, BookHttpService>(client => client.BaseAddress = new Uri("https://localhost:7003/"));
builder.Services.AddHttpClient<IReviewHttpService, ReviewHttpService>(client => client.BaseAddress = new Uri("https://localhost:7003/"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAuthenticationCore();
builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
