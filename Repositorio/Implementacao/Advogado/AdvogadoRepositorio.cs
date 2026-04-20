using System.Collections.Generic;
using System;
using System.Linq;
using Dominio;
using Repositorio.Interface;

namespace Repositorio.Implementacao
{
    public class AdvogadoRepositorio : MySqlRepositorio, IAdvogadoRepositorio
    {
        private static List<Advogado> _advogados = new List<Advogado>();
        private static List<string> _logs = new List<string>();
        private static int _proximoId = 1;

        public IEnumerable<Advogado> ListarAdvogados()
        {
            return _advogados;
        }

        public Advogado ObterAdvogado(int pIntId)
        {
            return _advogados.FirstOrDefault(a => a.Id == pIntId);
        }

        public void IncluirAdvogado(Advogado pObjAdvogado)
        {
            pObjAdvogado.Id = _proximoId++;
            _advogados.Add(pObjAdvogado);
        }

        public void AtualizarAdvogado(Advogado pObjAdvogado)
        {
            var existente = _advogados.FirstOrDefault(a => a.Id == pObjAdvogado.Id);
            if (existente != null)
            {
                existente.Nome = pObjAdvogado.Nome;
                existente.Senioridade = pObjAdvogado.Senioridade;
                existente.Logradouro = pObjAdvogado.Logradouro;
                existente.Bairro = pObjAdvogado.Bairro;
                existente.Estado = pObjAdvogado.Estado;
                existente.Cep = pObjAdvogado.Cep;
                existente.Numero = pObjAdvogado.Numero;
                existente.Complemento = pObjAdvogado.Complemento;
            }
        }

        public void ExcluirAdvogado(int pIntId)
        {
            var advogado = _advogados.FirstOrDefault(a => a.Id == pIntId);
            if (advogado != null)
            {
                _advogados.Remove(advogado);
            }
        }

        public bool ExcluirAdvogadoComLog(int pIntId, string pStrNomeUsuario)
        {
            var conexao = AbrirConexao();

            using (var comando = IniciarComando(conexao, pBlnUsarTransacao: true))
            {
                try
                {
                    var advogado = _advogados.FirstOrDefault(a => a.Id == pIntId);
                    if (advogado == null)
                        return false;

                    _advogados.Remove(advogado);

                    _logs.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Usuário {pStrNomeUsuario} excluiu o advogado {advogado.Nome} (ID: {pIntId})");

                    comando?.Transaction?.Commit();

                    return true;
                }
                catch (Exception)
                {
                    comando?.Transaction?.Rollback();
                    throw;
                }
                finally
                {
                    FecharConexao(conexao);
                }
            }
        }
    }
}
