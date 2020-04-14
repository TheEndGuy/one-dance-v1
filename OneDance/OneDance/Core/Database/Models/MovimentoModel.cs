namespace OneDance.Core.Database.Models
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class MovimentoModel
    {
        public MovimentoModel(MySqlDataReader reader)
        {
            Articulacoes = new List<ArticulacaoModel>();
            AssignFields(reader);
        }

        public int Id
        {
            get;
            set;
        }

        public string Nome
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public double? MargemMinima
        {
            get;
            set;
        }

        public double? MargemMaxima
        {
            get;
            set;
        }

        public int Tipo
        {
            get;
            set;
        }

        public List<ArticulacaoModel> Articulacoes
        {
            get;
            set;
        }

        private void AssignFields(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["MovimentoId"]);
            Nome = Convert.ToString(reader["MovimentoNome"]);
            Descricao = Convert.ToString(reader["MovimentoDescricao"]);
            MargemMaxima = Convert.ToDouble(reader["MargemMaxima"]);
            MargemMinima = Convert.ToDouble(reader["MargemMinima"]);
            Tipo = Convert.ToInt32(reader["Tipo"]);
        }
    }
}
