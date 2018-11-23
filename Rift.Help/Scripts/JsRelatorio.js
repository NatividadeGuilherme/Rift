

function BuscarProdutosCliente(idCliente) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Relatorio/BuscarProdutosCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idCliente: idCliente },
        success: function (retorno) {
            $("#codigoDoProduto").empty();
            var cliente = document.getElementById("Cliente");
            $('#codigoDoProduto').append("<option value='" + 0 + "'>" + 'Todos' + "</option>")

            retorno.Resultado.forEach(function (item) {
                $("#codigoDoProduto").append("<option value='" + item.IdProduto + "'>" + item.Produto.NomeProduto + "</option>");
            });



        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

$(document).on('change', '#codigoDoCliente', function () {
    BuscarProdutosCliente($(this).val());
});