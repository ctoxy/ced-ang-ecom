namespace Core.Entities
{
    public class Client : BaseEntity
    {
        public string N_Cpt { get; set; }
        public string Dept { get; set; }
        public int Champ3 { get; set; }
        public string Nom_Client { get; set; }
        public string Ad1 { get; set; }
        public string Ad2 { get; set; }
        public string Ad3 { get; set; }
        public string Ad4 { get; set; }
        public string Tel { get; set; }
        
        public string Fax { get; set; }
        public string Payeur { get; set; }
        public string Code_postal { get; set; }
        public string Pays { get; set; }
        public int Statut { get; set; }
        public string Clef_Recherche { get; set; }
        public string N_Agrément { get; set; }
        public string Info_Compl { get; set; }
        
    }
}

