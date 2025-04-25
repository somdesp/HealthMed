using System.Text.RegularExpressions;

namespace HealthMed.BuildingBlocks.ValueObjects;

public sealed class CPF : IEquatable<CPF>
{
    public string Numero { get; }

    protected CPF() { }

    public CPF(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("CPF não pode ser vazio.");

        numero = SomenteNumeros(numero);

        if (numero.Length != 11 || !EhValido(numero))
            throw new ArgumentException("CPF inválido.");

        Numero = numero;
    }

    public override string ToString()
    {
        return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
    }

    private static string SomenteNumeros(string texto)
    {
        return Regex.Replace(texto, "[^0-9]", "");
    }

    private static bool EhValido(string cpf)
    {
        if (new string(cpf[0], 11) == cpf)
            return false;

        var soma = 0;
        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        if ((cpf[9] - '0') != digito1)
            return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return (cpf[10] - '0') == digito2;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as CPF);
    }

    public bool Equals(CPF other)
    {
        if (other is null)
            return false;

        return Numero == other.Numero;
    }

    public override int GetHashCode()
    {
        return Numero.GetHashCode();
    }

    public static bool operator ==(CPF left, CPF right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CPF left, CPF right)
    {
        return !Equals(left, right);
    }
}