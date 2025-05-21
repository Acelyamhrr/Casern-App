using Pinpon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_Caserne.Classe
{
    internal class AjouterMission
    {
        public AjouterMission()
        {
        }

        public static void RemplirComboboxNsinistre(ComboBox combo)
        {
            try
            {
                string requete = "SELECT DISTINCT motifAppel FROM Mission";

                using (SQLiteCommand cmd = new SQLiteCommand(requete, Connexion.Connec))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        combo.Items.Clear();

                        while (reader.Read())
                        {
                            combo.Items.Add(reader["motifAppel"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
