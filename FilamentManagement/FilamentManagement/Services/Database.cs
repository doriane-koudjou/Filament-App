using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using FilamentManagement.Models;
using System.Threading.Tasks;

namespace FilamentManagement.Services
{
    public class Database 
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Filament>();
            _database.CreateTableAsync<Machine>();
            _database.CreateTableAsync<Record>();

        }

        //Filament functions
        public Task<List<Filament>> GetFilamentsAsync()
        {
            return _database.QueryAsync<Filament>("SELECT * FROM [Filament] ORDER BY [Name] ASC");
        }
        public Task<Filament> GetFilamentByIdAsync(int id)
        {
            return _database.FindWithQueryAsync<Filament>($"SELECT * FROM [Filament] WHERE [Id] = {id}");
        }
        public List<Filament> GetUnusedFilaments()
        {
            List<Filament> unusedFilaments = new List<Filament>();
            GetFilamentsAsync().Result.ForEach(filament =>
            {
                Record usageRecord = _database.FindWithQueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Filament] = {filament.Id} AND UnloadingDate IS NULL").Result;
                if (usageRecord == null)
                {
                    unusedFilaments.Add(filament);
                }
            });
                return unusedFilaments;
        }
        public List<Filament> GetEquipedFilaments(Machine machine)
        {
            List<Filament> equipedFilaments = new List<Filament>();
            List<Record> activeRecords = _database.QueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Machine] = {machine.Id} AND UnloadingDate IS NULL").Result;
            activeRecords.ForEach(record =>
            {
                Filament equipedFilament = _database.FindWithQueryAsync<Filament>($"SELECT * FROM [Filament] WHERE [Id] = {record.FK_Filament}").Result;
                equipedFilaments.Add(equipedFilament);
            });
            return equipedFilaments;
        }
        public Task<List<Filament>> SearchFilamentsByNameAsync(string searchInput)
        {
            return _database.QueryAsync<Filament>($"SELECT * FROM [Filament] WHERE [Name] LIKE '%{searchInput}%' ORDER BY [Name] ASC");
        }
        public Task<int> SaveFilamentAsync(Filament filament)
        {
            return _database.InsertAsync(filament);
        }
        public Task<int> UpdateFilamentAsync(Filament filament)
        {
            return _database.UpdateAsync(filament);
        }
        public Task<int> DeleteFilamentAsync(Filament filament)
        {
            List<Record> affectedRecords = _database.QueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Filament] = {filament.Id}").Result;
            affectedRecords.ForEach(record =>
            {
                record.FK_Filament = null;
                UpdateRecordAsync(record);
            });
            return _database.DeleteAsync(filament);
        }

        //Machine functions
        public Task<List<Machine>> GetMachinesAsync()
        {
            return _database.QueryAsync<Machine>("SELECT * FROM [Machine] ORDER BY [Name] ASC");
        }
        public Task<Machine> GetMachineByIdAsync(int id)
        {
            return _database.FindWithQueryAsync<Machine>($"SELECT * FROM [Machine] WHERE [Id] = {id}");
        }
        public Task<Machine> GetMachineByFilamentUsageAsync(Filament filament)
        {
            Record record = _database.FindWithQueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Filament] = {filament.Id}  AND  [UnloadingDate] IS NULL ").Result;
            if(record == null)
            {
                return Task.FromResult<Machine>(null);
            }
            else
            {
                return _database.FindWithQueryAsync<Machine>($"SELECT * FROM [Machine] WHERE [Id] = {record.FK_Machine}");
            }
        }
        public Task<List<Machine>> SearchMachinesByNameAsync(string searchInput)
        {
            return _database.QueryAsync<Machine>($"SELECT * FROM [Machine] WHERE [Name] LIKE '%{searchInput}%' ORDER BY [Name] ASC");
        }
        public Task<int> SaveMachineAsync(Machine machine)
        {
            return _database.InsertAsync(machine);
        }
        public Task<int> UpdateMachineAsync(Machine machine)
        {
            return _database.UpdateAsync(machine);
        }
        public Task<int> DeleteMachineAsync(Machine machine)
        {
            List<Record> dependingRecords = _database.QueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Machine] = {machine.Id}").Result;
            dependingRecords.ForEach(record => DeleteRecordAsync(record));
            return _database.DeleteAsync(machine);
        }

        //Record functions
        public Task<List<Record>> GetRecordsAsync()
        {
            return _database.QueryAsync<Record>("SELECT * FROM [Record] ORDER BY [Id] ASC");
        }
        public Task<List<Record>> GetRecordsByMachineAsync(Machine machine)
        {
            return _database.QueryAsync<Record>($"SELECT * FROM [Record] WHERE [FK_Machine] = {machine.Id} ORDER BY [UnloadingDate] ASC");
        }
        public Task<int> SaveRecordAsync(Record record)
        {
            return _database.InsertAsync(record);
        }
        public Task<int> UpdateRecordAsync(Record record)
        {
            return _database.UpdateAsync(record);
        }
        public Task<int> DeleteRecordAsync(Record record)
        {
            return _database.DeleteAsync(record);
        }

    }
}
