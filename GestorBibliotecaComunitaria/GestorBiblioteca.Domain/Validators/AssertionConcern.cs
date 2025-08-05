using System.Text.RegularExpressions;

namespace MicroServico.Domain.Validators
{
    public static class AssertionConcern
    {
        public static void ValidaObjetoIgual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaObjetoDiferente(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaObjetoNaoNulo(object object1, string message)
        {
            if (object1 == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaParametroFalso(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaComprimentoMaximo(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaComprimentoMinimo(string stringValue, int minimum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaComprimentoMinimoEMaximo(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaPadraoCorresponde(string pattern, string stringValue, string message)
        {
            Regex regex = new(pattern);

            if (!regex.IsMatch(stringValue))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaTextoSemDados(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMinimoEMaximo(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMinimoEMaximo(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMinimoEMaximo(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMinimoEMaximo(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaParametroVerdadeiro(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaEstadoFalso(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaEstadoVerdadeiro(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaDataMenorOuIgual(DateTime data, DateTime dataMinima, string message)
        {
            if (data <= dataMinima)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaDataMaiorOuIgual(DateTime data, DateTime dataMinima, string message)
        {
            if (data >= dataMinima)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaDataMaiorQueData(DateTime dataInicial, DateTime dataFinal, string message)
        {
            if (dataInicial > dataFinal)
            {
                throw new InvalidOperationException(message);
            }
        }
        public static void ValidaDataMenorQueData(DateTime dataInicial, DateTime dataFinal, string message)
        {
            if (dataInicial < dataFinal)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMenorOuIgualQueZero(int valor, string message)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMenorQueZero(double valor, string message)
        {
            if (valor < 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMaiorQueZero(int valor, string message)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorMaiorQueZero(double valor, string message)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaStringNaoVaziaOuNula(string texto, string message)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidaValorDiferente(int valor1, int valor2, string message)
        {
            if (valor1 == valor2)
            {
                throw new InvalidOperationException(message);
            }
        }
        public static void ValidaStatusRange(int valor, string message)
        {
            if (valor <= 0 || valor >= 3)
            {
                throw new InvalidOperationException(message);
            }
        }
    }

}
