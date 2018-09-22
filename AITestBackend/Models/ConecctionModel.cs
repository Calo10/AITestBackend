using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class ConecctionModel
    {

        #region Singleton

        private static ConecctionModel instance = null;


        public ConecctionModel()
        {
            InitClass();
        }

        public static ConecctionModel GetInstance()
        {
            if (instance == null)
                instance = new ConecctionModel();

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
                instance = null;
        }

        #endregion

        public static MySqlConnection conn = new MySqlConnection(AppManagement.connStr);

        private void InitClass()
        {

        }

    }
}
