using System.Diagnostics;


class ControlloDNSRecord
{

    static void Main()
    {
        string[] domini = File.ReadAllLines("domini.txt");
        StreamWriter sw = new StreamWriter("output.txt");
        foreach (string dominio in domini)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {   
                    FileName = "nslookup",
                    Arguments = "-q=all " + dominio,
                    RedirectStandardOutput = true,
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine("Qui di sotto sono rappresentati i DNS Record per " + dominio + ":");
            Console.WriteLine(output);
            if (( output.Contains("ASPMX.L.GOOGLE.COM") | output.Contains("google-site-verification") | output.Contains("spf.google")) &  ((output.Contains("MS=") | output.Contains("outlook.com") | output.Contains("spf.protection.outlook")))  )
            sw.WriteLine(dominio + " utilizza sia Google Workspace che Microsoft365");
            else
            sw.WriteLine(dominio + " non utilizza sia Google Workspace che Microsoft365");
    
        }
        sw.Close();
        Console.ReadKey();
    }
}