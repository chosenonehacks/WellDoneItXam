using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using WellDoneIt.Model;

namespace WellDoneIt.Services
{
    public class AzureDataService : IAzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<WellDoneItTask> wellDoneItTable;

        public async Task Initialize()
        {
            //Create our client
            MobileService = new MobileServiceClient("http://welldoneit.azurewebsites.net");

            const string path = "syncstore.db";

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<WellDoneItTask>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            wellDoneItTable = MobileService.GetSyncTable<WellDoneItTask>();
        }

        public async Task<IEnumerable> GetWellDoneItTasks()
        {
            await SyncWellDoneItTasks();
            return await wellDoneItTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task AddWellDoneItTask()
        {
            //create and insert task
            var wellDoneItTask = new WellDoneItTask
            {
                DateUtc = DateTime.UtcNow,
                Title = "Test title",
                Complete = false
            };

            await wellDoneItTable.InsertAsync(wellDoneItTask);

            //Synchronize tasks
            await SyncWellDoneItTasks();
        }

        public async Task SyncWellDoneItTasks()
        {
            //pull down all latest changes and then push current tasks up
            await wellDoneItTable.PullAsync("allWellDoneItTasks", wellDoneItTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
    }
}
