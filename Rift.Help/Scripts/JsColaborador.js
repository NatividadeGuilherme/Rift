var botao = document.getElementById("botaoDesconectar");
botao.addEventListener("Click", Desconectar);
function Desconectar() {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Colaborador/Desconectar",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { },
        success: function (retorno) {
            if (retorno.Resultado == 'desconectando') {
                window.location.href = "/Colaborador/ValidarLogin";
            }
                
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

var labelEmail = document.querySelector("#validarEmail");




function VerificacaoEmail(email) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Colaborador/VerificarEmailCadastrado",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { email: email},
        success: function (retorno) {
            if (retorno.Result == true) {
                labelEmail.textContent = "E-mail indisponível para utilização";
                $('#Email').focus();
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            alert('deu errado');
        }
    });
}
$(document).on('blur', '#Email', function () {
    VerificacaoEmail($(this).val());
});



function validaCPF(cpf) {
    cpf = cpf.replace(".", "");
    cpf = cpf.replace("-", "");
    cpf = cpf.replace(".", "");
    var numeros, digitos, soma, i, resultado, digitos_iguais;
    digitos_iguais = 1;
    if (cpf.length < 11)
        return false;
    for (i = 0; i < cpf.length - 1; i++)
        if (cpf.charAt(i) != cpf.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        numeros = cpf.substring(0, 9);
        digitos = cpf.substring(9);
        soma = 0;
        for (i = 10; i > 1; i--)
            soma += numeros.charAt(10 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        numeros = cpf.substring(0, 10);
        soma = 0;
        for (i = 11; i > 1; i--)
            soma += numeros.charAt(11 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

var labelCPF = document.querySelector("#validarCpforCnpj");
var cpf = document.querySelector("#Cpf");
$(document).on('blur', '#Cpf', function () {
    var result =validaCPF($(this).val());
    if (result == false) {
        labelCPF.textContent = "CPF Inválido";
        cpf.focus();
    } else {
        labelCPF.textContent = "";
    }
});