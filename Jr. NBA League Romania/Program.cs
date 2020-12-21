using System;
using System.Collections.Generic;
using System.Configuration;
using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using Jr._NBA_League_Romania.service;
using Jr._NBA_League_Romania.ui;
using Jr._NBA_League_Romania.views;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

namespace Jr._NBA_League_Romania
{
    class Program
    {
     
        static void Main(string[] args)
        {
            ConsoleUI console = new ConsoleUI();

            console.Run();
        }
    }
}
