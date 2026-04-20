var AdvogadoListaViewModel_IdParaExcluir = null;
var AdvogadoListaViewModel_IdParaExcluir = null;

function AdvogadoListaViewModel_AoCarregarComponente() {
    AdvogadoListaViewModel_CarregarCampos();
    AdvogadoListaViewModel_CarregarFiltros();
}

function AdvogadoListaViewModel_CarregarCampos() {
    $('.btn-excluir').on('click', function () {
        AdvogadoListaViewModel_IdParaExcluir = $(this).data('id');
        var nome = $(this).data('nome');
        $('#modalNomeAdvogado').text('"' + nome + '"');
        $('#modalExcluir').css('display', 'flex');
    });

    $('#btnCancelarExcluir').on('click', function () {
        AdvogadoListaViewModel_FecharModal();
    });

    $('#modalExcluir').on('click', function (e) {
        if ($(e.target).is('#modalExcluir')) {
            AdvogadoListaViewModel_FecharModal();
        }
    });

    $('#btnConfirmarExcluir').on('click', function () {
        $.ajax({
            url: AdvogadoListaViewModel_UrlExcluir,
            type: 'POST',
            data: { pIntId: AdvogadoListaViewModel_IdParaExcluir },
            success: function (result) {
                AdvogadoListaViewModel_FecharModal();
                if (result.sucesso) {
                    AdvogadoListaViewModel_ExibirToast(result.mensagem, '#2e7d32', '#43a047');
                    setTimeout(function () { location.reload(); }, 1600);
                } else {
                    AdvogadoListaViewModel_ExibirToast(result.mensagem, '#b71c1c', '#e53935');
                }
            }
        });
    });
}

function AdvogadoListaViewModel_FecharModal() {
    $('#modalExcluir').css('display', 'none');
    AdvogadoListaViewModel_IdParaExcluir = null;
}

function AdvogadoListaViewModel_ExibirToast(mensagem, corInicio, corFim) {
    var toast = $('#toastIndex');
    toast.css('background', 'linear-gradient(135deg,' + corInicio + ',' + corFim + ')')
         .text(mensagem).fadeIn(300);
    setTimeout(function () { toast.fadeOut(400); }, 3000);
}

function AdvogadoListaViewModel_CarregarFiltros() {
    $('#filtroNome, #filtroSenioridade, #filtroEstado').on('keyup change', function () {
        AdvogadoListaViewModel_FiltrarTabela();
    });
}

function AdvogadoListaViewModel_FiltrarTabela() {
    var texto = ($('#filtroNome').val() || '').toLowerCase();
    var senioridade = $('#filtroSenioridade').val();
    var estado = $('#filtroEstado').val();
    var visiveis = 0;

    $('.table-adv tbody tr').each(function () {
        var linha = $(this);
        var conteudo = linha.text().toLowerCase();
        var badgeSenioridade = linha.attr('data-senioridade') || '';
        var estadoLinha = linha.attr('data-estado') || '';

        var matchTexto = !texto || conteudo.indexOf(texto) > -1;
        var matchSenioridade = !senioridade || badgeSenioridade === senioridade;
        var matchEstado = !estado || estadoLinha === estado;

        if (matchTexto && matchSenioridade && matchEstado) {
            linha.show();
            visiveis++;
        } else {
            linha.hide();
        }
    });
}
