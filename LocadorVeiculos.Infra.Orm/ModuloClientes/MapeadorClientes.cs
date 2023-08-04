﻿using LocadorAutomoveis.Dominio.ModuloClientes;
using LocadorAutomoveis.Dominio.ModuloPlanoCobranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadorAutomoveis.Infra.Orm.ModuloClientes
{
    public class MapeadorClientes : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> planoBuilder)
        {
            planoBuilder.ToTable("TBClientes");

            planoBuilder.Property(p => p.Id).IsRequired().ValueGeneratedNever();

            planoBuilder.Property(g => g.NomeCliente).HasColumnType("varchar(200)").IsRequired();

            planoBuilder.Property(p => p.Cpf).HasColumnType("varchar(200)");

            planoBuilder.Property(p => p. Cnpj).HasColumnType("varchar(200)");

            planoBuilder.Property(p => p.Telefone).HasColumnType("varchar(200)");

        }

        
    }
}