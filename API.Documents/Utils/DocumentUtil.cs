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

        public static DomaineType GetDocumentDomaine(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.VenteDevis => DomaineType.VENTE,
                DocumentType.VenteCommande => DomaineType.VENTE,
                DocumentType.VentePrepaLivraison => DomaineType.VENTE,
                DocumentType.VenteLivraison => DomaineType.VENTE,
                DocumentType.VenteRetour => DomaineType.VENTE,
                DocumentType.VenteAvoir => DomaineType.VENTE,
                DocumentType.VenteFacture => DomaineType.VENTE,
                DocumentType.VenteFactureRetour => DomaineType.VENTE,
                DocumentType.VenteFactureAvoir => DomaineType.VENTE,

                DocumentType.AchatDemande => DomaineType.ACHAT,
                DocumentType.AchatPrepaCommande => DomaineType.ACHAT,
                DocumentType.AchatCommande => DomaineType.ACHAT,
                DocumentType.AchatLivraison => DomaineType.ACHAT,
                DocumentType.AchatRetour => DomaineType.ACHAT,
                DocumentType.AchatAvoir => DomaineType.ACHAT,
                DocumentType.AchatFacture => DomaineType.ACHAT,
                DocumentType.AchatFactureRetour => DomaineType.ACHAT,
                DocumentType.AchatFactureAvoir => DomaineType.ACHAT,

                DocumentType.StockEntree => DomaineType.STOCK,
                DocumentType.StockSortie => DomaineType.STOCK,
                DocumentType.StockTransfert => DomaineType.STOCK,
                _ => throw new Exception("Invalid domaine type")
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
