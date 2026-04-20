function AdvogadoViewModel_AoCarregarComponente() {
    AdvogadoViewModel_FormatarCampos();
    AdvogadoViewModel_Confirmar();
}

function AdvogadoViewModel_FormatarCampos() {
    $('.cep').mask('00000-000');

    $('.numero').on('keypress', function (e) {
        if (e.which < 48 || e.which > 57) {
            e.preventDefault();
        }
    });

    $('.numero').on('paste', function (e) {
        e.preventDefault();
    });

    $('.cep').on('blur', function () {
        var cep = $(this).val().replace(/\D/g, '');
        if (cep.length === 8) {
            AdvogadoViewModel_ConsultarCep(cep);
        }
    });
}

function AdvogadoViewModel_ConsultarCep(cep) {
    $('#cepLoading').show();
    $.ajax({
        url: 'https://viacep.com.br/ws/' + cep + '/json/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#cepLoading').hide();
            if (!data.erro) {
                $('#Logradouro').val(data.logradouro);
                $('#Bairro').val(data.bairro);
                var estadoOption = $('#Estado option').filter(function () {
                    return $(this).text() === data.uf;
                });
                if (estadoOption.length > 0) {
                    $('#Estado').val(estadoOption.val());
                }
                $('#Numero').focus();
            } else {
                AdvogadoViewModel_ExibirToast('CEP não encontrado.', 'erro');
            }
        },
        error: function () {
            $('#cepLoading').hide();
            AdvogadoViewModel_ExibirToast('Erro ao consultar o CEP.', 'erro');
        }
    });
}

function AdvogadoViewModel_Confirmar() {
    $('#formAdvogado').on('submit', function (e) {
        e.preventDefault();
        var form = $(this);
        if (!form.valid()) { return; }
        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (result) {
                if (result.sucesso) {
                    AdvogadoViewModel_ExibirToast(result.mensagem, 'sucesso');
                    setTimeout(function () {
                        window.location.href = AdvogadoViewModel_UrlIndex;
                    }, 1500);
                } else {
                    AdvogadoViewModel_ExibirToast(result.mensagem, 'erro');
                }
            }
        });
    });
}

function AdvogadoViewModel_ExibirToast(mensagem, tipo) {
    var toast = $('#toastCadastro');
    toast.removeClass('sucesso erro').addClass(tipo).text(mensagem).fadeIn(300);
    setTimeout(function () { toast.fadeOut(400); }, 3000);
}
