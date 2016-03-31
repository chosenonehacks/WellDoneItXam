using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using WellDoneIt.Model;

namespace WellDoneIt.Services
{
    public class WellDoneItMobileService : IWellDoneItMobileService
    {
        
        private MobileServiceCollection<WellDoneItTask, WellDoneItTask> _items;
        private IMobileServiceTable<WellDoneItTask> _wellDoneItTaskTable;
        private IMobileServiceSyncTable<WellDoneItTask> _wellDoneItTaskSyncTable;

        public MobileServiceClient MobileService { get; set; }

        bool _isInitialized;

        public async Task Initialize()
        {
            if (_isInitialized)
                return;
            //Create our client
            MobileService = new MobileServiceClient("http://welldoneit.azurewebsites.net");


            //const string path = "syncstore.db";

            //setup our local sqlite store and intialize our table
            //var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable<WellDoneItTask>();
            //await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            //_wellDoneItTaskSyncTable = MobileService.GetSyncTable<WellDoneItTask>();

            _wellDoneItTaskTable = MobileService.GetTable<WellDoneItTask>();

            _isInitialized = true;
        }

        
        public async Task<IEnumerable<WellDoneItTask>> GetWellDoneItTasks()
        {
            await Initialize();
            
            return await _wellDoneItTaskTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
            //return await _wellDoneItTaskSyncTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
            
        }

        public async Task AddWellDoneItTask()
        {
            var wellDoneItTask = new WellDoneItTask
            {
                DateUtc = DateTime.UtcNow,
                Title = "Test title",
                Complete = false
            };

            await _wellDoneItTaskTable.InsertAsync(wellDoneItTask);
            //await _wellDoneItTaskSyncTable.InsertAsync(wellDoneItTask);

        }
    }
}