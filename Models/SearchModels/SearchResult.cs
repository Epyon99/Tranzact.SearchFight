namespace Tranzact.SearchFight.Models.SearchModels
{
    public class SearchResult
    {
        public long Total { get; set; }
        public string SearchEngine { get; set; }
        public string Query { get; set; }

        public override string ToString()
        {
            return $"Search Engine: {SearchEngine} | Query {Query} | Total {Total}";
        }
    }
}
