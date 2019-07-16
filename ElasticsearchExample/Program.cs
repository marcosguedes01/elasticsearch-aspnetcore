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

            //client.CreateIndex(c => c
            //    .Index("my_blog")
            //    .InitializeUsing(indexSettings)
            //    .AddMapping<Post>(m => m.MapFromAttributes()));

            //InsertData();
            PerformTermQuery();
        }

        public static void InsertData()
        {
            var newBlogPost = new Post
            {
                UserId = 1,
                PostDate = DateTime.Now,
                PostText = "This is another blog post."
            };

            var newBlogPost2 = new Post
            {
                UserId = 2,
                PostDate = DateTime.Now,
                PostText = "This is a third blog post."
            };

            var newBlogPost3 = new Post
            {
                UserId = 2,
                PostDate = DateTime.Now.AddDays(5),
                PostText = "This is a blog post from the future."
            };

            client.Index(newBlogPost);
            client.Index(newBlogPost2);
            client.Index(newBlogPost3);
        }

        public static void PerformTermQuery()
        {
            var result =
                client.Search<Post>(s => s
                    .Query(p => p.Term(q => q.PostText, "blog")));
        }
    }
}
