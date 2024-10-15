namespace LojaManuel.APIPedidos.Modelos
{
    public class Caixa
    {
        public string Nome { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public Caixa() {
            Nome = string.Empty;
            Altura = 0;
            Largura = 0;
            Comprimento = 0;
        }
        public Caixa(string nome, double altura, double largura, double comprimento)
        {
            Nome = nome;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }

        public bool CabeProduto(Produto produto)
        {
            return produto.Dimensoes.Altura <= Altura && produto.Dimensoes.Largura <= Largura && produto.Dimensoes.Comprimento <= Comprimento;
        }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public double CalcularEspacoLivre(Produto produto)
        {
            double volumeCaixa = Altura * Largura * Comprimento;
            double volumeOcupado = Produtos.Sum(p => p.Dimensoes.Altura * p.Dimensoes.Largura * p.Dimensoes.Comprimento);
            double volumeProduto = produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;
            return volumeCaixa - (volumeOcupado + volumeProduto);
        }
    }
}
