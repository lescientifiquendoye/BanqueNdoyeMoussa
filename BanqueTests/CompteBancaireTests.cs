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
        var cb = new CompteBancaire("Pr Ibrahima Fall", soldeInitial);

        // Act
        cb.Débiter(montantDébit);

        // Assert
        Assert.AreEqual(soldeAttendu, cb.Solde, 0.001, "Compte débité incorrectement");
    }

    [TestMethod]
    public void DébiterMontantNégatifSoulèveApplicationException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Ibrahima Ndom", 500000);

        // Act
        void action() => cb.Débiter(-400000);

        // Assert
        var ex = Assert.Throws<ApplicationException>(action);
        Assert.AreEqual("Le montant retiré doit être Positif", ex.Message);
    }

    [TestMethod]
    public void DébiterMontantSupérieurAuSoldeSoulèveArgumentOutOfRangeException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Mamadou Diop", 100000);

        // Act
        void action() => cb.Débiter(200000);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [TestMethod]
    public void CréditerMontantValideAugmenteSolde()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Awa Ndiaye", 300000);

        // Act
        cb.Créditer(200000);

        // Assert
        Assert.AreEqual(500000, cb.Solde, 0.001);
    }

    [TestMethod]
    public void CréditerMontantNégatifSoulèveArgumentOutOfRangeException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Ousmane Sarr", 300000);

        // Act
        void action() => cb.Créditer(-100000);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [TestMethod]
    public void CréditerMontantZéroSoulèveArgumentOutOfRangeException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Aminata Ba", 300000);

        // Act
        void action() => cb.Créditer(0);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [TestMethod]
    public void DébiterCompteBloquéSoulèveException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Lamine Diallo", 500000);

        // Bloquer le compte via reflection (méthode privée)
        var method = typeof(CompteBancaire)
            .GetMethod("BloquerCompte", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method!.Invoke(cb, null);

        // Act
        void action() => cb.Débiter(10000);

        // Assert
        var ex = Assert.Throws<Exception>(action);
        Assert.AreEqual("Compte bloqué", ex.Message);
    }

    [TestMethod]
    public void CréditerCompteBloquéSoulèveException()
    {
        // Arrange
        var cb = new CompteBancaire("Pr Khady Fall", 500000);

        var method = typeof(CompteBancaire)
            .GetMethod("BloquerCompte", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method!.Invoke(cb, null);

        // Act
        void action() => cb.Créditer(10000);

        // Assert
        var ex = Assert.Throws<Exception>(action);
        Assert.AreEqual("Compte bloqué", ex.Message);
    }
}
