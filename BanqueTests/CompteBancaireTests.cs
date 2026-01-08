using Banque;
namespace BanqueTests;

[TestClass]
public sealed class CompteBancaireTests
{
    [TestMethod]
    public void VérifierDébitCompteCorrect()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantDébit = 400000;
        const double soldeAttendu = 100000;
        var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

        // Act
        cb.Débiter(montantDébit);

        // Assert
        Assert.AreEqual(soldeAttendu, cb.Solde, delta: 0.001, message: "Compte débité incorrectement");
    }

    [TestMethod()]
    public void DébiterMontantNégatifSoulèveApplicationException()
    {
        //Arrange
        const double soldeInitial = 500000;
        const double montantDébit = -400000;
        var cb = new CompteBancaire(nomClient: "Pr Ibrahima N6om", soldeInitial);

        // Act
        void Action() => cb.Débiter(montantDébit);

        // Assert
        var ex = Assert.Throws<ApplicationException>(Action);
        Assert.AreEqual("Le montant retiré doit être Positif", ex.Message);
    }
}
