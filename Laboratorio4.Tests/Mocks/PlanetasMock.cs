using Laboratorio4.Models;
using Laboratorio4.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
namespace UnitTestLab7.Mocks
{
    [TestClass]
    public class PlanetasMock
    {
        [TestMethod]
        public void AgregarMultiplesPlanetasVariosUsuarios()
        {
            ThreadStart usuario1 = new
           ThreadStart(SimulacionUsuarioCreandoPlanetas);
            ThreadStart usuario2 = new
           ThreadStart(SimulacionUsuarioCreandoPlanetas);
            Thread hilo1 = new Thread(usuario1);
            Thread hilo2 = new Thread(usuario2);
            hilo1.Start();
            hilo2.Start();
            hilo1.Join();
            hilo2.Join();
        }
        public void SimulacionUsuarioCreandoPlanetas()
        {
            MyTestPostedFileBase archivoTest = new MyTestPostedFileBase(new
            System.IO.MemoryStream(), "/test.file", "TestFile");
            PlanetaModel planeta = new PlanetaModel
            {
                nombre = "Test-planeta",
                numeroAnillos = 100,
                archivo = archivoTest,
                tipo = "De prueba"
            };
            PlanetaHandler db = new PlanetaHandler();
            bool exitoAlCrear = false;
            for (int intento = 0; intento < 10; ++intento)
            {
                //Act
                exitoAlCrear = db.crearPlaneta(planeta);
                //Assert
                Assert.IsTrue(exitoAlCrear);
            }
        }
    }
}
