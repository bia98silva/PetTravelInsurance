
using System.Text.Json.Serialization;

namespace PetTravelInsurance.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int TutorId { get; set; }
        public int PetId { get; set; }
        public int PlanoPetId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public string PlanoNome { get; set; } = string.Empty;
        public decimal PlanoPreco { get; set; }
        public string PlanoCobertura { get; set; } = string.Empty;

        public Tutor? Tutor { get; set; }
        public Pet? Pet { get; set; }
        public PlanoPet? PlanoPet { get; set; }
    }
}
