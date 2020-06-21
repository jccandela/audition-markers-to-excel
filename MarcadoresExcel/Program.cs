using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MarcadoresExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            string directorio = Directory.GetCurrentDirectory();
          
            System.Console.WriteLine(directorio);

            TextReader reader = null;

            try{
                reader = new StreamReader("Marcadores.csv");
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.Delimiter = "\t";
                config.HasHeaderRecord = true;

                using (var csv = new CsvReader(reader, config))
                {
                    var records = new List<Marcador>();
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = new Marcador
                        {                          
                            Name = csv.GetField("Name"),
                            Start = csv.GetField("Start"),
                            Duration = csv.GetField("Duration"),
                            TimeFormat = csv.GetField("Time Format"),
                            Type = csv.GetField("Type"),
                            Description = csv.GetField("Description")
                        };
                        records.Add(record);
                    }

                    var nuevosMarcadores = new List<Marcador2>();

                    foreach (Marcador marcador in records)
                    {
                        var nuevoMarcador = new Marcador2
                        {
                            Name = marcador.Name,
                            Start = marcador.Start,
                            Duration = marcador.Duration,
                            TimeFormat = marcador.TimeFormat,
                            Type = marcador.Type,
                            Description = marcador.Description,
                            Minute = marcador.Start.Split(':')[0],
                            Second = marcador.Start.Split(':')[1].Split('.')[0],
                            Millisecond = marcador.Start.Split('.')[1]
                        };
                        nuevosMarcadores.Add(nuevoMarcador);
                    }


                    CsvConfiguration config2 = new CsvConfiguration(CultureInfo.InvariantCulture);
                    config2.Delimiter = ";";

                    using (var writer = new StreamWriter("MarcadoresTransformacion.csv"))
                    using (var nuevoCSV = new CsvWriter(writer, config2))
                    {
                        nuevoCSV.WriteRecords(nuevosMarcadores);
                    }
                    
                System.Console.WriteLine("Transformación finalizada");

                }

                
            }
            catch (System.IO.FileNotFoundException)
            {
                System.Console.WriteLine("Tiene que colocar el archivo 'Marcadores.csv' en la carpeta");
                System.Console.WriteLine(directorio);
            }
            catch (System.IO.IOException)
            {
                System.Console.WriteLine("El archivo 'Marcadores.csv' está siendo utilizado por otro proceso, párelo");
            }

            System.Console.WriteLine("Pulse cualquier tecla + INTRO para finalizar ");
            System.Console.ReadLine();
        }
    }
}
