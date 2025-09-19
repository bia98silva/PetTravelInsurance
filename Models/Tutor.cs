using System.Text.Json.Serialization;

namespace PetTravelInsurance.Models
{
    public class Tutor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Pet>? Pets { get; set; } = new List<Pet>();
        [JsonIgnore]
        public ICollection<Contrato>? Contratos { get; set; } = new List<Contrato>();
    }
}