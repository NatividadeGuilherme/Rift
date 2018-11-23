var esqueciminhaSenha = document.querySelector("#p-password");

esqueciminhaSenha.addEventListener("click", DispararEmail);
var email =document.querySelector('#emaillogin');

function DispararEmail() {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Colaborador/RecuperarSenha",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { email: email.value  },
        success: function (retorno) {
            if (retorno.Result == '') {
                swal("Atenção", "E-mail inválido não cadastrado!", "warning");
            } else {
                swal("Sucesso", "Sua senha foi enviada para o e-mail fornecido\n Consulte sua caixa de entrada!", "success");
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

