using System;
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

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World");
            var schema = Schema.For(@"
                type Jedi {
                    name: String,
                    side: String,
                    id: ID
                }

                type Query {
                    hello: String,
                    jedis: [Jedi],
                    jedi(id: ID): Jedi
                }
                ", _ =>
            {
                _.Types.Include<Query>();
            });

            var writer = new GraphQL.SystemTextJson.DocumentWriter();
            var root = new { Hello = "Hello World!" };
            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = "{ jedi(id: 1) {name} }";
                _.Root = root;
            });

            Console.WriteLine(json);
        }
    }
}
