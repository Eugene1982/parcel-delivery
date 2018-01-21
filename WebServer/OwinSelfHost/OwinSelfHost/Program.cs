﻿using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using Raven.Client.Documents.Session;


namespace OwinSelfHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

                // code here
                // Start OWIN host 
                using (WebApp.Start<Startup>(url: baseAddress))
                {
                    // Create HttpCient and make a request to api/values 
                    HttpClient client = new HttpClient();

                    var response = client.GetAsync(baseAddress + "api/departments").Result;

                    Console.WriteLine(response);
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    Console.ReadLine();
                }
            
        }
    }
}