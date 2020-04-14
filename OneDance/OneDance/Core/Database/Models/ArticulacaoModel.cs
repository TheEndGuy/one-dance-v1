namespace OneDance.Core.Database.Models
{
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ArticulacaoModel
    {
        public ArticulacaoModel(MySqlDataReader reader)
        {
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

        private void AssignFields(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["IdArticulacao"]);
            Nome = Convert.ToString(reader["ArticulacaoNome"]);
        }
    }
}
