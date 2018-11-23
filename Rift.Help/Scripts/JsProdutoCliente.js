var botao = document.getElementById("botaozinho");
botao.addEventListener("click", ActivateProdutoCliente);


function ActivateProdutoCliente(idProdutoCliente) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Produto/ActivateProdutoCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idProdutoCliente: idProdutoCliente },
        success: function (retorno) {
            if (retorno.Sucesso == "ATIVO") {
                swal("Atenção", "Produto está inativo, \n não é possível registrar chamado para este item", "warning");
            }else {
                swal("Sucesso", "Produto foi ativado!", "success");     
            }
            setTimeout(function () {
                window.location.reload()
            }, 2000);
            //window.location.reload();
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

var botaoExcluir = document.getElementById("botaoExcluir");
botaoExcluir.addEventListener("Click", RemoverProdutoCliente)

function RemoverProdutoCliente(idProdutoCliente) {
    swal("Confirma a remoção do Produto a este cliente ?",
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
                    ProcessarExclusao(idProdutoCliente)
                    break;
                default:
            }
        });
}



function ProcessarExclusao(idProdutoCliente) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Produto/RemoverProdutoCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idProdutoCliente: idProdutoCliente },
        success: function (retorno) {
            if (retorno.Resultado == true) {
                swal("Sucesso", "Produto Removido!", "success");
            } else {
                swal("Atenção", retorno.Resultado, "warning");
                setTimeout(function () {
                    window.location.reload()
                }, 2000);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Erro');
        }
    });
}