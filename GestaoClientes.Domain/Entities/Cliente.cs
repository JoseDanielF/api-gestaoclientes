using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoClientes.Domain.ValueObjects;

namespace GestaoClientes.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string NomeFantasia { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public bool Ativo { get; private set; }

    private Cliente(string nomeFantasia, Cnpj cnpj)
    {
        Id = Guid.NewGuid();
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Ativo = true; 
    }

    public static Cliente Criar(string nomeFantasia, Cnpj cnpj)
    {
        if (string.IsNullOrWhiteSpace(nomeFantasia))
        {
            throw new ArgumentException("O nome fantasia não pode ser vazio.", nameof(nomeFantasia));
        }

        return new Cliente(nomeFantasia, cnpj);
    }

    public void Inativar()
    {
        Ativo = false;
    }

    public void Reativar()
    {
        Ativo = true;
    }
}