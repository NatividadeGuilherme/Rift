function BuscarProdutosCliente(idCliente) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Chamado/BuscarProdutosCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idCliente: idCliente },
        success: function (retorno) {
            $("#Produto").empty();
            var cliente = document.getElementById("Cliente");
           
            retorno.ProdutosPorCliente.forEach(function (item) {
                $("#Produto").append("<option value='" + item.IdProduto + "'>" + item.Produto.NomeProduto + "</option>");
            });



        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

$(document).on('change', '#Cliente', function () {
    BuscarProdutosCliente($(this).val());
});



function ApagarChamado(idChamado) {
    swal("Confirma a exclusão deste chamado ?",
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
                    ApagarChamado(idChamado)
                    break;
                default:
            }
        });
}


function ApagarChamado(idChamado) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Chamado/ExcluirChamado",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idChamado: idChamado },
        success: function (retorno) {
            if (retorno.Result == true) {
                swal("Sucesso", "Registro Excluído!", "success");
                window.location.href = "/Chamado/Index";
            } else {
                swal("Atenção!", retorno.Result, "warning");
            } 
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}
