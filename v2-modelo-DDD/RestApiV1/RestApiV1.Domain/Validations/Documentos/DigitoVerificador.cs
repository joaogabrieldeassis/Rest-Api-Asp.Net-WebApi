namespace RestApiV1.Domain.Validations.Documentos
{
    public partial class CpfValidacao
    {
        public class DigitoVerificador
        {
            private const int MODULO = 11;

            private string _numero;
            private bool _complementarDoModulo = true;
            private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
            private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();

            public DigitoVerificador(string numero)
            {
                _numero = numero;
            }

            public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
            {
                _multiplicadores.Clear();

                for (int indice = primeiroMultiplicador; indice <= ultimoMultiplicador; indice++)
                    _multiplicadores.Add(indice);

                return this;
            }

            public DigitoVerificador Substituindo(string substituto, params int[] digitos)
            {
                foreach (int digito in digitos)
                    _substituicoes[digito] = substituto;

                return this;
            }

            public void AddDigito(string digito)
            {
                _numero = string.Concat(_numero, digito);
            }

            public string CalculaDigito()
            {
                return !(_numero.Length > 0) ? string.Empty : GetDigitSum();
            }

            private string GetDigitSum()
            {
                int _soma = 0;

                for (int indice = _numero.Length - 1, m = 0; indice >= 0; indice--)
                {
                    int _produto = (int)char.GetNumericValue(_numero[indice]) * _multiplicadores[m];
                    _soma += _produto;

                    if (++m >= _multiplicadores.Count)
                        m = 0;
                }

                int _modulo = (_soma % MODULO);
                var _resultado = _complementarDoModulo ? MODULO - _modulo : _modulo;

                return _substituicoes.ContainsKey(_resultado) ? _substituicoes[_resultado] : _resultado.ToString();
            }
        }
    }
}
