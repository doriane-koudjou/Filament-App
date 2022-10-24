using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FilamentManagement.Models
{
    public class Machine
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SpoolCount { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Owner { get; set; }
        public double FilamentDiameter { get; set; }
        public double MaxNozzleTemperature { get; set; }
        public double MaxHeatbedTemperature { get; set; }
        

        

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Brand: {Brand}, Model: {Model}, Owner: {Owner}, Description: {Description}, SpoolCount: {SpoolCount}" +
                $"FilamentDiameter: {FilamentDiameter}, MaxNozzleTemperature: {MaxNozzleTemperature}, MaxHeatbedTemperature: {MaxHeatbedTemperature}";
        }
    }
}
