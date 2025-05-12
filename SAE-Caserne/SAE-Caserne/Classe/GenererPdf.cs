using System;
using System.Collections.Generic;
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

        private GenererPdf() {}

        public static void genererPdfTermine(string nomFichier, string id, string missionType, string missionDate, string missionDetails, string caserne, int EtatMission, string compteRendu)
        {
            PdfWriter writer = new PdfWriter(nomFichier);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);


            document.Add(new Paragraph("Détails de la mission").SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph($"ID de la mission : {id}"));
            document.Add(new Paragraph($"Type de mission : {missionType}"));
            document.Add(new Paragraph($"Date de départ : {missionDate}"));
            document.Add(new Paragraph($"Caserne : {caserne}"));
            document.Add(new Paragraph($"État de la mission : {EtatMission}"));
            document.Add(new Paragraph($"Détails : {missionDetails}"));
            document.Add(new Paragraph("Compte-rendu :"));
            document.Add(new Paragraph(compteRendu).SetTextAlignment(TextAlignment.JUSTIFIED));

            document.Close();
        }


        public static void genererPdfEnCours(string nomFichier,string id, string missionType, string missionDate, string missionDetails, string caserne, int EtatMission, string compteRendu)
        {
            PdfWriter writer = new PdfWriter(nomFichier);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);


            document.Add(new Paragraph("Détails de la mission").SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph($"ID de la mission : {id}"));
            document.Add(new Paragraph($"Type de mission : {missionType}"));
            document.Add(new Paragraph($"Date de départ : {missionDate}"));
            document.Add(new Paragraph($"Caserne : {caserne}"));
            document.Add(new Paragraph($"État de la mission : {EtatMission}"));
            document.Add(new Paragraph($"Détails : {missionDetails}"));
            document.Add(new Paragraph("Compte-rendu :"));
            document.Add(new Paragraph(compteRendu).SetTextAlignment(TextAlignment.JUSTIFIED));

            document.Close();
        }


    }
}
