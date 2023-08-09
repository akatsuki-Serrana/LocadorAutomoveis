﻿using FizzWare.NBuilder;
using LocadorAutomoveis.Dominio;
using LocadorAutomoveis.Dominio.ModuloCondutor;
using LocadorAutomoveis.Dominio.ModuloCupom;
using LocadorAutomoveis.Dominio.ModuloFuncionario;
using LocadorAutomoveis.Dominio.ModuloGrupoAutomoveis;
using LocadorAutomoveis.Dominio.ModuloParceiro;
using LocadorAutomoveis.Dominio.ModuloPlanoCobranca;
using LocadorAutomoveis.Dominio.ModuloTaxasEServicos;
using LocadorAutomoveis.Infra.Orm.Compartilhado;
using LocadorAutomoveis.Infra.Orm.ModiuloFuncionario;
using LocadorAutomoveis.Infra.Orm.ModuloCondutor;
using LocadorAutomoveis.Infra.Orm.ModuloGrupoAutomoveis;
using LocadorAutomoveis.Infra.Orm.ModuloParceiro;
using LocadorAutomoveis.Infra.Orm.ModuloPlanoCobranca;
using LocadorAutomoveis.Infra.Orm.ModuloTaxasEServicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LocadorAutomoveis.TestesIntegracao.Compartilhado
{
    public class TestesIntegracaoBase
    {
        protected IContextoPersistencia contextoPersistencia;      
        protected IRepositorioGrupoAutomoveis repositorioGrupoAutomoveis;
        protected IRepositorioFuncionario repositorioFuncionario;
        protected IRepositorioParceiro repositorioParceiro;
        protected IRepositorioTaxasServico repositorioTaxasServico;
        protected IRepositorioPlanoCobranca repositorioPlanoCobranca;
        protected IRepositorioCupom repositorioCupom;
        protected IRepositorioCondutor repositorioCondutor;
        public TestesIntegracaoBase()
        {

            var connectionString = ObterConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<LocadorAutomoveisDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            var dbContext = new LocadorAutomoveisDbContext(optionsBuilder.Options);
            contextoPersistencia = dbContext;

            var migracoesPendentes = dbContext.Database.GetPendingMigrations();

            if (migracoesPendentes.Count() > 0)
            {
                dbContext.Database.Migrate();
            }           

            LimparTabelas(dbContext);

            //GrupoAutomoveis
            repositorioGrupoAutomoveis = new RepositorioGrupoAutomoveisEmOrm(dbContext);
      
            BuilderSetup.SetCreatePersistenceMethod<GrupoAutomoveis>(repositorioGrupoAutomoveis.Inserir);

            //Funcionario
            repositorioFuncionario = new RepositorioFuncionarioEmOrm(dbContext);

            BuilderSetup.SetCreatePersistenceMethod<Funcionario>(repositorioFuncionario.Inserir);

            //Parceiro
            repositorioParceiro = new RepositorioParceiroOrm(dbContext);

            BuilderSetup.SetCreatePersistenceMethod<Parceiro>(repositorioParceiro.Inserir);

            //Taxa e Serviço
            repositorioTaxasServico = new RepositorioTaxasEServicosOrm(dbContext);
            BuilderSetup.SetCreatePersistenceMethod<TaxasEServico>(repositorioTaxasServico.Inserir);

            //Plano Cobrança
            repositorioPlanoCobranca = new RepositorioPlanoCobrancaEmOrm(dbContext);
            BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorioPlanoCobranca.Inserir);

            //Condutor
            repositorioCondutor = new RepositorioCondutorEmOrm(dbContext);
            BuilderSetup.SetCreatePersistenceMethod<Condutor>(repositorioCondutor.Inserir);

        }

        protected static void LimparTabelas(LocadorAutomoveisDbContext dbContext)
        {           
            LimparLista<GrupoAutomoveis>(dbContext);
            LimparLista<Parceiro>(dbContext);
            LimparLista<Funcionario>(dbContext);
            LimparLista<PlanoCobranca>(dbContext);
            LimparLista<TaxasEServico>(dbContext);
            LimparLista<Condutor>(dbContext);
            
        }
           

        private static void LimparLista<T>(LocadorAutomoveisDbContext dbContext)
        where T : EntidadeBase<T>
        {
            var registros = dbContext.Set<T>();
            registros.RemoveRange(registros);
            dbContext.SaveChanges();
        }

        protected static string ObterConnectionString()
        {
            var configuracao = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");
            return connectionString;
        }
    }
}