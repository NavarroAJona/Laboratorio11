using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace WebApplication2.Tests.MoqExamples
{
    public interface Calculadora
    {
        int multiplicar(int numero1, int numero2);
    }
    public class CalculadoraController : Controller {

        protected Calculadora calcu;
        public CalculadoraController(Calculadora calcu) {
            this.calcu = calcu;
        }

        public ActionResult RedireccionarCalculo(int numero1, int numero2) {

            var resultado = this.calcu.multiplicar(numero1, numero2);
            if (resultado < 10)
            {
                return RedirectToAction("disminiuirValorMultiplicacion", "Calculadora", new { valorADisminiuir = resultado });
            }
            else {
                return RedirectToAction("aumentarValorMultiplicacion", "Calculadora", new { valorAAumentar = resultado });
            }
        
        }
    }




    namespace Laboratorio4.Tests.MoqExamples
    {
        [TestClass]
        public class Ejemplo3
        {
            [TestMethod]
            public void ProbarMultiplicacionEnDisminiuir()
            {
                var mock = new Mock<Calculadora>();
                mock.Setup(calcu => calcu.multiplicar(4, 6)).Returns(24);
                var calculadoraController = new CalculadoraController(mock.Object);
                ActionResult vista = calculadoraController.RedireccionarCalculo(4, 6);
                Assert.AreEqual("RedirectToRouteResult", vista.GetType().Name);
        }
        }
    }
}