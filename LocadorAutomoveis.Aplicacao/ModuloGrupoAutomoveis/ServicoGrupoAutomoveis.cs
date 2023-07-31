﻿using LocadorAutomoveis.Dominio.ModuloGrupoAutomoveis;

namespace LocadorAutomoveis.Aplicacao.ModuloGrupoAutomoveis
{
    public class ServicoGrupoAutomoveis
    {
        private IRepositorioGrupoAutomoveis repositorioGrupoAutomoveis;
        private IValidadorGrupoAutomoveis validadorGrupoAutomoveis;

        public ServicoGrupoAutomoveis(
            IRepositorioGrupoAutomoveis repositorioGrupoAutomoveis,
            IValidadorGrupoAutomoveis validadorGrupoAutomoveis)
        {
            this.repositorioGrupoAutomoveis = repositorioGrupoAutomoveis;
            this.validadorGrupoAutomoveis = validadorGrupoAutomoveis;
        }

        public Result Inserir(GrupoAutomoveis grupo)
        {
            Log.Debug("Tentando inserir grupoAutomóveis...{@g}", grupo);

            List<string> erros = ValidarGrupoAutomoveis(grupo);

            if (erros.Count() > 0)
                return Result.Fail(erros); //cenário 2

            try
            {
                repositorioGrupoAutomoveis.Inserir(grupo);

                Log.Debug("GrupoAutomóveis {GrupoAutomoveisId} inserido com sucesso", grupo.Id);

                return Result.Ok(); //cenário 1
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir grupoAutomóveis.";

                Log.Error(exc, msgErro + "{@g}", grupo);

                return Result.Fail(msgErro); //cenário 3
            }
        }
        
        public Result Editar(GrupoAutomoveis grupo)
        {
            Log.Debug("Tentando editar grupoAutomóveis...{@g}", grupo);

            List<string> erros = ValidarGrupoAutomoveis(grupo);

            if (erros.Count() > 0)
                return Result.Fail(erros);

            try
            {
                repositorioGrupoAutomoveis.Editar(grupo);

                Log.Debug("GrupoAutomóveis {GrupoAutomoveisId} editado com sucesso", grupo.Id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar grupoAutomóveis.";

                Log.Error(exc, msgErro + "{@g}", grupo);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(GrupoAutomoveis grupo)
        {
            Log.Debug("Tentando excluir grupoAutomóveis...{@g}", grupo);

            try
            {
                bool grupoExiste = repositorioGrupoAutomoveis.Existe(grupo);

                if (grupoExiste == false)
                {
                    Log.Warning("GrupoAutomóveis {GrupoAutomoveisId} não encontrado para excluir", 
                        grupo.Id);

                    return Result.Fail("GrupoAutomóveis não encontrado");
                }

                repositorioGrupoAutomoveis.Excluir(grupo);

                Log.Debug("GrupoAutomóveis {GrupoAutomoveisId} excluído com sucesso", grupo.Id);

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                List<string> erros = new List<string>();
              
                string msgErro;

                msgErro = "Falha ao tentar excluir grupoAutomóveis";

                erros.Add(msgErro);

                Log.Error(ex, msgErro + " {GrupoAutomoveisId}", grupo.Id);

                return Result.Fail(erros);
            }
        }

        private List<string> ValidarGrupoAutomoveis(GrupoAutomoveis grupo)
        {
            var resultadoValidacao = validadorGrupoAutomoveis.Validate(grupo);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (TipoDuplicado(grupo))
                erros.Add($"Este tipo '{grupo.Tipo}' já está sendo utilizado");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool TipoDuplicado(GrupoAutomoveis grupo)
        {
            GrupoAutomoveis grupoEncontrado = repositorioGrupoAutomoveis.SelecionarPorTipo(grupo.Tipo);

            if (grupoEncontrado != null &&
                grupoEncontrado.Id != grupo.Id &&
                grupoEncontrado.Tipo == grupo.Tipo)
            {
                return true;
            }

            return false;
        }

    }
}