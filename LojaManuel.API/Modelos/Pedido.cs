namespace LojaManuel.API.Modelos
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<Produto> Produtos { get; set; } = [];
    }
}
