namespace Сonfectionery.Services.Kafka.Configurations
{
    public class KSqlDbConfig
    {
        public static string KSqlDbConfiguration = "KSqlDB";

        public string BaseUrl { get; set; }
        public string Subscription { get; set; }
        public bool ShouldPluralizeFromItemName { get; set; }
    }
}