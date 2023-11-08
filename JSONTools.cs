using System.Text.Json;
using System.IO;

namespace Stock
{
    class JSONTools
    {
        public static List<Product> ReadJSON()
        {
            string JSON_stock = File.ReadAllText(@"./data.json");
            var stock = new List<Product>();
            var OPTIONS = new JsonSerializerOptions { IncludeFields = true };
            stock = JsonSerializer.Deserialize<List<Product>>(JSON_stock, OPTIONS);
            return stock;
        }

        public static void WriteJSON(List<Product> newStock)
        {
            try
            {
                var OPTIONS = new JsonSerializerOptions
                {
                    IncludeFields = true,
                    WriteIndented = true
                };
                string JSON_stock = JsonSerializer.Serialize(newStock, OPTIONS);
                File.WriteAllText(@"./data.json", JSON_stock);
            }
            catch
            {
                Console.WriteLine("Algum erro ocorreu.");
            }
        }
    }
}