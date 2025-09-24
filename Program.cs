using Microsoft.OpenApi.Models;


List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name ="Tom", Age = 37},
    new() { Id = Guid.NewGuid().ToString(), Name ="bob", Age = 41},
    new() { Id = Guid.NewGuid().ToString(), Name ="Sam", Age = 24}
 };


var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);
//app.MapGet("/", () => "Hello World!");
app.MapGet("/api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "the usser not found" });

    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "the usser not found" });    
    

    users.Remove(user);
    return Results.Json(user);  
});

app.MapPost("/api/users", (Person user) =>
{
    user.Id = Guid.NewGuid().ToString();
    users.Add(user);

    foreach (var u in users) { Console.WriteLine($"User:{u.Id}|{u.Name}|{u.Age}"); }
    return user;
});

app.MapPut("/api/users", (Person userData) =>
{
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    if (user == null) return Results.NotFound(new { message = "the usser not found" });

    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

//app.Run(async (context) => await context.Response.WriteAsync("Hello World"));
app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
    /* public string Description { get; set; }
     public string Email { get; set; }
     public string Address { get; set; }
     public string City { get; set; }
     public string Region { get; set; }
     public string PostalCode { get; set; }
     public string Country { get; set; }
     public string Phone { get; set; }
     public string Fax { get; set; }
     public string FaxNumber { get; set; }
     public string PhoneNumber { get; set; }
     public string HomePage { get; set; }
     public string Extension { get; set; }*/
}
