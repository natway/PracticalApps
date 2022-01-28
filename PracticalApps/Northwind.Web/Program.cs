using Northwind.Web; // Startup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => {
    webBuilder.UseStartup<Startup>();
}).Build().Run();


//if (!app.Environment.IsDevelopment())
//{
//    app.UseHsts();
//}
//app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");

//app.Run();
Console.WriteLine("This executes after the web server has stopped!");