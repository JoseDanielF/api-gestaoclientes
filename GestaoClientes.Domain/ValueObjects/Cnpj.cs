using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GestaoClientes.Domain.ValueObjects;

public partial record Cnpj
{
    private const int CnpjMaxLength = 14;
    public string Numero { get; }

    private Cnpj(string numero)
    {
        Numero = numero;
    }

    public static Cnpj? Criar(string? numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return null;

        var numeroLimpo = ApenasNumeros().Replace(numero, "");

        if (numeroLimpo.Length != CnpjMaxLength)
            return null;


        return new Cnpj(numeroLimpo);
    }

    public override string ToString() => Numero;

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex ApenasNumeros();
}