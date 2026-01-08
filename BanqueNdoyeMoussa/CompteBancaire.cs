namespace Banque
{
    public class CompteBancaire
    {
        private readonly string _nomClient;
        private bool _bloque = false;

        private CompteBancaire() { }

        public CompteBancaire(string nomClient, double solde)
        {
            _nomClient = nomClient;
            Solde = solde;
        }

        public double Solde { get; private set; }

        public void Débiter(double montant)
        {
            if (_bloque)
            {
                throw new Exception("Compte bloqué");
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(montant, other: Solde);

            if (montant < 0)
            {
                throw new ApplicationException("Le montant retiré doit être Positif");
            }

            Solde -= montant; // code intentionnellement faux
        }

        public void Créditer(double montant)
        {
            if (_bloque)
            {
                throw new Exception("Compte bloqué");
            }
            
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            Solde += montant;
        }

        private void BloquerCompte()
        {
            _bloque = true;
        }

        private void DébloquerCompte()
        {
            _bloque = false;
        }

        public static void Main()
        {
            var cb = new CompteBancaire(nomClient: "Pr Mamadou Samba Camara", solde: 500000);
            cb.Créditer(montant: 100000);
            cb.Débiter(montant: 50000);
            Console.WriteLine($"Solde disponible : {cb.Solde}");
        }
    }
}