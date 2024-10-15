namespace LojaManuel.API.Modelos
{
    public class Caixa
    {
        public string Nome { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public List<Produto> Produtos { get; set; } = [];


        public double Volume => Altura * Largura * Comprimento;
        public double VolumeUsado => Produtos.Sum(p => p.Volume);
        public double VolumeLivre => Volume - VolumeUsado;

        public Caixa()
        {
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

        public bool CabeVolume(Produto produto)
        {
            return produto.Dimensoes.Altura <= Altura && produto.Dimensoes.Largura <= Largura && produto.Dimensoes.Comprimento <= Comprimento;
        }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public void AdicionarListaProdutos(List<Produto> produtos)
        {
            foreach (Produto produto in produtos)
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
