using BluePrism.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BluePrism.App
{
    class Program
    {
        private static IDictionaryService _service;

        private static string dictionaryFilePath;
        private static string resultsFilePath;

        static void Main(string[] args)
        {
            LoadSettings();

            var serviceProvider = new ServiceCollection()
                        .AddSingleton<IFileservice, FileService>()
                        .AddSingleton<IDictionaryService, DictionaryService>()
                        .BuildServiceProvider();

            _service = serviceProvider.GetService<IDictionaryService>();

            bool endApp = false;

            while (!endApp)
            {
                Console.WriteLine("Test Project 2019 by Jonathan Smart");
                Console.WriteLine("\r");
                Console.WriteLine("------------------------\n");

                Console.Write("Please Enter Start Word: ");
                string startWordInput = Console.ReadLine().ToLower();

                Console.Write("Please Enter End Word: ");
                string endWordInput = Console.ReadLine().ToLower();

                var validStartWord = _service.Exists(startWordInput, dictionaryFilePath) && startWordInput.Length > 0;
                var validEndWord = _service.Exists(endWordInput, dictionaryFilePath) && endWordInput.Length > 0;

                if (validStartWord && validEndWord)
                {
                    var results = _service.CreateResult(dictionaryFilePath, startWordInput,
                        endWordInput, resultsFilePath);

                    Console.WriteLine("\r");
                    Console.WriteLine($"Results saved to file : {resultsFilePath} ");
                    Console.WriteLine("\r");
                    Console.WriteLine($"File Contents:");

                    foreach (var word in results)
                    {
                        Console.WriteLine($"{word}");
                    }

                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"Start Word Found : {validStartWord} ");
                    Console.WriteLine($"End Word Found: {validEndWord} ");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private static void LoadSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            dictionaryFilePath = AppDomain.CurrentDomain.BaseDirectory + configuration.GetSection("DictionaryFilePath").Value;
            resultsFilePath = AppDomain.CurrentDomain.BaseDirectory + configuration.GetSection("ResultsFilePath").Value;
        }
    }
}