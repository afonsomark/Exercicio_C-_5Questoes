namespace QuickType
{
    public partial class Geral
    {
        public long Page { get; set; }
        public long PerPage { get; set; }
        public long Total { get; set; }
        public long TotalPages { get; set; }
        public DataGames[] ?Data { get; set; }
    }
}
