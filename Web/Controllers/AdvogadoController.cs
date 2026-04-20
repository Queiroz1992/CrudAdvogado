using System;
using System.Linq;
using System.Web.Mvc;
using Dominio;
using Repositorio.Implementacao;
using Repositorio.Interface;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AdvogadoController : Controller
    {
        IAdvogadoRepositorio AdvogadoRepositorio;

        public AdvogadoController()
        {
            AdvogadoRepositorio = new AdvogadoRepositorio();
        }

        public ActionResult Index()
        {
            var advogados = AdvogadoRepositorio.ListarAdvogados();

            var viewModel = advogados.Select(a => new AdvogadoListaViewModel
            {
                Id = a.Id,
                Nome = a.Nome,
                SenioridadeId = (int)a.Senioridade,
                Senioridade = a.Senioridade == Dominio.Senioridade.Junior ? "Júnior"
                            : a.Senioridade == Dominio.Senioridade.Pleno ? "Pleno"
                            : "Sênior",
                Logradouro = a.Logradouro,
                Bairro = a.Bairro,
                Estado = a.Estado.ToString(),
                Cep = a.Cep,
                Numero = a.Numero,
                Complemento = a.Complemento
            }).ToList();

            return View(viewModel);
        }

        public ActionResult Cadastro(int? pIntId, [Bind(Prefix = "id")] int? pIntIdLegado)
        {
            var viewModel = new AdvogadoViewModel();
            pIntId = pIntId ?? pIntIdLegado;

            if (pIntId.HasValue)
            {
                var advogado = AdvogadoRepositorio.ObterAdvogado(pIntId.Value);
                if (advogado != null)
                {
                    viewModel.Id = advogado.Id;
                    viewModel.Nome = advogado.Nome;
                    viewModel.Senioridade = (int)advogado.Senioridade;
                    viewModel.Logradouro = advogado.Logradouro;
                    viewModel.Bairro = advogado.Bairro;
                    viewModel.Estado = (int)advogado.Estado;
                    viewModel.Cep = advogado.Cep;
                    viewModel.Numero = advogado.Numero;
                    viewModel.Complemento = advogado.Complemento;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult IncluirAdvogado(AdvogadoViewModel pObjViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { sucesso = false, mensagem = string.Join(", ", erros) });
                }

                var advogado = new Advogado
                {
                    Nome = pObjViewModel.Nome,
                    Senioridade = (Senioridade)pObjViewModel.Senioridade,
                    Logradouro = pObjViewModel.Logradouro,
                    Bairro = pObjViewModel.Bairro,
                    Estado = (Estado)pObjViewModel.Estado,
                    Cep = pObjViewModel.Cep,
                    Numero = pObjViewModel.Numero.Value,
                    Complemento = pObjViewModel.Complemento
                };

                AdvogadoRepositorio.IncluirAdvogado(advogado);

                return Json(new { sucesso = true, mensagem = "Advogado cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AtualizarAdvogado(AdvogadoViewModel pObjViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { sucesso = false, mensagem = string.Join(", ", erros) });
                }

                var advogado = new Advogado
                {
                    Id = pObjViewModel.Id,
                    Nome = pObjViewModel.Nome,
                    Senioridade = (Senioridade)pObjViewModel.Senioridade,
                    Logradouro = pObjViewModel.Logradouro,
                    Bairro = pObjViewModel.Bairro,
                    Estado = (Estado)pObjViewModel.Estado,
                    Cep = pObjViewModel.Cep,
                    Numero = pObjViewModel.Numero.Value,
                    Complemento = pObjViewModel.Complemento
                };

                AdvogadoRepositorio.AtualizarAdvogado(advogado);

                return Json(new { sucesso = true, mensagem = "Advogado atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ExcluirAdvogado(int? pIntId, [Bind(Prefix = "id")] int? pIntIdLegado)
        {
            try
            {
                pIntId = pIntId ?? pIntIdLegado;
                if (!pIntId.HasValue)
                {
                    return Json(new { sucesso = false, mensagem = "Id do advogado não informado." });
                }

                AdvogadoRepositorio.ExcluirAdvogado(pIntId.Value);
                return Json(new { sucesso = true, mensagem = "Advogado excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}
