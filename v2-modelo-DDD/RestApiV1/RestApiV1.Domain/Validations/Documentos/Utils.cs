namespace RestApiV1.Domain.Validations.Documentos
{
    public partial class CpfValidacao
    {
        public class Utils
        {
            public static string ApenasNumeros(string valor)
            {
                string _somenteNumero = string.Empty;

                foreach (char digito in valor)
                {
                    if (char.IsDigit(digito))
                        _somenteNumero += digito;
                }

                return _somenteNumero.Trim();
            }
        }
    }
}
