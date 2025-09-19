using System.Text.Json.Serialization;

namespace PetTravelInsurance.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public string Idade { get; set; } = string.Empty;
        public int TutorId { get; set; }     

     
        public Tutor? Tutor { get; set; }    
        [JsonIgnore]
        public ICollection<Contrato>? Contratos { get; set; } = new List<Contrato>();
    }
}
