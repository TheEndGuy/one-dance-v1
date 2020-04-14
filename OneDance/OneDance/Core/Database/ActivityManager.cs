using Microsoft.Kinect;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OneDance.Core.Database.Models;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace OneDance.Core.Database
{
    public class ActivityManager
    {
        private readonly Dictionary<int, PositionTemplate> m_templates = new Dictionary<int, PositionTemplate>();

        private readonly string LoadScript = "SELECT MO.Id AS 'MovimentoId'," +
                                                    "MO.Nome AS 'MovimentoNome'," +
                                                    "MO.Descricao AS 'MovimentoDescricao'," +
                                                    "MO.MargemMaxima," +
                                                    "MO.MargemMinima," +
                                                    "MO.Tipo," +
                                                    "AR.Id AS 'IdArticulacao'," +
                                                    "AR.Nome AS 'ArticulacaoNome' " +
                                             "FROM movimento_articulacao AS MA " +
                                             "INNER JOIN movimento AS MO ON MO.Id = MA.IdMovimento " +
                                             "LEFT JOIN articulacao AS AR ON MA.IdArticulacao = AR.Id " +
                                             "ORDER BY MO.Id";

        private ActivityManager()
        {
            LoadDatabase();
        }

        private static ActivityManager m_activityManager;

        public static ActivityManager Instance
        {
            get { return m_activityManager ?? (m_activityManager = new ActivityManager()); }
        }

        private static MySqlConnection Database
        {
            get;
            set;
        }

        public string GetPositionPictogram(MovimentosEnum positionId)
        {
            return m_templates.Values.FirstOrDefault(x => x.MovementId == positionId).PictogramDirectory;
        }

        public PositionTemplate GetPosition(MovimentosEnum positionId)
        {
            return m_templates.Values.FirstOrDefault(x => x.MovementId == positionId);
        }

        public MovimentosEnum GetMovement(string movementImag)
        {
            return m_templates.Values.FirstOrDefault(x => x.PictogramDirectory.Equals(movementImag)).MovementId;
        }

        public List<PositionTemplate> FilterList(List<JointType> joints)
        {
            List<PositionTemplate> list = new List<PositionTemplate>();

            foreach (var @value in m_templates)
            {
                if (joints.Any(x => @value.Value.Model.Articulacoes.Any(entry => entry.Id == (int)x)))
                    continue;

                list.Add(@value.Value);
            }

            return list;
        }

        private bool LoadDatabase()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Configuration.CONFIGURATION_FILE;

            if (!File.Exists(directory))
                return false;

            DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration();
            databaseConfiguration = JsonConvert.DeserializeObject<DatabaseConfiguration>(File.ReadAllText(directory));

            if (databaseConfiguration.Database == null)
                return false;

            Database = new MySqlConnection(databaseConfiguration.ToString());

            try
            {
                LoadPositions();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar o banco de dados.");
            }

            return true;
        }

        public void LoadPositions()
        {
            var command = Database.CreateCommand();
            List<MovimentoModel> m_movimentList = new List<MovimentoModel>();

            try
            {
                if (Database.State != ConnectionState.Open)
                    Database.Open();

                command.CommandText = LoadScript;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader == null)
                        return;

                    while (reader.Read())
                    {
                        var positionId = Convert.ToInt32(reader["MovimentoId"]);

                        if (!m_templates.ContainsKey(positionId))
                        {
                            var moviment = new MovimentoModel(reader);
                            m_templates.Add(positionId, new PositionTemplate(moviment));
                        }

                        m_templates[positionId].Model.Articulacoes.Add(new ArticulacaoModel(reader));
                    }

                    m_templates.Values.ToList().ForEach(x => x.CreateJoints());
                }
            }

            finally
            {
                if (Database.State == ConnectionState.Open)
                    Database.Close();
            }
        }
    }
}
