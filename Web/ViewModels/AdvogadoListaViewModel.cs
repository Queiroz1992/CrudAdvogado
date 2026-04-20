using System;

namespace Web.ViewModels
{
    [Serializable]
    public class AdvogadoListaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senioridade { get; set; }
        public int SenioridadeId { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
