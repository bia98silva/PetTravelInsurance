using System.Text.Json.Serialization;

namespace PetTravelInsurance.Models
{
    public class PlanoPet
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Cobertura { get; set; } = string.Empty;  
       
        public string Descricao { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        [JsonIgnore]
        public ICollection<Contrato>? Contratos { get; set; } = new List<Contrato>();
    }
}