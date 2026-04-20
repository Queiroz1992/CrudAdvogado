using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IAdvogadoRepositorio
    {
        IEnumerable<Advogado> ListarAdvogados();
        Advogado ObterAdvogado(int pIntId);
        void IncluirAdvogado(Advogado pObjAdvogado);
        void AtualizarAdvogado(Advogado pObjAdvogado);
        void ExcluirAdvogado(int pIntId);
        bool ExcluirAdvogadoComLog(int pIntId, string pStrNomeUsuario);
    }
}
