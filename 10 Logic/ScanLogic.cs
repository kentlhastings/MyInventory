using System.Collections.Generic;
using System.Threading.Tasks;
using MyInventory.Models;

namespace MyInventory.Logic
{
    public class ScanLogic
    {
        public async Task<List<ScanResult>> GetScanResultsAsync()
        {
            var json = GetDummyScanResults();
            var results = await GetDummyScanResults();
            return results ?? new List<ScanResult>();
        }

        public async Task<List<ScanResult>> GetDummyScanResults()
        {
            await Task.Delay(1000);
            return new List<ScanResult>
            {
                new ScanResult
                {
                    Ticker = "BMGL",
                    Price = 4.52,
                    PercentChange = 12.34,
                    RelativeVolume = 6.78,
                    Float = 15.2,
                    Bid = 4.5,
                    Ask = 4.55
                },
                new ScanResult
                {
                    Ticker = "HCVI",
                    Price = 3.21,
                    PercentChange = 15.67,
                    RelativeVolume = 5.43,
                    Float = 18.7,
                    Bid = 3.19,
                    Ask = 3.25
                }
            };
        }
    }
}
