namespace HPlusSportsAPI.Services
{
    /// <summary>
    /// Options loaded from config at startup
    /// </summary>
    public class CosmosDBServiceOptions
    {
        public string DBUri { get; set; }
        public string DBKey { get; set; }
        public string DBName { get; set; }

        public string DBCollection { get; set; }
    }
}
