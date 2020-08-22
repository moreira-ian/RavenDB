using System;
using System.Linq;
using Raven.Client.Documents;

namespace RavenDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connection
            var documentStore = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Cliente"
            };

            documentStore.Initialize();

            //Inset
            using (var session = documentStore.OpenSession())
            {
                var cliente = new Cliente
                {
                    Id = "123e4567-e89b-12d3-a456-426655440000",
                    Nome = "Nome_Cliente",
                    DataDeNascimento = Convert.ToDateTime("01/01/2020")
                };
                session.Store(cliente);
                session.SaveChanges();
            }
            documentStore.Dispose();

            //List
            using (var session = documentStore.OpenSession())
            {
                var clientes = session.Query<Cliente>().Where(c => c.Nome.StartsWith("N"));
                foreach (var cliente in clientes)
                {
                    Console.WriteLine(cliente.Id);
                    Console.WriteLine(cliente.Nome);
                    Console.WriteLine(cliente.DataDeNascimento.Year);
                    Console.WriteLine("/############/");
                   
                }
            }

            Console.WriteLine("Operação concluída");
          
        }
    }
}
