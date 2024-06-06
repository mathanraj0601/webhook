namespace ClimateWebhookAgent.Model
{
    public class PriceChangeDto
    {
        public string? WebhookType { get; set; }
        public string? Publisher { get; set; }
        public string? WebhookUrl { get; set; }
        public string? Secret { get; set; }
        public double OldTemp { get; set; }
        public double NewTemp { get; set; }
        public string? Area { get; set; }

    }
}
