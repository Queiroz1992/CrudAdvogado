using System;
using System.Data;

namespace Repositorio.Implementacao
{
    public abstract class MySqlRepositorio
    {
        protected IDbConnection AbrirConexao()
        {
            return null;
        }

        protected void FecharConexao(IDbConnection pObjConexao)
        {
            if (pObjConexao != null && pObjConexao.State == ConnectionState.Open)
            {
                pObjConexao.Close();
            }
        }

        protected IDbCommand IniciarComando(IDbConnection pObjConexao, bool pBlnUsarTransacao = false)
        {
            var comando = pObjConexao?.CreateCommand();

            if (pBlnUsarTransacao && pObjConexao != null)
            {
                comando.Transaction = pObjConexao.BeginTransaction();
            }

            return comando;
        }

        protected void ExecutarTransacao(Action<IDbCommand> pObjAcao)
        {
            var conexao = AbrirConexao();

            using (var comando = IniciarComando(conexao, pBlnUsarTransacao: true))
            {
                try
                {
                    pObjAcao(comando);

                    comando.Transaction?.Commit();
                }
                catch (Exception)
                {
                    comando.Transaction?.Rollback();
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
