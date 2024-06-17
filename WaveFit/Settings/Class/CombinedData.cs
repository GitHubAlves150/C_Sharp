using System;

namespace WaveFit2.Settings.Class
{
    public class CombinedData
    {
        // HealthCenter properties
        public int Id { get; set; }

        public string Name { get; set; }
        public string Nickname { get; set; }
        public byte[] Logo { get; set; }
        public string CNPJ { get; set; }
        public string Telephone { get; set; }
        public int IdPlace { get; set; }
        public int IdAudiometer { get; set; }

        // Audiometer properties
        public string Code { get; set; }

        public string Model { get; set; }
        public DateTime Maintenance { get; set; }

        // Location properties
        public string State { get; set; }

        public string Acronym { get; set; }

        // Place properties
        public string City { get; set; }

        public string Area { get; set; }
        public string Address { get; set; }
        public string CEP { get; set; }
        public int IdLocation { get; set; }
    }
}