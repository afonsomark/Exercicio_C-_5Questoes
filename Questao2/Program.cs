using Newtonsoft.Json;
using QuickType;
using System.Net;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        long totalGoals = getTotalScoredGoals(teamName, year);
        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);
        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 21 goals in 2013
        // Team Chelsea scored 26 goals in 2014
    }

    static string GetJSON(string team, int year)
    {
        var request = WebRequest.Create("https://jsonmock.hackerrank.com/api/football_matches?year=" + year + "&team1=" + team);
        request.Method = "GET";
        var response = (HttpWebResponse)request.GetResponse();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            using(var stream = response.GetResponseStream()) 
            { 
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                return json; 
            }
        }
        return null;
    }

    public static long getTotalScoredGoals(string team, int year)
    {
        long score = 0;
        var jsonData = GetJSON(team, year);
        Geral apiData = JsonConvert.DeserializeObject<Geral>(jsonData);

        foreach (var item in apiData.Data)
        {
            score += item.Team1Goals;
        }
        return score;
    }
}

