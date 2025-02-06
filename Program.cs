// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
if (resp == "1")
{
    // create data file
    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");
    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    if (!File.Exists("data.txt"))
    {
        Console.WriteLine("Data file not found. Please create data first.");
    }
    else{
        Console.WriteLine("File Found");
    }
    string[] lines = File.ReadAllLines("data.txt");

    foreach (string line in lines)
    {
        // Split the line into date and sleep data
        string[] parts = line.Split(",");
        string weekStartDate = parts[0];
        string[] sleepHours = parts[1].Split("|");

        // Convert sleepHours to integers
        int[] sleepData = Array.ConvertAll(sleepHours, int.Parse);

        // Calculate total and average sleep
        int totalSleep = sleepData.Sum();
        double avgSleep = Math.Round(totalSleep / 7.0, 1);

        // Display the weekly report
        Console.WriteLine($"Week of {DateTime.Parse(weekStartDate):MMM, dd, yyyy}");
        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
        Console.WriteLine(" -- -- -- -- -- -- -- --- ---");

        // Print sleep hours with correct spacing
        Console.WriteLine($" {string.Join(" ", sleepData.Select(h => h.ToString().PadLeft(2)))}  {totalSleep.ToString().PadLeft(3)} {avgSleep.ToString("0.0").PadLeft(4)}");

        Console.WriteLine();
    }
}
