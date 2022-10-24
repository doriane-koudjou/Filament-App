using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FilamentManagement.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime LoadingDate { get; set; }
        public DateTime? UnloadingDate { get; set; }
        public int FK_Machine { get; set; }
        public int? FK_Filament { get; set; }

        public override string ToString()
        {
            var temp = UnloadingDate == null ? "null" : UnloadingDate.ToString();
            return $"Id: {Id}, DateTime: {LoadingDate}, UnloadingDate: {temp}, FK_Machine: {FK_Machine}, FK_Filament: {FK_Filament}";
        }
    }
}
