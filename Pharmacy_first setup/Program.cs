using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;
using System.Globalization;


namespace Pharmacy_first_setup;

public class Program
{
    public static void Main(string[] args)
    {
        PharmacyOps.LoadData();

        while (true)
        {
            DisplayMenu();
            string option = Console.ReadLine();

            if (!ValidateOption(option))
            {
                Console.Clear();
                Console.WriteLine("Invalid option, please select an option from the list below.");
                continue;
            }

            switch (option)
            {
                case "1":
                    PharmacyOps.LoadProducts();
                    break;
                case "2":
                    PharmacyOps.SellProducts();
                    break;
                case "3":
                    PharmacyOps.RemoveExpiredProducts();
                    break;
                case "4":
                    PharmacyOps.DisplayStock();
                    break;
                case "5":
                    PharmacyOps.AddNewProduct();
                    break;
                case "6":
                    PharmacyOps.RemoveExistingProduct();
                    break;
                case "7":
                    PharmacyOps.SaveData();
                    return;
            }
        }
    }

    static void DisplayMenu()
    {
        {
            Console.WriteLine

            (@"

          ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗    ████████╗ ██████╗     ████████╗██╗  ██╗███████╗    ██████╗ ██╗  ██╗ █████╗ ██████╗ ███╗   ███╗ █████╗  ██████╗██╗   ██╗
          ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝    ╚══██╔══╝██╔═══██╗    ╚══██╔══╝██║  ██║██╔════╝    ██╔══██╗██║  ██║██╔══██╗██╔══██╗████╗ ████║██╔══██╗██╔════╝╚██╗ ██╔╝
          ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗         ██║   ██║   ██║       ██║   ███████║█████╗      ██████╔╝███████║███████║██████╔╝██╔████╔██║███████║██║      ╚████╔╝ 
          ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝         ██║   ██║   ██║       ██║   ██╔══██║██╔══╝      ██╔═══╝ ██╔══██║██╔══██║██╔══██╗██║╚██╔╝██║██╔══██║██║       ╚██╔╝  
          ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗       ██║   ╚██████╔╝       ██║   ██║  ██║███████╗    ██║     ██║  ██║██║  ██║██║  ██║██║ ╚═╝ ██║██║  ██║╚██████╗   ██║   
           ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝       ╚═╝    ╚═════╝        ╚═╝   ╚═╝  ╚═╝╚══════╝    ╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝   ╚═╝   
           
            ");

            Console.WriteLine(" Main Menu:");
            Console.WriteLine();
            Console.WriteLine(" 1) update products stock");
            Console.WriteLine(" 2) Sell products");
            Console.WriteLine(" 3) Remove expired products from stock");
            Console.WriteLine(" 4) Display products in stock");
            Console.WriteLine(" 5) Add new product");
            Console.WriteLine(" 6) Delete existing product");
            Console.WriteLine(" 7) Exit");
        }
    }

    static bool ValidateOption(string option)
    {
        return option.Length == 1 && "1234567".Contains(option);
    }
}












