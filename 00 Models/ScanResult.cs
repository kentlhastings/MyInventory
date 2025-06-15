namespace MyInventory.Models
{
    public class ScanResult
    {
        public string Ticker { get; set; }
        public double Price { get; set; }
        public double PercentChange { get; set; }
        public double RelativeVolume { get; set; }
        public double Float { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}
