using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace BeersLibrary
{
    public class BeerLocalDatabase
    {
        readonly SQLiteAsyncConnection database;
        public string dbPath;

        public BeerLocalDatabase(string dbPath)
        {
            this.dbPath = dbPath;
            database = new SQLiteAsyncConnection(dbPath); 
        }

        public void CreateDatabase()
        {
            database.CreateTableAsync<Beer>().Wait();
        }

        public Task<List<Beer>> GetItemsAsync()
        {
            var bla = database.Table<Beer>().ToListAsync();
            return bla;
        }

        public async Task<bool> CheckIfBeerIsInLocalDatabase(string name)
        {
            Beer beer = await database.Table<Beer>().Where(i => i.Name == name).FirstOrDefaultAsync();
            return beer != null;
        }

        public async Task<int> SaveItemAsync(Beer beer)
        {
            return await database.InsertAsync(beer);
        }

        public Task<int> DeleteItemAsync(Beer beer)
        {
            return database.DeleteAsync(beer);
        }
    }
}
