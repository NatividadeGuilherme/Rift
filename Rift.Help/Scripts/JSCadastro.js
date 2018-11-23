var resp = null;
var labelEsquerdo = document.querySelector("#validarEmail");
function BuscarCEP(Cep) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Cliente/BuscaCEP",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { Cep: Cep },
        success: function (retorno) {
            if (retorno != null || retorno != '') {
                $('#Endereco').val(retorno.Endereco);
                $('#Bairro').val(retorno.Bairro);
                $('#uf').val(retorno.Estado).trigger("change");
                $("#Cidade").append("<option value='" + item.Nome + "'>" + item.Nome + "</option>")
                resp = 1;
            }
            
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            alert('Deu errro')
        }
    });
}
$(document).on('blur', '#Cep', function () {
    BuscarCEP($(this).val());
});



function BuscarCidadesPorEstado(uf) {
    $.ajax({
        type: "GET",
        url: document.location.origin + "/Cliente/BuscarCidadesPorEstado",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { uf: uf },
        success: function (retorno) {
            if (resp == null) {
                $("#Cidade").empty();
                retorno.Cidade.forEach(function (item) {
                    $("#Cidade").append("<option value='" + item.Nome + "'>" + item.Nome + "</option>");
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            alert('teste');
        }
    });
}

$(document).on('change', '#uf', function () {
    BuscarCidadesPorEstado($(this).val());
});

function validacaoEmail(field) {
    var 
    usuario = field.value.substring(0, field.value.indexOf("@"));
    dominio = field.value.substring(field.value.indexOf("@") + 1, field.value.length);

    if ((usuario.length >= 1) &&
        (dominio.length >= 3) &&
        (usuario.search("@") == -1) &&
        (dominio.search("@") == -1) &&
        (usuario.search(" ") == -1) &&
        (dominio.search(" ") == -1) &&
        (dominio.search(".") != -1) &&
        (dominio.indexOf(".") >= 1) &&
        (dominio.lastIndexOf(".") < dominio.length - 1)) {
       
        labelEsquerdo.textContent=""
       
    }
    else {
        
        
        $('#Email').focus();
        labelEsquerdo.textContent = "E-mail inválido";
          
    }
}


function Onlynumbers(e) {
    var tecla = new Number();
    if (window.event) {
        tecla = e.keyCode;
    }
    else if (e.which) {
        tecla = e.which;
    }
    else {
        return true;
    }
    if ((tecla >= "97") && (tecla <= "122")) {
        return false;
    }
}