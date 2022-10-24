using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace FilamentManagement.Models
{
    public class Filament
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string Batch { get; set; }
        public string Producer { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double NozzleTemperature { get; set; }
        public double NozzleTemperatureTolerance { get; set; }
        public double HeatbedTemperature { get; set; }
        public double HeatbedTemperatureTolerance { get; set; }
        public double Diameter { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Material: {Material}, Color: {Color}, Batch: {Batch}, Producer: {Producer}, Weight: {Weight}, Lenght: {Length}" +
                $"\n Diameter: {Diameter}, NozzleTemperature: {NozzleTemperature}+-{NozzleTemperatureTolerance}, HeatbedTemperature: {HeatbedTemperature}+-{NozzleTemperatureTolerance}";
        }
    }
}
