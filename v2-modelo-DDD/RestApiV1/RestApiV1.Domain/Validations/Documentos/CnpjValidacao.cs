namespace RestApiV1.Domain.Validations.Documentos
{
    public partial class CpfValidacao
    {
        public class CnpjValidacao
        {
            public const int TAMANHO_CNPJ = 14;

            public static bool Validar(string cpnj)
            {
                string _cnpjNumeros = Utils.ApenasNumeros(cpnj);

                if (!TemTamanhoValido(_cnpjNumeros))
                    return false;

                return !TemDigitosRepetidos(_cnpjNumeros) && TemDigitosValidos(_cnpjNumeros);
            }

            private static bool TemTamanhoValido(string valor)
            {
                return valor.Length == TAMANHO_CNPJ;
            }

            private static bool TemDigitosRepetidos(string valor)
            {
                string[] _numerosInvalidos =
                {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

                return _numerosInvalidos.Contains(valor);
            }

            private static bool TemDigitosValidos(string valor)
            {
                string _numero = valor.Substring(0, TAMANHO_CNPJ - 2);

                DigitoVerificador _digitoVerificador = new DigitoVerificador(_numero).ComMultiplicadoresDeAte(2, 9)
                                                                                     .Substituindo("0", 10, 11);

                string _primeiroDigito = _digitoVerificador.CalculaDigito();

                _digitoVerificador.AddDigito(_primeiroDigito);

                string _segundoDigito = _digitoVerificador.CalculaDigito();

                return string.Concat(_primeiroDigito, _segundoDigito) == valor.Substring(TAMANHO_CNPJ - 2, 2);
            }
        }
    }
}
