var enderecoProduto = "https://localhost:5001/Produtos/Produto/";
var enderecoGerarVenda = "https://localhost:5001/Produtos/GerarVenda/";
var produto;
var compra = [];
var __totalVenda__ = 0.0;

$("#posvenda").hide();
atualizarTotal();

function atualizarTotal(){
    $("#totalVenda").html(__totalVenda__);
}

function preencherFormulario(dadosProduto){
    $("#campoNome").val(dadosProduto.nome);
    $("#campoCategoria").val(dadosProduto.categoria.nome);
    $("#campoFornecedor").val(dadosProduto.fornecedor.nome);
    $("#campoPreco").val(dadosProduto.precoDeVenda);
}

function zerarFormulario(){
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
}

function adicionarNaTabela(p, q){
    var produtoTemp = {};

    Object.assign(produtoTemp, produto);
    var venda = {produto: produtoTemp , quantidade: q, subtotal: produtoTemp.precoDeVenda * q}
    
    __totalVenda__ += venda.subtotal;
    atualizarTotal();
    
    compra.push(venda);

    $("#compras").append(`<tr>
        <td>${p.id}</td>
        <td>${p.nome}</td>
        <td>${q}</td>
        <td>R$ ${p.precoDeVenda}</td>
        <td>${p.medida}</td>
        <td>R$ ${p.precoDeVenda * q}</td>
        <td><button class='btn btn-danger'>Remover</button></td>
    </tr>`);
}

$("#produtoForm").on("submit", function(e){
    event.preventDefault();
    var produtoParaTabela = produto;
    var qtd = $("#campoQuantidade").val();
    adicionarNaTabela(produtoParaTabela, qtd);
    // var produto = undefined;
    zerarFormulario();
});

$("#pesquisar").click(function(){
    var codProduto = $("#codProduto").val();
    var enderecoTemporario = enderecoProduto+codProduto;
    $.post(enderecoTemporario, function(dados, status){
            produto = dados;
            
            var med = "";
            switch(produto.medida){
                case 0:
                    med = "L";
                    break;
                case 1:
                    med = "Kg";
                    break;
                case 2:
                    med = "Un";
                    break;
                default:
                    med = "Un";
                    break;
            }

            produto.medida = med;

            preencherFormulario(produto);
    }).fail(function(){
        alert("Produto inválido");
    });
});

$("#finalizarVendaBTN").click(function () {
    if(__totalVenda__ <= 0){
        alert("Compra inválida. Nenhum produto adicionado.");
        return;
    }

    var _valorPago = $("#valorPago").val();

    if(!isNaN(_valorPago)){
        _valorPago = parseFloat(_valorPago);
        if(_valorPago >= __totalVenda__){
            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);
            
            var _troco = _valorPago - __totalVenda__;
            $("#troco").val(_troco);

            compra.forEach(x => {
                x.produto = x.produto.id;
            });

            var _venda = {Total: __totalVenda__, Troco: _troco, Produtos: compra}

            $.ajax({
                type: "POST",
                url: enderecoGerarVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(_venda),
                success: function(data){
                    console.log("Dados enviados com sucesso");
                    console.log(data);
                }
            });
        }else{
            alert("Valor pago está abaixo do valor devido.");
        return;
        }
    }else{
        alert("Valor pago inválido.");
        return;
    }
})

function restaurarModal(){
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
}

$("#fecharModal").click(function(){
    restaurarModal();
})