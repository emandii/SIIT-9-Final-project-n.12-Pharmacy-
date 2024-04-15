using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pharmacy_first_setup
{
    public class PharmacyOps
    {
        private static Dictionary<string, List<Product>> data;

        public static void LoadData()
        {
            try
            {
                string json = File.ReadAllText("data.json");
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Culture = CultureInfo.InvariantCulture
                };
                data = JsonConvert.DeserializeObject<Dictionary<string, List<Product>>>(json, settings);
            }
            catch (FileNotFoundException)
            {
                data = new Dictionary<string, List<Product>>();
            }
        }

        public static void SaveData()
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Culture = CultureInfo.InvariantCulture,
                FloatFormatHandling = FloatFormatHandling.String
            };

            string json = JsonConvert.SerializeObject(data, settings);
            File.WriteAllText("data.json", json);
        }

        public static void DisplayMenu()
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

        public static bool ValidateOption(string option)
        {
            return option.Length == 1 && "1234567".Contains(option);
        }
        

        public static void SellProducts()
        {
            Console.Clear();

            if (data.Values.Any(category => category.Any(product => product.Quantity > 0)))
            {
                Console.WriteLine("Select the Product you want to sell:");
                foreach (var category in data)
                {
                    foreach (var product in category.Value)
                    {
                        if (product.Quantity > 0)
                        {
                            Console.WriteLine($"{product.Name} - {product.Quantity} units - Price: {product.Price.ToString("N2", CultureInfo.InvariantCulture)} RON");
                        }
                    }
                }

                string productName;
                int quantity;

                do
                {
                    Console.Write("Product Name: ");
                    productName = Console.ReadLine().ToLower();

                    if (!data.Values.Any(category => category.Any(product => product.Name.ToLower() == productName)))
                    {
                        Console.WriteLine("Incorrect product; please select a product from the list.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    break;
                } while (true);

                do
                {
                    Console.Write("Number of units sold: ");
                    if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid number of units. Please enter a valid positive integer.");
                    }
                } while (quantity <= 0);

                bool productExists = false;

                foreach (var category in data)
                {
                    foreach (var product in category.Value)
                    {
                        if (product.Name.ToLower() == productName)
                        {
                            productExists = true;
                            if (product.Quantity >= quantity)
                            {
                                Console.Clear();
                                product.Quantity -= quantity;
                                decimal totalPrice = quantity * product.Price;
                                {
                                    Console.WriteLine
                                        (@"

        ██╗██╗██╗    ███╗   ███╗ █████╗ ██╗  ██╗███████╗    ██╗████████╗    ██████╗  █████╗ ██╗███╗   ██╗    ██╗██╗██╗
        ██║██║██║    ████╗ ████║██╔══██╗██║ ██╔╝██╔════╝    ██║╚══██╔══╝    ██╔══██╗██╔══██╗██║████╗  ██║    ██║██║██║
        ██║██║██║    ██╔████╔██║███████║█████╔╝ █████╗      ██║   ██║       ██████╔╝███████║██║██╔██╗ ██║    ██║██║██║
        ╚═╝╚═╝╚═╝    ██║╚██╔╝██║██╔══██║██╔═██╗ ██╔══╝      ██║   ██║       ██╔══██╗██╔══██║██║██║╚██╗██║    ╚═╝╚═╝╚═╝
        ██╗██╗██╗    ██║ ╚═╝ ██║██║  ██║██║  ██╗███████╗    ██║   ██║       ██║  ██║██║  ██║██║██║ ╚████║    ██╗██╗██╗
        ╚═╝╚═╝╚═╝    ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝    ╚═╝   ╚═╝       ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝    ╚═╝╚═╝╚═╝

                                       ");
                                }
                                Console.WriteLine($"You earned {totalPrice:N2} RON.");

                                if (product.Quantity == 0)
                                {
                                    category.Value.Remove(product);
                                    Console.WriteLine($"Product {product.Name} has been deleted from stock because the total amount of remaining units reached zero.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Quantity of selected product is insufficient.");
                            }

                            Console.WriteLine("Press any key to return to Main Menu...");
                            Console.ReadKey();
                            Console.Clear();
                            return;
                        }
                    }
                }

                if (!productExists)
                {
                    Console.WriteLine("Selected product does not exist.");
                }

                Console.WriteLine("Press any key to return to Main Menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("No products available");
                Console.WriteLine("Press any key to return to Main Menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

public static void RemoveExpiredProducts()
        {
            Console.Clear ();
            while (true)
            {
                
                Console.Write(" Expired products will be removed from stock. Do you wish to proceed? (y/n): ");
                string confirmation = Console.ReadLine().ToLower();

                if (confirmation == "y")
                {
                    if (data.Values.Any(category => category.Any(product => product.ExpirationDate.HasValue && product.ExpirationDate.Value < DateTime.Today)))
                    {
                        List<Product> removedProducts = new List<Product>();

                        foreach (var category in data)
                        {
                            foreach (var product in category.Value)
                            {
                                if (product.ExpirationDate.HasValue && product.ExpirationDate.Value < DateTime.Today)
                                {
                                    removedProducts.Add(product);
                                }
                            }

                            foreach (var product in removedProducts)
                            {
                                category.Value.Remove(product);
                            }
                        }

                        Console.WriteLine(" List of removed products:");
                        foreach (var product in removedProducts)
                        {
                            Console.WriteLine($" {product.Name} - {product.Quantity} units");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" No products in stock");
                    }

                    break;
                }
                else if (confirmation == "n")
                {
                    Console.Clear();
                    Console.WriteLine(" Operation has been canceled.");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" Invalid choice; please select \"Y\" or \"N\"!");
                }
            }

            Console.WriteLine(" Press any key to return to Main Menu...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void DisplayStock()
        {
            Console.Clear();
            Console.WriteLine(" Products in stock:");

            foreach (var category in data)
            {
                Console.WriteLine($" {category.Key}:" + $"{category.Value.Count}");
                foreach (var product in category.Value)
                {
                    if (product.ExpirationDate.HasValue)
                    {
                        int daysUntilExpiration = (product.ExpirationDate.Value - DateTime.Today).Days;
                        Console.WriteLine($" {product.Name} - {product.Quantity} units - Expires în {daysUntilExpiration} days - Price: {product.Price.ToString("F2", CultureInfo.InvariantCulture)} RON");
                    }
                    else
                    {
                        Console.WriteLine($" {product.Name} - {product.Quantity} units - Price: {product.Price:F2} lei");
                    }
                }
            }

            Console.WriteLine(" Press any key to return to Main Menu ...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void AddNewProduct()
        {
            string category;
            do
            {
                Console.Clear();
                Console.WriteLine(" What kind of product do you want to add?:");
                Console.WriteLine(" 1) medicines");
                Console.WriteLine(" 2) supplements");
                Console.WriteLine(" 3) paramedical products");
                Console.WriteLine(" 0) back to Main Menu");
                category = Console.ReadLine();

                if (category == "0")
                    return;

                if (category != "1" && category != "2" && category != "3")
                {

                    Console.WriteLine(" Selected category is not valid. Please select a valid category from the list! ");
                    Console.WriteLine(" Press any key to continue...");
                    Console.ReadKey();
                }
            } while (category != "1" && category != "2" && category != "3");

            string categoryName = "";
            if (category == "1")
                categoryName = "medicines";
            else if (category == "2")
                categoryName = "supplements";
            else if (category == "3")
                categoryName = "paramedical products";

            Console.WriteLine($" Current items in {categoryName}:");
            if (data.ContainsKey(categoryName))
            {
                foreach (Product product in data[categoryName])
                {
                    Console.WriteLine($" {product.Name} - {product.Quantity} units - Price: {product.Price.ToString("F2", CultureInfo.InvariantCulture)} RON");
                }
            }

            Console.Write(" Insert Product name: ");

            string selectedProduct = Console.ReadLine();

            if (data.ContainsKey(categoryName) && data[categoryName].Any(p => p.Name == selectedProduct))
            {
                Console.Clear();
                Console.WriteLine(" Selected product already exists; to update product quantity select \"Update products stock\" from below list.");
                return;
            }
            else
            {
                int quantity;
                bool validQuantity;
                do
                {
                    Console.Write(" Insert product quantity: ");
                    validQuantity = int.TryParse(Console.ReadLine(), out quantity) && quantity > 0;
                    if (!validQuantity)
                    {
                        Console.WriteLine(" Invalid quantity. Please insert a valid quantity.");
                    }
                } while (!validQuantity);

                decimal price;
                bool validPrice;
                do
                {
                    Console.WriteLine($" Insert price per unit:");
                    string input = Console.ReadLine();

                    validPrice = decimal.TryParse(input.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out price) && price > 0;

                    if (!validPrice)
                    {
                        Console.WriteLine("Invalid price; please insert a valid price:");
                    }
                }
                while (!validPrice);

                DateTime? expirationDate = null;
                if (category == "1" || category == "2")
                {
                    DateTime date;
                    bool validDate;
                    do
                    {
                        Console.Write(" Insert expiration date (YYYY-MM-DD): ");
                        validDate = DateTime.TryParse(Console.ReadLine(), out date);
                        if (!validDate)
                        {
                            Console.WriteLine(" Expiration date is not valid; please insert date (YYYY-MM-DD):");
                        }
                    } while (!validDate);
                    expirationDate = date;
                }

                if (data.ContainsKey(categoryName))
                {
                    data[categoryName].Add(new Product(selectedProduct, quantity, price, expirationDate));
                }


                Console.Clear();
                Console.WriteLine
                   (@"

                                            █████                     █████                     █████     █████             █████
                                            ░░███                     ░░███                     ░░███     ░░███             ░░███ 
             ████████  ████████  ██████   ███████ █████ ████  ██████  ███████       ██████    ███████   ███████   ██████  ███████ 
             ░███░░███░░███░░██████░░███ ███░░███░░███ ░███  ███░░███░░░███░       ░░░░░███  ███░░███  ███░░███  ███░░██████░░███ 
             ░███ ░███ ░███ ░░░░███ ░███░███ ░███ ░███ ░███ ░███ ░░░   ░███         ███████ ░███ ░███ ░███ ░███ ░███████░███ ░███ 
             ░███ ░███ ░███    ░███ ░███░███ ░███ ░███ ░███ ░███  ███  ░███ ███    ███░░███ ░███ ░███ ░███ ░███ ░███░░░ ░███ ░███ 
             ░███████  █████   ░░██████ ░░████████░░████████░░██████   ░░█████    ░░████████░░████████░░████████░░██████░░████████
             ░███░░░  ░░░░░     ░░░░░░   ░░░░░░░░  ░░░░░░░░  ░░░░░░     ░░░░░      ░░░░░░░░  ░░░░░░░░  ░░░░░░░░  ░░░░░░  ░░░░░░░░ 
             ░███  
             █████  
            ░░░░░ 

                  ");

            }

            Console.WriteLine(" Press any key to return to Main Menu...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void LoadProducts()
        {
            string category;

            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("You want to update:");
                Console.WriteLine("1) medicines");
                Console.WriteLine("2) supplements");
                Console.WriteLine("3) paramedical products");
                Console.WriteLine("0) back to main menu");
                category = Console.ReadLine();

                if (category == "0")
                {
                    Console.Clear();
                    return;
                }

                if (category != "1" && category != "2" && category != "3")
                {
                    Console.WriteLine("Wrong selection; please select an option from the list");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (category != "1" && category != "2" && category != "3");

            string categoryName = "";
            if (category == "1")
                categoryName = "medicines";
            else if (category == "2")
                categoryName = "supplements";
            else if (category == "3")
                categoryName = "paramedical products";

            if (data.ContainsKey(categoryName) && data[categoryName].Count > 0)
            {
                Console.Clear();

                Console.WriteLine($"Products stocked in {categoryName}:");

                foreach (Product prod in data[categoryName])
                {
                    Console.WriteLine($" {prod.Name} - {prod.Quantity} units - Price per unit: {prod.Price.ToString("N2", CultureInfo.InvariantCulture)} RON");
                }

                Console.Write("Name of the product you want to update: ");
                string productName = Console.ReadLine();

                bool productFound = false;

                foreach (Product prod in data[categoryName])
                {
                    if (string.Equals(prod.Name, productName, StringComparison.OrdinalIgnoreCase))
                    {


                        int quantity;

                        do
                        {
                            Console.WriteLine($"Insert {prod.Name} quantity added:");
                            string input = Console.ReadLine();

                            if (!int.TryParse(input, out quantity) || quantity <= 0)
                            {
                                Console.Clear();
                                Console.WriteLine
                                    (@"

                                       ░░░░░▄▀░░░░░░░░░░░░░▀▀▄▄░░░░░ 
                                       ░░░▄▀░░░░░░░░░░░░░░░░░░░▀▄░░░ 
                                       ░░▄▀░░░░░░░░░░░░░░░░░░░░░░█░░ 
                                       ░█░░░░░░░░░░░░░░░░░░░░░░░░░█░ 
                                       ▐░░░░░░░░░░░░░░░░░░░░░░░░░░░█ 
                                       █░░░░▀▀▄▄▄▄░░░▄▌░░░░░░░░░░░░▐ 
                                       ▌░░░░░▌░░▀▀█▀▀░░░▄▄░░░░░░░▌░▐ 
                                       ▌░░░░░░▀▀▀▀░░░░░░▌░▀██▄▄▄▀░░▐ 
                                       ▌░░░░░░░░░░░░░░░░░▀▄▄▄▄▀░░░▄▌ 
                                       ▐░░░░▐░░░░░░░░░░░░░░░░░░░░▄▀░ 
                                       ░█░░░▌░░▌▀▀▀▄▄▄▄░░░░░░░░░▄▀░░ 
                                       ░░█░░▀░░░░░░░░░░▀▌░░▌░░░█░░░░ 
                                       ░░░▀▄░░░░░░░░░░░░░▄▀░░▄▀░░░░░ 
                                       ░░░░░▀▄▄▄░░░░░░░░░▄▄▀▀░░░░░░░ 
                                       ░░░░░░░░▐▌▀▀▀▀▀▀▀▀░░░░░░░░░░░ 
                                       ░░░░░░░░█░░░░░░░░░░░░░░░░░░░░                                       
                                      
                                      "
                                    );
                                Console.WriteLine($" Only positive integers allowed; please insert a valid number of {prod.Name} units to be added");
                            }
                        }
                        while (quantity <= 0);

                        prod.Quantity += quantity;

                        Console.Clear();
                        Console.WriteLine
                         (@"

                           ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                           ██░▄▄░██░██░█░▄▄▀██░▀██░█▄▄░▄▄█▄░▄█▄▄░▄▄██░███░████░██░██░▄▄░██░▄▄▀█░▄▄▀█▄▄░▄▄██░▄▄▄██░▄▄▀█
                           ██░██░██░██░█░▀▀░██░█░█░███░████░████░████▄▀▀▀▄████░██░██░▀▀░██░██░█░▀▀░███░████░▄▄▄██░██░█
                           ██▄▄░▀██▄▀▀▄█░██░██░██▄░███░███▀░▀███░██████░██████▄▀▀▄██░█████░▀▀░█░██░███░████░▀▀▀██░▀▀░█
                           ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀

                            ");

                        Console.WriteLine($" Quantity updated, new quantity of {prod.Name} = {prod.Quantity}");
                        Console.WriteLine
                        (@"

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                       ");

                        decimal price;
                        bool validPrice;
                        do
                        {
                            Console.WriteLine($"Insert new price per unit for {prod.Name}:");
                            string input = Console.ReadLine();

                            validPrice = decimal.TryParse(input.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out price) && price > 0;

                            if (!validPrice)
                            {
                                Console.WriteLine("Invalid price; please insert a valid price:");
                            }
                        }
                        while (!validPrice);

                        prod.Price = price;

                        Console.Clear();
                        Console.WriteLine($"Price updated; new price for {prod.Name} is {prod.Price.ToString("N2", CultureInfo.InvariantCulture)} RON");

                        productFound = true;
                        break;
                    }
                }

                if (!productFound)
                {
                    Console.Clear();
                    Console.WriteLine
                            (@"

                          ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                          █▀▄▄▀█ ▄▄▀█▀▄▄▀█ ▄▀█ ██ █▀▄▀█▄ ▄███ ▄▄▀█▀▄▄▀█▄ ▄███ ▄▄█▀▄▄▀█ ██ █ ▄▄▀█ ▄▀█
                          █ ▀▀ █ ▀▀▄█ ██ █ █ █ ██ █ █▀██ ████ ██ █ ██ ██ ████ ▄██ ██ █ ██ █ ██ █ █ █
                          █ ████▄█▄▄██▄▄██▄▄███▄▄▄██▄███▄████▄██▄██▄▄███▄████▄████▄▄███▄▄▄█▄██▄█▄▄██
                          ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀                           

                         "
                            );
                        Console.WriteLine(" This product does not exist yet; please select \"Add new product\" or choose a different option from the below list");
                        Console.WriteLine
                            (@"

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                           ");
                        return;
                    
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine
                    (@"

                   ^ ^           
                  (O,O)          
                  (   ) Empty    
                 -""-""-----------
                 

                    ");
                Console.WriteLine(" Stock is empty; please select \"Add new product\" or choose a different option from the below list");
            }

            Console.WriteLine(" Press any key to go back to Main Menu...");
            Console.ReadKey();
            Console.Clear();
        }


        public static void RemoveExistingProduct()
        { 
            string category;
            do
            {
                Console.Clear();
                Console.WriteLine("What kind of product do you want to remove?:");
                Console.WriteLine("1) medicines");
                Console.WriteLine("2) supplements");
                Console.WriteLine("3) paramedical products");
                Console.WriteLine("0) back to Main Menu");
                category = Console.ReadLine();

                if (category == "0")
                    return;

                if (category != "1" && category != "2" && category != "3")
                {
                    Console.WriteLine("Invalid category selection. Please select a valid category from the list!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            while (category != "1" && category != "2" && category != "3");

            string categoryName = "";
            if (category == "1")
                categoryName = "medicines";
            else if (category == "2")
                categoryName = "supplements";
            else if (category == "3")
                categoryName = "paramedical products";

            Console.WriteLine($"Current items in {categoryName}:");

            if (!data.ContainsKey(categoryName) || data[categoryName].Count == 0)
            {
                Console.WriteLine("Selected category does not contain products.");

                string selection;
                do
                {
                    Console.WriteLine("Do you want to select a different category from the list? (y/n):");
                    selection = Console.ReadLine().ToLower();

                    if (selection == "n")
                    {
                        Console.WriteLine("Press any key to return to Main Menu...");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    else if (selection == "y")
                    {
                        RemoveExistingProduct();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please select yes or no (y/n).");
                    }
                }
                while (selection != "y" && selection != "n");
            }

            foreach (Product product in data[categoryName])
            {
                Console.WriteLine($" {product.Name} - {product.Quantity} units - Price: {product.Price.ToString("F2", CultureInfo.InvariantCulture)} RON");
            }

            bool productFound = false;

            while (!productFound)
            {
                Console.Write("Insert product name: ");
                string productName = Console.ReadLine();

                if (!data.ContainsKey(categoryName))
                {
                    Console.WriteLine("Selected category does not exist.");
                    Console.WriteLine("Press any key to return to Main Menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                bool productExists = false;
                foreach (Product product in data[categoryName])
                {
                    if (product.Name.ToLower() == productName.ToLower())
                    {
                        productExists = true;

                        int quantity;
                        do
                        {
                            Console.WriteLine($"Insert quantity of {product.Name} to be removed:");
                            string input = Console.ReadLine();

                            if (!int.TryParse(input, out quantity) || quantity <= 0 || quantity > product.Quantity)
                            {
                                Console.WriteLine("Inserted product quantity is not valid; please select a valid quantity to be removed:");
                            }
                        }
                        while (quantity <= 0 || quantity > product.Quantity);

                        product.Quantity -= quantity;

                        if (product.Quantity == 0)
                        {
                            data[categoryName].Remove(product);
                            Console.WriteLine($"Product {product.Name} has been deleted from {categoryName} as quantity reached zero.");
                        }
                        else
                        {
                            Console.WriteLine($"Quantity has been updated, new quantity of {product.Name} = {product.Quantity}");
                        }

                        productFound = true;
                        break;
                    }
                }

                if (!productExists)
                {
                    Console.WriteLine("Selected product does not exist.");

                    string selection;
                    do
                    {
                        Console.WriteLine("Do you want to select a different product or go back to Main Menu? (P/M):");
                        selection = Console.ReadLine().ToLower();

                        if (selection == "m")
                        {
                            Console.WriteLine("Press any key to return to Main Menu...");
                            Console.ReadKey();
                            Console.Clear();
                            return;
                        }
                        else if (selection == "p")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Please select product (P) or Main Menu (M).");
                        }
                    }
                    while (selection != "p" && selection != "m");
                }
            }

            Console.WriteLine("Press any key to return to Main Menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
