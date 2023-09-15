namespace API.Documents.Enums
{
    public enum DocumentType
    {
        VenteDevis = 0,
        VenteCommande = 1,
        VentePrepaLivraison = 2,
        VenteLivraison = 3,
        VenteRetour = 4,
        VenteAvoir = 5,
        VenteFacture = 6,
        VenteFactureRetour = 7,
        VenteFactureAvoir = 8,

        AchatDemande = 9,
        AchatPrepaCommande = 10,
        AchatCommande = 11,
        AchatLivraison = 12,
        AchatRetour = 13,
        AchatAvoir = 14,
        AchatFacture = 15,
        AchatFactureRetour = 16,
        AchatFactureAvoir = 17,

        StockEntree = 18,
        StockSortie = 19,
        StockTransfert = 20
    }
}
