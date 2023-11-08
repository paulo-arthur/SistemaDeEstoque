using System;

namespace Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                List<Product> stock = JSONTools.ReadJSON();
                PrintProductList(stock);
                PrintMainMenu();
                stock = ReadAndProcessOption(stock);
                JSONTools.WriteJSON(stock);
            }
        }

        static void PrintProductList(List<Product> stock)
        {
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            foreach (var product in stock)
            {
                Console.Write("Código: ");
                Console.WriteLine(product.code);
                Console.Write("Nome: ");
                Console.WriteLine(product.name);
                Console.Write("Preço de venda: ");
                Console.WriteLine(product.outcomePrice);
                Console.Write("Qtd. em estoque: ");
                Console.WriteLine(product.amount);
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            }
        }

        static List<Product> ReadAndProcessOption(List<Product> stock)
        {
            try
            {
                Console.Write("> ");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        return AddProducts(stock);
                        break;
                    case "2":
                        return RegisterNewProduct(stock);
                        break;
                    case "3":
                        return NewSale(stock);
                        break;
                    case "4":
                        Environment.Exit(0);
                        return stock;
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        ReadAndProcessOption(stock);
                        return stock;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Opção inválida");
                ReadAndProcessOption(stock);
                return stock;
            }
        }
        static void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Sistema de Estoque ===");
            Console.WriteLine("Selecione a opção desejada.");
            Console.WriteLine("[1] - Adicionar carregamento");
            Console.WriteLine("[2] - Cadastrar novo produto");
            Console.WriteLine("[3] - Realizar venda");
            Console.WriteLine("[4] - Fechar sistema");
            Console.WriteLine();
        }

        static List<Product> AddProducts(List<Product> stock)
        {
            try
            {
                Console.Write("Código do produto: ");
                string code = Console.ReadLine();
                int index = stock.FindIndex(product => product.code == code);
                Console.Write("Quantidade: ");
                double qtt = Convert.ToDouble(Console.ReadLine());
                stock[index].amount += qtt;
                return stock;
            }
            catch
            {
                AddProducts(stock);
            }
            return stock;
        }

        static List<Product> RegisterNewProduct(List<Product> stock)
        {
            Console.Write("Nome do produto: ");
            string product_name = Console.ReadLine();
            Console.Write("Preço de venda: ");
            double product_outcome_price = Convert.ToDouble(Console.ReadLine());
            Product new_product = new Product();
            new_product.name = product_name;
            new_product.outcomePrice = product_outcome_price;
            new_product.amount = 0.0;
            new_product.code = RandomNewCode();
            stock.Add(new_product);
            return stock;
        }

        static List<Product> NewSale(List<Product> stock)
        {
            List<InvoiceItem> invoice = new List<InvoiceItem>();

            while (true)
            {
                double salePrice = 0.0;
                Console.Clear();
                PrintProductList(stock);
                Console.WriteLine();

                Console.WriteLine("-=-=-=-=-= NOTA -=-=-=-=-=-=-");
                foreach (var item in invoice)
                {
                    Console.WriteLine($"{item.code} - {item.name} X{item.saleAmount} (R${item.standartOutcomePrice} un.) - R${item.outcomePrice}");
                    salePrice += item.outcomePrice;
                }
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine("TOTAL: R$" + salePrice);

                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Digite o código do produto, f para fechar a nota ou s para sair.");
                    Console.Write("> ");
                    string op = Console.ReadLine();

                    switch (op)
                    {
                        case "f":
                            salePrice = 0.0;
                            Console.Clear();
                            Console.WriteLine("-=-=-=-=-= NOTA -=-=-=-=-=-=-");
                            foreach (var item in invoice)
                            {
                                Console.WriteLine($"{item.code} - {item.name} X{item.saleAmount} (R${item.standartOutcomePrice} un.) - R${item.outcomePrice}");
                                salePrice += item.outcomePrice;
                            }
                            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                            Console.WriteLine("TOTAL: R$" + salePrice);
                            Console.ReadLine();
                            return stock;
                            break;
                        case "s":
                            return stock;
                            break;
                        default:
                            try
                            {
                                string code = op;
                                int index = stock.FindIndex(product => product.code == code);
                                Console.Write("Quantidade: ");
                                double saleAmount = Convert.ToDouble(Console.ReadLine());
                                InvoiceItem item = new InvoiceItem();
                                item.code = code;
                                item.name = stock[index].name;
                                item.saleAmount = saleAmount;
                                item.standartOutcomePrice = stock[index].outcomePrice;
                                item.outcomePrice = saleAmount * item.standartOutcomePrice;
                                invoice.Add(item);
                                stock[index].amount -= item.saleAmount;
                            }
                            catch
                            {
                                Console.WriteLine("Código de produto inválido");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Código de produto inválido.");
                }
                Console.Clear();
            }
        }

        static string RandomNewCode()
        {
            string numbers = "1234567890";
            var random = new Random();
            char[] code = new char[4];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = numbers[random.Next(numbers.Length)];
            }

            return new String(code);
        }
    }
}