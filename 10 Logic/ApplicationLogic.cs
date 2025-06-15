using System.Threading.Tasks;
using MyInventory.Models;

namespace MyInventory.Logic
{
    public class ApplicationLogic
    {
        public async Task<Data> LoadData()
        {
            return new Data();
        }

        public async Task<bool> SaveData(Data data)
        {
            return true;
        }
    }
}
