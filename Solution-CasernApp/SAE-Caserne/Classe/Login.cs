using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pinpon;

namespace SAE_Caserne.Classe
{
    internal class Login
    {

        public Login() { }

        public int getIDMax()
        {
            string requete = "SELECT MAX(id) FROM Admin";
            SQLiteConnection connection = Connexion.Connec;
            SQLiteCommand command = new SQLiteCommand(requete, connection);
            int idMax = Convert.ToInt16(command.ExecuteScalar());
            //MessageBox.Show("DEBUG : ID max récupéré : " + idMax);
            return idMax;
        }

        public bool verifIdMdp(string identifiant, string mdp)
        {
            bool identifiantsCorrects = false;
            int IDMax = getIDMax();
            SQLiteConnection connection = Connexion.Connec;

            int i = 1;
            //MessageBox.Show("DEBUG : Juste avant le do while, i = " + i);
            do
            {
                string requeteIdentifiant = "SELECT login FROM Admin WHERE id = " + i;
                string requeteMDP = "SELECT mdp FROM Admin WHERE id = " + i;


                SQLiteCommand commandIdentifiant = new SQLiteCommand(requeteIdentifiant, connection);
                string idRequete = (commandIdentifiant.ExecuteScalar()).ToString();
                //MessageBox.Show("DEBUG : Juste avant le if identifiant. Identifiant attendu : " + idRequete + "\t Identifiant reçu : " + identifiant);
                if (idRequete == identifiant)
                {
                    SQLiteCommand commandMDP = new SQLiteCommand(requeteMDP, connection);
                    string mdpRequete = (commandMDP.ExecuteScalar()).ToString();
                    //MessageBox.Show("DEBUG : Juste avant le if MDP. MDP attendu : " + mdpRequete + "\t MDP reçu : " + mdp);
                    if (mdpRequete == mdp)
                    {
                        identifiantsCorrects = true;

                        //MessageBox.Show("DEBUG : Identifiant attendu : " + idRequete + "\t Identifiant reçu : " + identifiant + "\n MDP Attendu : " + mdpRequete + "MDP reçu : "+ mdp);
                    }
                }

                i++;

            } while (i <= IDMax && identifiantsCorrects == false);

            return identifiantsCorrects;
        }





    }
}
