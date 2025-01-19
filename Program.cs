// See https://aka.ms/new-console-template for more information
using GitHubUserActivity;
using System.Net.Http.Json;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hello, World!");

if (args.Length == 0)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("username empty");
    return;
}
else if (args.Length > 1)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Only one user name");
    return;
}

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("Awesome-Octocat-App");
try
{
    var response = await client.GetAsync($"https://api.github.com/users/{args[0]}/events");

    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("This username doesnt exist");
        return;
    }
    var events = await response.Content.ReadFromJsonAsync<IEnumerable<GitHubUserActivityClass>>();

    Console.ForegroundColor = ConsoleColor.DarkYellow;

    Console.WriteLine($"Events of user: {args[0]}");

    Console.ForegroundColor = ConsoleColor.DarkGreen;

    foreach (var activity in events)
    {
        Console.WriteLine($"Event: {activity.type}");
    }
    Console.ForegroundColor = ConsoleColor.White;
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(e.Message);
}



