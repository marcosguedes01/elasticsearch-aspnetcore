using Nest;
using System;

namespace ElasticsearchExample
{
    class Program
    {
        public static Uri node { get; set; }
        public static ConnectionSettings settings { get; set; }
        public static ElasticClient client { get; set; }

        static void Main(string[] args)
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node, defaultIndex: "my_blog");
            client = new ElasticClient(settings);

            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            client.CreateIndex(c => c
                .Index("my_blog")
                .InitializeUsing(indexSettings)
                .AddMapping<Post>(m => m.MapFromAttributes()));
        }
    }
}
