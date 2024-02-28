namespace RestApiV1.Domain.Validations.Documentos
{
    public partial class CpfValidacao
    {
        public const int TAMANHO_CPF = 11;

        public static bool Validar(string cpf)
        {
            string _cpfNumeros = Utils.ApenasNumeros(cpf);

            if (!TamanhoValido(_cpfNumeros))
                return false;

            return !TemDigitosRepetidos(_cpfNumeros) && TemDigitosValidos(_cpfNumeros);
        }

        private static bool TamanhoValido(string valor)
        {
            return valor.Length == TAMANHO_CPF;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] _numerosInvalidos =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return _numerosInvalidos.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            string _numero = valor.Substring(0, TAMANHO_CPF - 2);

            DigitoVerificador _digitoVerificador = new DigitoVerificador(_numero).ComMultiplicadoresDeAte(2, 11)
                                                                                 .Substituindo("0", 10, 11);

            string _primeiroDigito = _digitoVerificador.CalculaDigito();

            _digitoVerificador.AddDigito(_primeiroDigito);

            string _segundoDigito = _digitoVerificador.CalculaDigito();

            return string.Concat(_primeiroDigito, _segundoDigito) == valor.Substring(TAMANHO_CPF - 2, 2);
        }
    }
}
