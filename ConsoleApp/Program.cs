using ConsoleApp.Configurations.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


//Microsoft.Extensions.Configuration
var config = new ConfigurationBuilder()
    //Microsoft.Extensions.Configuration.FileExtensions
    //Microsoft.Extensions.Configuration.Ini
    .AddIniFile("Configurations/config.ini", optional: true)
    //Microsoft.Extensions.Configuration.Json
    .AddJsonFile("Configurations/config.json", optional: false, reloadOnChange: true)
    //Microsoft.Extensions.Configuration.Xml
    .AddXmlFile("Configurations/config.xml")
    //NetEscapades.Configuration.Yaml
    .AddYamlFile("Configurations/config.yaml")
    //Microsoft.Extensions.Configuration.EnvironmentVariables
    .AddEnvironmentVariables()
    .Build();


//obiekt do konfiguracji wstrzykiwanie zależności
var serviceCollection = new ServiceCollection();

//wskazujemy konkretny obiekt, który ma zostać dostarczony pod danym interfejsem
serviceCollection.AddSingleton<IConfiguration>(config);

//rejestracja typu DebugOutputService
//serviceCollection.AddTransient<DebugOutputService>();

//rejestracja typu IOutputService, pod którym kryje się DebugOutputService i ConsoleOutputService
serviceCollection.AddTransient<IOutputService, DebugOutputService>();
serviceCollection.AddTransient<IOutputService, ConsoleOutputService>();
//ręczna konfiguracja tworzenia obiektu
//serviceCollection.AddTransient<IOutputService, ConsoleOutputService>(x => new ConsoleOutputService(x.GetServices<IFontService>()));
//serviceCollection.AddTransient<IOutputService>(x => new ConsoleOutputService(new SubzeroFontService()));


//transient - zawsze nowa instancja
serviceCollection.AddTransient<IFontService, StandardFontService>();
//singleton - zawsze ta sama instancja
serviceCollection.AddSingleton<IFontService, SubzeroFontService>();
//scope - nowa instancja dla każdego utworzonego scope
serviceCollection.AddScoped<IFontService, ArrowsFontService>();


//zbudowanie dostawcy usług
var serviceProvider  = serviceCollection.BuildServiceProvider();



var text = "ala ma kota";


IOutputService outputService = new ConsoleOutputService();
outputService.ShowText(text);

outputService = new DebugOutputService();
outputService.ShowText(text);

//pobranie pojedynczej usługi - jeśli wiele usług pod tym samym interfejsem, powoduje wybranie tej ostatnio zarejestrowanej
var service = serviceProvider.GetService<IOutputService>()!;
service.ShowText(text);


//utworzenie ręcznie scope
using (var scope = serviceProvider.CreateScope())
{
    foreach (var item in scope.ServiceProvider.GetServices<IOutputService>())
    {
        item.ShowText(text);
    }
    foreach (var item in scope.ServiceProvider.GetServices<IOutputService>())
    {
        item.ShowText(text);
    }
}

using (var scope = serviceProvider.CreateScope())
{
    foreach (var item in scope.ServiceProvider.GetServices<IOutputService>())
    {
        item.ShowText(text);
    }
}

Console.WriteLine(serviceProvider.GetService<IConfiguration>()["TMP"]);


void ConfigurationDemo()
{

    //for (int i = 0; i < int.Parse(config["Count"]!); i++)
    //binder pozwala nam pobierać wartości z konfiguracji o wskazanym typie (nie tylko string)
    for (int i = 0; i < config.GetValue<int>("Count"); i++)
    {
        Console.WriteLine($"Hello {config["HelloJson"]}!");
        Console.WriteLine($"Hello {config["HelloIni"]}!");
        Console.WriteLine($"Hello {config["HelloXml"]}!");
        Console.WriteLine($"Hello {config["HelloYaml"]}!");

        Console.ReadLine();
    }

    Console.WriteLine($"{config["Greetings:Value"]} from {config["Greetings:Targets:From"]} to {config["Greetings:Targets:To"]}");

    var greetingsSection = config.GetSection("Greetings");
    Console.WriteLine($"{greetingsSection["Value"]} from {greetingsSection["Targets:From"]} to {greetingsSection["Targets:To"]}");

    //var targetsSection = config.GetSection("Greetings:Targets");
    var targetsSection = greetingsSection.GetSection("Targets");
    Console.WriteLine($"{greetingsSection["Value"]} from {targetsSection["From"]} to {targetsSection["To"]}");


    //Microsoft.Extensions.Configuration.Binder

    //var greetings = new Greetings();

    //uzupełniamy obiekt na podstawie konfiguracji
    //config.GetSection(nameof(Greetings)).Bind(greetings);

    //wytwarzamy obiekt na podstawie konfiguracji
    var greetings = config.GetSection(nameof(Greetings)).Get<Greetings>();

    Console.WriteLine($"{greetings!.Value} from {greetings.Targets!.From} to {greetings.Targets.To}");


    Console.WriteLine(config["TMP"]);
    Console.WriteLine(config["OneDrive"]);
    Console.WriteLine(config["ala"]);
}


void Introduction()
{

    //namespace ConsoleApp
    //{
    //    internal class Program
    //    {
    //        static void Main(string[] args)
    //        {


    Console.WriteLine("Hello, World!");

    JsonConvert.SerializeObject(new object());

    //Nullable<int> a = null;
    int? a = null;
    string b = "a";

    Console.WriteLine(b);
    b = ToUpper(b)!; //! - wyłączenie ostrzeżenia o potencjalnym null

    //<Nullable>enable</Nullable> - włączenie tej opcji powoduje, że wszystkie typy traktowane są jako wartościowe i powinniśmy jawnie określać, że mogą przyjąć wartość null
    string? ToUpper(string? str /*!! - null-checking feature - dodaje podczas kompilacji (niejawnie) kod wyjątku jak poniżej */)
    {
        //if (a == null)
        //    throw new ArgumentNullException(nameof(str));

        return str?.ToUpper();
    }



    //       }
    //    }
    //}
}