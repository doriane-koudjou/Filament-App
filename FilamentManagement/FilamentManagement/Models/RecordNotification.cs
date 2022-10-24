using System;
using System.Collections.Generic;
using System.Text;

namespace FilamentManagement.Models
{
    public class RecordNotification
    {
        public int Id { get; set; }
        public string type { get; set; }
        public DateTime LoadingDate { get; set; }
        public DateTime? UnloadingDate { get; set; }
        public Machine Machine { get; set; }
        public Filament Filament { get; set; }

        public RecordNotification(Record record, Machine machine, Filament filament)
        {
            Id = record.Id;
            LoadingDate = record.LoadingDate;
            UnloadingDate = record.UnloadingDate;
            type = record.UnloadingDate.HasValue ? "uneqip" : "equip";
            Machine = machine;
            Filament = filament;
        }
    }
}
