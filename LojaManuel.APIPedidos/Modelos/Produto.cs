namespace LojaManuel.APIPedidos.Modelos
{
    public class Produto
    {
        public string Id { get; set; } = string.Empty;
        public Dimensoes Dimensoes { get; set; } = new();
    }

    public class Dimensoes
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
    }
}
