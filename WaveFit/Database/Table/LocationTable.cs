using System.Collections.Generic;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Table
{
    internal class LocationTable
    {
        public static List<LocationModel> Locations()
        {
            return new List<LocationModel>
            {
                new LocationModel {State = "Acre",
                                   Acronym = "AC"},

                new LocationModel {State = "Alagoas",
                                   Acronym = "AL"},

                new LocationModel {State = "Amapá",
                                   Acronym = "AP"},

                new LocationModel {State = "Amazonas",
                                   Acronym = "AM"},

                new LocationModel {State = "Bahia",
                                   Acronym = "BA"},

                new LocationModel {State = "Ceará",
                                   Acronym = "CE"},

                new LocationModel {State = "Distrito Federal",
                                   Acronym = "DF"},

                new LocationModel {State = "Espírito Santo",
                                   Acronym = "ES"},

                new LocationModel {State = "Goiás",
                                   Acronym = "GO"},

                new LocationModel {State = "Maranhão",
                                   Acronym = "MA"},

                new LocationModel {State = "Mato Grosso",
                                   Acronym = "MT"},

                new LocationModel {State = "Mato Grosso do Sul",
                                   Acronym = "MS"},

                new LocationModel {State = "Minas Gerais",
                                   Acronym = "MG"},

                new LocationModel {State = "Pará",
                                   Acronym = "PA"},

                new LocationModel {State = "Paraíba",
                                   Acronym = "PB"},

                new LocationModel {State = "Paraná",
                                   Acronym = "PR"},

                new LocationModel {State = "Pernambuco",
                                   Acronym = "PE"},

                new LocationModel {State = "Piauí",
                                   Acronym = "PI"},

                new LocationModel {State = "Rio de Janeiro",
                                   Acronym = "RJ"},

                new LocationModel {State = "Rio Grande do Norte",
                                   Acronym = "RN"},

                new LocationModel {State = "Rio Grande do Sul",
                                   Acronym = "RS"},

                new LocationModel {State = "Rondônia",
                                   Acronym = "RO"},

                new LocationModel {State = "Roraima",
                                   Acronym = "RR"},

                new LocationModel {State = "Santa Catarina",
                                   Acronym = "SC"},

                new LocationModel {State = "São Paulo",
                                   Acronym = "SP"},

                new LocationModel {State = "Sergipe",
                                   Acronym = "SE"},

                new LocationModel {State = "Tocantins",
                                   Acronym = "TO"},
            };
        }
    }
}