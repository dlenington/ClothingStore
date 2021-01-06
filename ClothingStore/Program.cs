using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
using GraphQL.SystemTextJson;


namespace ClothingStore
{

    public static class StarWarsDB
    {
        public static IEnumerable<Jedi> GetJedis()
        {
            return new List<Jedi>() {
            new Jedi() { Name = "Luke", Side="Light"},
            new Jedi() { Name = "Yoda", Side = "Light"},
            new Jedi() { Name = "Darth Vader", Side = "Dark"},
                };
        }
        
    }

    public class Jedi
    {
        public String Name { get; set; }
        public String Side { get; set; }

        public Jedi()
        {

        }
    }

    public class Query
    {
        [GraphQLMetadata("jedis")]
        public IEnumerable<Jedi> GetJedis()
        {
            return StarWarsDB.GetJedis();
        }

        [GraphQLMetadata("hello")]
        public string GetHello()
        {
            return "Hello Query class";
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World");
            var schema = Schema.For(@"
                type Jedi {
                    name: String,
                    side: String
                }

                type Query {
                    hello: String,
                    jedis: [Jedi]
                }
            ", _ =>
            {
                _.Types.Include<Query>();
            });

            var writer = new GraphQL.SystemTextJson.DocumentWriter();
            var root = new { Hello = "Hello World!" };
            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = "{ jedis {name, side} }";
                _.Root = root;
            });

            Console.WriteLine(json);
            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
