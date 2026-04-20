using Dominio;
using Dominio;
using Repositorio.Implementacao;

namespace Web.App_Start
{
    public static class AdvogadoSeed
    {
        public static void Inicializar()
        {
            var repositorio = new AdvogadoRepositorio();

            repositorio.IncluirAdvogado(new Advogado
            {
                Nome = "Carlos Eduardo Mendonça",
                Senioridade = Senioridade.Senior,
                Logradouro = "Avenida Paulista",
                Bairro = "Bela Vista",
                Estado = Estado.SP,
                Cep = "01310-100",
                Numero = 1000,
                Complemento = "Conjunto 52"
            });

            repositorio.IncluirAdvogado(new Advogado
            {
                Nome = "Fernanda Lopes Carvalho",
                Senioridade = Senioridade.Pleno,
                Logradouro = "Rua das Flores",
                Bairro = "Centro",
                Estado = Estado.RJ,
                Cep = "20040-020",
                Numero = 305,
                Complemento = "Sala 10"
            });

            repositorio.IncluirAdvogado(new Advogado
            {
                Nome = "Rafael Augusto Teixeira",
                Senioridade = Senioridade.Junior,
                Logradouro = "Rua Sete de Setembro",
                Bairro = "Centro Cívico",
                Estado = Estado.PR,
                Cep = "80230-010",
                Numero = 88,
                Complemento = "Apto 201"
            });
        }
    }
}
