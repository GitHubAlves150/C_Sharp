using System.Collections.Generic;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Table
{
    internal class PlaceTable
    {
        public static List<PlaceModel> Places()
        {
            return new List<PlaceModel>
            {
                //SP Sala 1
                new PlaceModel {
                                IdLocation = 25,
                                City = "São Paulo",
                                Area = "Paraíso",
                                Address = "R. Sampaio Viana, 202 – Sala 51",
                                CEP = "04004-000"},

                //FL Sala 1
                new PlaceModel {
                                IdLocation = 24,
                                City = "Santa Catarina",
                                Area = "João Paulo",
                                Address = "Rod. José Carlos Daux, 600 – KM 01 Sala G1 0.2",
                                CEP = "88030-000"},
            };
        }
    }
}