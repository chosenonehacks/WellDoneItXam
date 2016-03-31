using System.Collections;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace WellDoneIt.Services
{
    public interface IAzureDataService
    {
        MobileServiceClient MobileService { get; set; }
        

        Task Initialize();


        Task<IEnumerable> GetWellDoneItTasks();


        Task AddWellDoneItTask();


        Task SyncWellDoneItTasks();

    }
}