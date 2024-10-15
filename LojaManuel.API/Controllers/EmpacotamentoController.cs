using Microsoft.AspNetCore.Mvc;

namespace LojaManuel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpacotamentoController : Controller
    {
        [HttpPost]
        public IActionResult ProcessarPedidos(List<Modelos.Pedido> pedidos)
        {
            var resultado = new List<Modelos.ResultadoPedido>();

            foreach (Modelos.Pedido pedido in pedidos)
            {
                var caixasUsadas = CalcularEmbalagem(pedido);
                resultado.Add(new Modelos.ResultadoPedido
                {
                    PedidoId = pedido.Id,
                    Caixas = caixasUsadas
                });
            }

            return Ok(resultado);
        }

        private static List<Modelos.Caixa> CalcularEmbalagem(Modelos.Pedido pedido)
        {
            List<Modelos.Caixa> caixasDisponiveis =
            [
                new("Caixa 1", 30, 40, 80),
                new("Caixa 2", 80, 50, 40),
                new("Caixa 3", 50, 80, 60)
            ];

            List<Modelos.Produto> produtosOrdenados = [.. pedido.Produtos.OrderByDescending(p => p.Dimensoes.Altura * p.Dimensoes.Largura * p.Dimensoes.Comprimento)];
            List<Modelos.Caixa> caixasUsadas = [];

            // Primeiro passo é verificar se o volume total dos produtos cabem em alguma caixa (para agilizar o processamento)
            // Verificar caixa de menor volume para maior
            double volumeTotal = produtosOrdenados.Sum(p => p.Volume);
            if (caixasDisponiveis.Any(cd => cd.Volume >= volumeTotal))
            {
                foreach (var caixa in caixasDisponiveis)
                {
                    if (caixa.Volume > volumeTotal)
                    {
                        caixa.AdicionarListaProdutos(produtosOrdenados);
                        caixasUsadas.Add(caixa);
                        return caixasUsadas;
                    }
                }
            }

            // Caso não caibam todos numa única caixa
            foreach (var produto in produtosOrdenados)
            {
                // Verifica primeiro se produto cabe em alguma caixa já em uso, caso exista
                if (caixasUsadas.Count != 0)
                {
                    foreach (var caixaUsada in caixasUsadas)
                    {
                        if (caixaUsada.VolumeLivre > produto.Volume)
                        {
                            caixaUsada.AdicionarProduto(produto);
                            produto.Embalado = true;
                            break;
                        }
                    }
                }

                // Se o produto não coube em nenhuma caixa em uso ou ainda não existam caixas em uso
                if (!produto.Embalado && caixasDisponiveis.Any(cd => cd.Volume >= produto.Volume))
                {
                    foreach (var caixa in caixasDisponiveis)
                    {
                        if (caixa.CabeProduto(produto))
                        {
                            caixa.AdicionarProduto(produto);
                            produto.Embalado = true;
                            caixasUsadas.Add(caixa);
                            break;
                        }
                    }
                }

                // Se o produto não couber em nenhuma das caixas, adicionar uma nova caixa personalizada
                if (!produto.Embalado)
                {
                    var novaCaixa = new Modelos.Caixa("Caixa Personalizada", produto.Dimensoes.Altura, produto.Dimensoes.Largura, produto.Dimensoes.Comprimento);
                    novaCaixa.AdicionarProduto(produto);
                    caixasDisponiveis.Add(novaCaixa);
                    caixasUsadas.Add(novaCaixa);
                }
            }

            return caixasUsadas;
        }
    }
}
