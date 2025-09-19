namespace PetTravelInsurance.DTO
{
    public class ContratoDTO
    {
        public int Id { get; set; }
        public int TutorId { get; set; }
        public int PetId { get; set; }
        public int PlanoPetId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string? PlanoNome { get; set; } = string.Empty;
        public decimal PlanoPreco { get; set; }
        public string? PlanoCobertura { get; set; } = string.Empty;
    }
}