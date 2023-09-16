using API.Documents.Enums;

namespace API.Documents.Utils
{
    public class DocumentUtil
    {
        public static string GetDocumentNumberPrefix(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.VenteDevis => "VDE",
                DocumentType.VenteCommande => "VBC",
                DocumentType.VentePrepaLivraison => "VPL",
                DocumentType.VenteLivraison => "VBL",
                DocumentType.VenteRetour => "VBR",
                DocumentType.VenteAvoir => "VBV",
                DocumentType.VenteFacture => "VFA",
                DocumentType.VenteFactureRetour => "VFR",
                DocumentType.VenteFactureAvoir => "VFV",

                DocumentType.AchatDemande => "ADA",
                DocumentType.AchatPrepaCommande => "APC",
                DocumentType.AchatCommande => "ABC",
                DocumentType.AchatLivraison => "ABL",
                DocumentType.AchatRetour => "ABR",
                DocumentType.AchatAvoir => "ABV",
                DocumentType.AchatFacture => "AFA",
                DocumentType.AchatFactureRetour => "AFR",
                DocumentType.AchatFactureAvoir => "AFV",

                DocumentType.StockEntree => "SME",
                DocumentType.StockSortie => "SMS",
                DocumentType.StockTransfert => "SMT",
                _ => throw new Exception("Invalid document type")
            };
        }

        public static string IncrementDocumentNumber(string? lastDocumentNumber)
        {
            if (string.IsNullOrEmpty(lastDocumentNumber))
                return "000000001";

            if (!int.TryParse(lastDocumentNumber, out var lastNumber))
                throw new Exception("Unable to increment document number");

            lastNumber++;
            string newDocumentNumber = lastNumber.ToString();
            while (newDocumentNumber.Length < 9)
                newDocumentNumber = "0" + newDocumentNumber;

            return newDocumentNumber;
        }
    }
}
