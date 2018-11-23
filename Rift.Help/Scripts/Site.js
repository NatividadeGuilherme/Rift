$(document).ready(function () {
    $(".mascara-telefones").each(function () {
        $(this).inputmask("(99)9999-9999");
    });
    $(".mascara-celular").each(function () {
        $(this).inputmask("(99)99999-9999");
    });
    $(".mascara-cpf").each(function () {
        $(this).inputmask("999.999.999-99");
    });
    $(".mascara-cnpj").each(function () {
        $(this).inputmask("99.999.999/9999-99")
    })
    $(".mascara-cep").each(function () {
        $(this).inputmask("99999-999")
    })

});
