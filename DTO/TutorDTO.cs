namespace PetTravelInsurance.DTO
{
    public class TutorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public ICollection<ContratoDTO>? Contratos { get; set; } = new List<ContratoDTO>();
    }
}