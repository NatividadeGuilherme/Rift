//var botaozinho = document.querySelector("#botao");

//botaozinho.addEventListener("Click", ApagarAcoesChamado);

function ApagarAcoesChamado(idAcoes) {
    swal("Confirma a exclusão desta ação ?",
        {
            buttons:
            {
                Cancelar: "Cancelar",
                Confirmar:
                {
                    text: "Confirmar",
                    value: "Catch",
                },
            },
        })
        .then((value) => {
            switch (value) {
                case "Catch":
                    ProcessarExclusao(idAcoes)
                    break;
                default:
            }
        });
}


function ProcessarExclusao(idAcoes) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Acoes/ExcluirAcao",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idAcoes: idAcoes },
        success: function (retorno) {
            if (retorno.Result == true) {
                window.location.reload();
                
            } 
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

var botaoFinalizar = document.getElementById("botaoFinalizar");
botaoFinalizar.addEventListener("Click", FinalizarChamado);

function FinalizarChamado(idChamado) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Acoes/FinalizarChamado",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idChamado: idChamado },
        success: function (retorno) {
            if (retorno.Sucess == true) {
                swal("Sucesso", "Chamado Finalizado", "success")
                setTimeout(function () {
                    window.location.href = "/Chamado/Index";

                },4000);
            } else {
                alert('Error');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('erro');

        }
    });
}

