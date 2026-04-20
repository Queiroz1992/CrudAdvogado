using System;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    [Serializable]
    public class AdvogadoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a Senioridade")]
        public int? Senioridade { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Estado")]
        public int? Estado { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Número")]
        [Range(1, int.MaxValue, ErrorMessage = "O Número deve ser maior que zero")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Complemento")]
        public string Complemento { get; set; }
    }
}
