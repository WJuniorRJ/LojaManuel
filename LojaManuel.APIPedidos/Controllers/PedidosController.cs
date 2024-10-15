using LojaManuel.APIPedidos.Modelos;

using Microsoft.AspNetCore.Mvc;

namespace LojaManuel.APIPedidos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        [HttpPost]
        public IActionResult ProcessarPedidos(List<Pedido> pedidos)
        {
            var resultado = new List<ResultadoPedido>();

            foreach (var pedido in pedidos)
            {
                var caixasUsadas = CalcularEmbalagem(pedido);
                resultado.Add(new ResultadoPedido
                {
                    PedidoId = pedido.Id,
                    Caixas = caixasUsadas
                });
            }

            return Ok(resultado);
        }

        private List<Caixa> CalcularEmbalagem(Pedido pedido)
        {
            var caixasDisponiveis = new List<Caixa>
            {
                new("Caixa 1", 30, 40, 80),
                new("Caixa 2", 80, 50, 40),
                new("Caixa 3", 50, 80, 60)
            };

            var produtosOrdenados = pedido.Produtos
                .OrderByDescending(p => p.Dimensoes.Altura * p.Dimensoes.Largura * p.Dimensoes.Comprimento)
                .ToList();

            var caixasUsadas = new List<Caixa>();

            foreach (var produto in produtosOrdenados)
            {
                Caixa? melhorCaixa = null;
                double espacoRestante = double.MaxValue;

                foreach (var caixa in caixasDisponiveis)
                {
                    if (caixa.CabeProduto(produto))
                    {
                        double espacoLivre = caixa.CalcularEspacoLivre(produto);
                        if (espacoLivre < espacoRestante)
                        {
                            melhorCaixa = caixa;
                            espacoRestante = espacoLivre;
                        }
                    }
                }

                if (melhorCaixa != null)
                {
                    melhorCaixa.AdicionarProduto(produto);
                    if (!caixasUsadas.Contains(melhorCaixa))
                    {
                        caixasUsadas.Add(melhorCaixa);
                    }
                }
                else
                {
                    var novaCaixa = new Caixa("Caixa Personalizada", produto.Dimensoes.Altura, produto.Dimensoes.Largura, produto.Dimensoes.Comprimento);
                    novaCaixa.AdicionarProduto(produto);
                    caixasDisponiveis.Add(novaCaixa);
                    caixasUsadas.Add(novaCaixa);
                }
            }

            return caixasUsadas;
        }
    }
}
