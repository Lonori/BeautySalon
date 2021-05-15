using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon
{
    class Crutches
    {
        public static void UpdateMaterialRegister(OleDbConnection DbConnection)
        {
            Dictionary<string, float> material_sum = new Dictionary<string, float>();
            using (OleDbCommand command = new OleDbCommand("SELECT `material_id`,SUM(`amount`) FROM `materials_arrival` GROUP BY `material_id`", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    material_sum.Add(reader[0].ToString(), float.Parse(reader[1].ToString()));
                }
            }

            using (OleDbCommand command = new OleDbCommand("SELECT `material_id`,SUM(`amount`) FROM `materials_consumption` GROUP BY `material_id`", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    material_sum[reader[0].ToString()] -= float.Parse(reader[1].ToString());
                }
            }

            foreach (KeyValuePair<string, float> keyValue in material_sum)
            {
                new OleDbCommand("UPDATE `materials` SET `amount`=" + keyValue.Value.ToString() + " WHERE `id`=" + keyValue.Key, DbConnection).ExecuteNonQuery();
            }
        }
    }
}
