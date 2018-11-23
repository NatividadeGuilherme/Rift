var botao = document.getElementById("botaozinho");
botao.addEventListener("click", ApagarProduto);

function ApagarProduto(idProduto)
{
    swal("Confirma a exclusão deste produto ?",
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
                    ProcessarExclusaoProduto(idProduto)
                    window.location.reload();
                    break;
                default:
                    

             }

        });
}
function ProcessarExclusaoProduto(idProduto) {
    $.ajax({
        type: "POST",
        url: document.location.origin + "/Produto/ProcessarExclusao",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { idProduto: idProduto },
        success: function (retorno) {
         
            
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            
        }
    });
}
