﻿// <auto-generated />
using System;
using LocadorAutomoveis.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LocadorAutomoveis.Infra.Orm.Migrations
{
    [DbContext(typeof(LocadorAutomoveisDbContext))]
    partial class LocadorAutomoveisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloCupom.Cupom", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("ParceiroId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ParceiroId");

                    b.ToTable("TBCupom", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloDisciplina.Disciplina", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TBDisciplina", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloFuncionario.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DataAdmissao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Salario")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBFuncionario", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloGrupoAutomoveis.GrupoAutomoveis", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBGrupoAutomoveis", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloParceiro.Parceiro", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBParceiro", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloPlanoCobranca.PlanoCobranca", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrupoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("KmLivre")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoDiario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoKm")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoPlano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.ToTable("TBPlanoCobranca", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloTaxasEServicos.TaxasEServico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanoDeCalculo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.ToTable("TBTaxasEServico", (string)null);
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloCupom.Cupom", b =>
                {
                    b.HasOne("LocadorAutomoveis.Dominio.ModuloParceiro.Parceiro", "Parceiro")
                        .WithMany("Cupons")
                        .HasForeignKey("ParceiroId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TBCupom_TBParceiro");

                    b.Navigation("Parceiro");
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloPlanoCobranca.PlanoCobranca", b =>
                {
                    b.HasOne("LocadorAutomoveis.Dominio.ModuloGrupoAutomoveis.GrupoAutomoveis", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TBPlanoCobranca_TBGrupoAutomoveis");

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("LocadorAutomoveis.Dominio.ModuloParceiro.Parceiro", b =>
                {
                    b.Navigation("Cupons");
                });
#pragma warning restore 612, 618
        }
    }
}
