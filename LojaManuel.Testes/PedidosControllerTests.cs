using LojaManuel.API.Controllers;
using LojaManuel.API.Modelos;

using Microsoft.AspNetCore.Mvc;

// using Newtonsoft.Json; // Para visualização manual

namespace LojaManuel.Testes
{
    public class PedidosControllerTests
    {
        [Fact]
        public void ProcessarPedidos_DeveRetornarCaixasUsadas()
        {
            // Arrange
            List<Pedido> pedidos =
            [
                new()
                {
                    Id = 1,
                    Produtos =
                    [
                        new() { Id = "Produto 1", Dimensoes = new(){ Altura = 10, Largura = 10, Comprimento = 10 } },
                        new() { Id = "Produto 2", Dimensoes = new(){ Altura = 20, Largura = 20, Comprimento = 20 } }
                    ]
                }
            ];

            EmpacotamentoController controller = new();

            // Act
            var result = controller.ProcessarPedidos(pedidos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoPedidos = Assert.IsType<List<ResultadoPedido>>(okResult.Value);
            // string teste = JsonConvert.SerializeObject(resultadoPedidos).ToString(); // Para visualização manual

            Assert.NotEmpty(resultadoPedidos);
            Assert.NotEmpty(resultadoPedidos[0].Caixas);
        }

        [Fact]
        public void ProcessarPedidos_VariosPedidos()
        {
            // Arrange
            List<Pedido> pedidos =
            [
                new()
                {
                    Id = 1,
                    Produtos =
                    [
                        new() { Id = "PS5", Dimensoes = new(){ Altura = 40, Largura = 10, Comprimento = 25 } },
                        new() { Id = "Volante", Dimensoes = new(){ Altura = 40, Largura = 30, Comprimento = 30 } }
                    ]
                },
                new()
                {
                    Id = 2,
                    Produtos =
                    [
                        new() { Id = "Joystick", Dimensoes = new(){ Altura = 15, Largura = 20, Comprimento = 10 } },
                        new() { Id = "Fifa 24", Dimensoes = new(){ Altura = 10, Largura = 30, Comprimento = 10 } },
                        new() { Id = "Call of Duty", Dimensoes = new(){ Altura = 30, Largura = 15, Comprimento = 10 } }
                    ]
                },
                new()
                {
                    Id = 3,
                    Produtos =
                    [
                        new() { Id = "Headset", Dimensoes = new(){ Altura = 25, Largura = 15, Comprimento = 20 } }
                    ]
                },
                new()
                {
                    Id = 4,
                    Produtos =
                    [
                        new() { Id = "Mouse Gamer", Dimensoes = new(){ Altura = 5, Largura = 8, Comprimento = 12 } },
                        new() { Id = "Teclado Mecânico", Dimensoes = new(){ Altura = 4, Largura = 45, Comprimento = 15 } }
                    ]
                },
                new()
                {
                    Id = 5,
                    Produtos =
                    [
                        new() { Id = "Cadeira Gamer", Dimensoes = new(){ Altura = 120, Largura = 60, Comprimento = 70 } }
                    ]
                }
            ];

            EmpacotamentoController controller = new();

            // Act
            var result = controller.ProcessarPedidos(pedidos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoPedidos = Assert.IsType<List<ResultadoPedido>>(okResult.Value);
            // string teste = JsonConvert.SerializeObject(resultadoPedidos).ToString(); // Para visualização manual

            Assert.NotEmpty(resultadoPedidos);
            Assert.NotEmpty(resultadoPedidos[0].Caixas);
        }

        [Fact]
        public void ProcessarPedidos_DeveEmpacotarProdutosEmUmaCaixaSePossivel()
        {
            // Arrange
            Pedido pedido = new()
            {
                Id = 4,
                Produtos = 
                [
                    new() { Id = "Mouse Gamer", Dimensoes = new(){ Altura = 5, Largura = 8, Comprimento = 12 } },
                    new() { Id = "Teclado Mecânico", Dimensoes = new(){ Altura = 4, Largura = 45, Comprimento = 15 } }
                ]
            };

            EmpacotamentoController controller = new();

            // Act
            var result = controller.ProcessarPedidos([pedido]);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoPedidos = Assert.IsType<List<ResultadoPedido>>(okResult.Value);
            // string teste = JsonConvert.SerializeObject(resultadoPedidos).ToString(); // Para visualização manual

            Assert.NotEmpty(resultadoPedidos);
            Assert.Single(resultadoPedidos);
            var resultadoPedido = resultadoPedidos[0];
            Assert.NotEmpty(resultadoPedido.Caixas);
            Assert.Single(resultadoPedido.Caixas);
            var caixa = resultadoPedido.Caixas[0];
            Assert.Equal(2, caixa.Produtos.Count);
        }

        [Fact]
        public void ProcessarPedidos_VariosPedidos_Grandes()
        {
            // Arrange
            List<Pedido> pedidos =
            [
                new()
                {
                    Id = 1,
                    Produtos =
                    [
                        new() { Id = "PS5", Dimensoes = new(){ Altura = 40, Largura = 10, Comprimento = 25 } },
                        new() { Id = "Volante", Dimensoes = new(){ Altura = 40, Largura = 30, Comprimento = 30 } },
                        new() { Id = "Joystick", Dimensoes = new(){ Altura = 15, Largura = 20, Comprimento = 10 } },
                        new() { Id = "Fifa 24", Dimensoes = new(){ Altura = 10, Largura = 30, Comprimento = 10 } },
                        new() { Id = "Call of Duty", Dimensoes = new(){ Altura = 30, Largura = 15, Comprimento = 10 } },
                        new() { Id = "Headset", Dimensoes = new(){ Altura = 25, Largura = 15, Comprimento = 20 } }
                    ]
                },
                new()
                {
                    Id = 2,
                    Produtos =
                    [
                        new() { Id = "Mouse Gamer", Dimensoes = new(){ Altura = 5, Largura = 8, Comprimento = 12 } },
                        new() { Id = "Teclado Mecânico", Dimensoes = new(){ Altura = 4, Largura = 45, Comprimento = 15 } },
                        new() { Id = "Cadeira Gamer", Dimensoes = new(){ Altura = 120, Largura = 60, Comprimento = 70 } }
                    ]
                },
                new()
                {
                    Id = 3,
                    Produtos =
                    [
                        new() { Id = "PS5", Dimensoes = new(){ Altura = 40, Largura = 10, Comprimento = 25 } },
                        new() { Id = "Volante", Dimensoes = new(){ Altura = 40, Largura = 30, Comprimento = 30 } },
                        new() { Id = "Joystick", Dimensoes = new(){ Altura = 15, Largura = 20, Comprimento = 10 } },
                        new() { Id = "Fifa 24", Dimensoes = new(){ Altura = 10, Largura = 30, Comprimento = 10 } },
                        new() { Id = "Call of Duty", Dimensoes = new(){ Altura = 30, Largura = 15, Comprimento = 10 } },
                        new() { Id = "Headset", Dimensoes = new(){ Altura = 25, Largura = 15, Comprimento = 20 } },
                        new() { Id = "Mouse Gamer", Dimensoes = new(){ Altura = 5, Largura = 8, Comprimento = 12 } },
                        new() { Id = "Teclado Mecânico", Dimensoes = new(){ Altura = 4, Largura = 45, Comprimento = 15 } },
                        new() { Id = "Cadeira Gamer", Dimensoes = new(){ Altura = 120, Largura = 60, Comprimento = 70 } }
                    ]
                }
            ];

            EmpacotamentoController controller = new();

            // Act
            var result = controller.ProcessarPedidos(pedidos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultadoPedidos = Assert.IsType<List<ResultadoPedido>>(okResult.Value);
            // string teste = JsonConvert.SerializeObject(resultadoPedidos).ToString(); // Para visualização manual

            Assert.NotEmpty(resultadoPedidos);
            Assert.NotEmpty(resultadoPedidos[0].Caixas);

            Assert.True(resultadoPedidos[0].Caixas.Count == 1);
            Assert.True(resultadoPedidos[1].Caixas.Count == 2);
            Assert.True(resultadoPedidos[2].Caixas.Count == 2);
        }
    }
}
