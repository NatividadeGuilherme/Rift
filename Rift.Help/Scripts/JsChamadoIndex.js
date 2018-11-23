MudarCor();
function MudarCor() {
    
    var status = document.querySelectorAll("#status-obrigatorio");

    status.forEach(function (texto) {
        if (texto.textContent == 'Em andamento') {
            texto.style.color = "orange"; 
        } else {
            texto.style.color = "#00FF00";
        } 
    });
}


function BuscarChamados() {
    var produto = document.getElementById("ProdutoIndex").value;
    var cliente = document.getElementById("ClienteIndex").value;

    $.ajax({
        type: "GET",
        url: document.location.origin + "/Chamado/RetornarRegistrosPorClienteProduto",
        data: { IdProduto: produto/*$('#ProdutoPraRelatorio').val()*/, IdCliente: cliente /*$('#ClientePraRelatorio').val()*/ },
        success: function (retorno) {

            $('#divListaChamados').html(retorno);

            $('#table-chamados').DataTable({
                order: [[3, 'DESC']],
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Portuguese-Brasil.json"
                }
            }
            );
            //$('#tabela-todoschamados tr').remove();

            //var tabela = document.querySelector("#tabela-todoschamados");
            //retorno.Resultado.forEach(function (chamado) {

            //    var trChamado = document.createElement("tr");

            //    trChamado.appendChild(criarTd(chamado.IdChamado));
            //    trChamado.appendChild(criarTd(chamado.Titulo));
            //    trChamado.appendChild(criarTd(chamado.DataAbertura));
            //    trChamado.appendChild(criarTd(chamado.Status));
            //    trChamado.appendChild(criarTd(chamado.Produt.NomeProduto));
            //    trChamado.appendChild(criarTd(chamado.Client.NomeFantasia));
            //    trChamado.appendChild(criarTd(chamado.Colaborad.Nome));
            //    tabela.appendChild(trChamado);
            //});
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

            alert(errorThrown);
        }
    });
}

//$(document).on('change', '#Produto', function () {
//    BuscarChamados($(this).val());
//});

function criarTd(dado) {
    var chamadoTd = document.createElement('td');

    chamadoTd.textContent = dado;
    return chamadoTd;
}

function BuscarProdutosCliente(idCliente) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Chamado/BuscarProdutosCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idCliente: idCliente },
        success: function (retorno) {
            $("#ProdutoIndex").empty();
            var cliente = document.getElementById("Cliente");

            $("#ProdutoIndex").append("<option value='" + 0 + "'>" + "Todos" + "</option>");
            retorno.ProdutosPorCliente.forEach(function (item) {
                $("#ProdutoIndex").append("<option value='" + item.IdProduto + "'>" + item.Produto.NomeProduto + "</option>");
            });



        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error

        }
    });
}

$(document).on('change', '#ClienteIndex', function () {
    BuscarProdutosCliente($(this).val());
});