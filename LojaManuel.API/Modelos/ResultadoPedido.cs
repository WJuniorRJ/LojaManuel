namespace LojaManuel.API.Modelos
{
    public class ResultadoPedido
    {
        public int PedidoId { get; set; }
        public List<Caixa> Caixas { get; set; } = [];
    }
}
