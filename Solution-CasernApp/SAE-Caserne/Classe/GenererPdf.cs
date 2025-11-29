using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace SAE_Caserne.Classe
{
    internal class GenererPdf
    {

        private GenererPdf() { }

        public static void genererPdfTermine(string nomFichier, string id, string missionType, string missionDate, string missionDetails, string caserne, int EtatMission, string compteRendu, List<string> pompiers,string heureRetour,string motifAppel,string adresse, string ville, List<string> engins)
        {
            PdfWriter writer = new PdfWriter(nomFichier);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            document.Add(new Paragraph("Rapport de mission").SetFontSize(26).SetTextAlignment(TextAlignment.CENTER));

            document.Add(new Paragraph($"Date de départ : {missionDate}"));
            document.Add(new Paragraph($"Date de retour : {heureRetour}"));
            document.Add(new Paragraph(".........................................................................................................."));
            document.Add(new Paragraph($"Type de mission : {missionType}\n\n").SetFontSize(22));
            document.Add(new Paragraph($"Motif : {missionDetails}\n"));
            document.Add(new Paragraph($"Adresse : {adresse}, {ville}\n"));
            document.Add(new Paragraph($"Compte-rendu : {compteRendu}"));
            document.Add(new Paragraph(".........................................................................................................."));

            document.Add(new Paragraph($"Caserne : {caserne}\n\n"));


            document.Add(new Paragraph("Pompiers affectés :").SetFontSize(22));
            foreach (string pompier in pompiers)
            {
                document.Add(new Paragraph("-->" + pompier));
            }
            document.Add(new Paragraph("\n\n\n"));
            document.Add(new Paragraph("Engins affectés :\n").SetFontSize(22));
            foreach (string engin in engins)
            {
                document.Add(new Paragraph("-->" + engin));
            }
            document.Close();
        }


        public static void genererPdfEnCours(string nomFichier, string id, string missionType, string missionDate, string missionDetails, string caserne, int EtatMission, string compteRendu, List<string> pompiers, string heureRetour, string motifAppel, string adresse, string ville, List<string> engins)
        {
            PdfWriter writer = new PdfWriter(nomFichier);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            document.Add(new Paragraph("Rapport de mission").SetFontSize(26).SetTextAlignment(TextAlignment.CENTER));

            document.Add(new Paragraph($"Date de départ : {missionDate}"));
            document.Add(new Paragraph($"Date de retour : missions encore en cours"));
            document.Add(new Paragraph(".........................................................................................................."));
            document.Add(new Paragraph($"Type de mission : {missionType}\n\n").SetFontSize(22));
            document.Add(new Paragraph($"Motif : {missionDetails}\n"));
            document.Add(new Paragraph($"Adresse : {adresse}, {ville}\n"));
            document.Add(new Paragraph(".........................................................................................................."));

            document.Add(new Paragraph($"Caserne : {caserne}\n\n"));


            document.Add(new Paragraph("Pompiers affectés :").SetFontSize(22));
            foreach (string pompier in pompiers)
            {
                document.Add(new Paragraph("-->" + pompier));
            }
            document.Add(new Paragraph("\n\n\n"));
            document.Add(new Paragraph("Engins affectés :\n").SetFontSize(22));
            foreach (string engin in engins)
            {
                document.Add(new Paragraph("-->" + engin));
            }
            document.Close();
        }

       
    }
}
