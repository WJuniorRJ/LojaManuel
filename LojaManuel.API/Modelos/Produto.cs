using System.Text.Json.Serialization;

namespace LojaManuel.API.Modelos
{
    public class Produto
    {
        /// <summary>
        /// Identificador do produto
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Dimensões do produto
        /// </summary>
        public Dimensoes Dimensoes { get; set; } = new();

        #region Propriedades de processamento 

        /// <summary>
        /// Fornece o volume do produto
        /// </summary>
        [JsonIgnore]
        public double Volume => Dimensoes.Altura * Dimensoes.Largura * Dimensoes.Comprimento;

        /// <summary>
        /// Indica se produto já está embalado (Usado para processamento)
        /// </summary>
        [JsonIgnore]
        public bool Embalado { get; set; } = false;

        #endregion
    }

    public class Dimensoes
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
    }
}
