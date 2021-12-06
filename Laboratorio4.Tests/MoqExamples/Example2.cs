using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace WebApplication2.Tests.MoqExamples
{
    public interface IDogsUpdate
    {
        bool Update(string breed);
    }
    public class HuskyController : Controller
    {
        protected IDogsUpdate updateDogsService;
        public HuskyController(IDogsUpdate updateDogsService)
        {
            this.updateDogsService = updateDogsService;
        }
        public ActionResult UpdateDog(string breed)
        {
            var result = this.updateDogsService.Update(breed);
            if (result == false)
            {
                return RedirectToAction("RazasFalsas", "Husky", new { raza = breed });
            }
            else {
                return RedirectToAction("RazasVerdaderas", "Husky", new { raza = breed });
            }

        }
    }
    [TestClass]
    public class Example2
    {
        [TestMethod]
        public void TestUpdateDogFromDogsController()
        {
            var huskyServiceMock = new Mock<IDogsUpdate>();
            huskyServiceMock.Setup(huskyService =>
           huskyService.Update("husky")).Returns(false);
            var huskyController = new HuskyController(huskyServiceMock.Object);
            ActionResult vista = huskyController.UpdateDog("husky");
            Assert.AreEqual("RedirectToRouteResult", vista.GetType().Name);

        }
    }
}