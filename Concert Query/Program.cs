// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Concert
{
    public int Id { get; set; }
    public bool ReducedVenue { get; set; }
    public DateTime Date { get; set; }
    public string Performer { get; set; }
    public int BeginsAt { get; set; }
    public int FullCapacitySales { get; set; }
}
class Program
{
    static void Main()
    {

        string currentDirectory = Directory.GetCurrentDirectory();

        string jsonFilePath = Path.Combine(currentDirectory, "concert_data.json");

        string concertData = File.ReadAllText(jsonFilePath);
        List<Concert> concerts = JsonConvert.DeserializeObject<List<Concert>>(concertData);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Concert Query Menu:");
            Console.WriteLine();
            Console.WriteLine("1. Display concerts ordered by Date from the present date");
            Console.WriteLine("2. Display concerts at Reduced Venue");
            Console.WriteLine("3. Display concerts during 2024");
            Console.WriteLine("4. Display five concerts with the biggest Full Capacity Sales");
            Console.WriteLine("5. Display concerts taking place on a Friday");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-6): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":

                    DateTime currentDate = DateTime.Now;
                    List<Concert> orderedByDate = concerts.Where(c => c.Date >= currentDate).OrderBy(c => c.Date).ToList();
                    DisplayConcerts("Concerts ordered by Date from the present date", orderedByDate);
                    break;
                case "2":

                    List<Concert> reducedVenueConcerts = concerts.Where(c => c.ReducedVenue).ToList();
                    DisplayConcerts("Concerts at Reduced Venue", reducedVenueConcerts);
                    break;
                case "3":

                    List<Concert> concertsIn2024 = concerts.Where(c => c.Date.Year == 2024).ToList();
                    DisplayConcerts("Concerts during 2024", concertsIn2024);
                    break;
                case "4":

                    List<Concert> topFiveSales = concerts.OrderByDescending(c => c.FullCapacitySales).Take(5).ToList();
                    DisplayConcerts("Five concerts with the biggest Full Capacity Sales", topFiveSales);
                    break;
                case "5":

                    List<Concert> fridayConcerts = concerts.Where(c => c.Date.DayOfWeek == DayOfWeek.Friday).ToList();
                    DisplayConcerts("Concerts taking place on a Friday", fridayConcerts);
                    break;
                case "6":

                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
    static void DisplayConcerts(string header, List<Concert> concertList)
    {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(new string('-', 50));
        foreach (var concert in concertList)
        {
            Console.WriteLine($"ID: {concert.Id}");
            Console.WriteLine($"Date: {concert.Date.ToShortDateString()}");
            Console.WriteLine($"Performer: {concert.Performer}");
            Console.WriteLine($"Begins At: {concert.BeginsAt} minutes past midnight");
            Console.WriteLine($"Full Capacity Sales: {concert.FullCapacitySales}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
        }
    }
}
