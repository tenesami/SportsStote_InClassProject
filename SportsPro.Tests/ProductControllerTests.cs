using CSC237_tatomsa_InClassProject.Controllers;
using CSC237_tatomsa_InClassProject.DataLayer;
using CSC237_tatomsa_InClassProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace SportsPro.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void List_ReturnsAViewResult()
        {
            //array
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.List(It.IsAny<QueryOptions<Product>>())).Returns (new List<Product>());
            var controller = new ProductController(rep.Object);

            //Act
            var result = controller.List();

            //Assret
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_ModelIsACollectionOfPrducts()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny <QueryOptions<Product>>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            //act
            var model = controller.List().ViewData.Model as IEnumerable<Product>;

            //assert
            Assert.IsType<List<Product>>(model);
        }
        [Fact]
        public void Add_GET_ModelISAsProductObject()
        {
            //Arrang
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            //act
            var model = controller.Add().ViewData.Model as Product;

            //Assert
            Assert.IsType<Product>(model);
        }
            
        [Fact]
        public void Add_GET_ValueOfViewBagActionPropertyIsAdd()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);
            string expected = "Add";

            //act 
            ViewResult result = controller.Add();

            //Assert
            Assert.Equal(expected, result.ViewData["Action"]?.ToString());
        }

        [Fact]
        public void Edit_GET_ModelIsProductObject()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            //act
            var model = controller.Edit(1).ViewData.Model as Product;

            //assert
            Assert.IsType<Product>(model);
        }

        [Fact]
        public void Edit_GET_ValueOfViewBagActionPropertyIsEdit()
        {
            //arrage
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);
            string expected = "Edit";

            //act
            ViewResult result = controller.Edit(1);

            //assert
            Assert.Equal(expected, result.ViewData["action"]?.ToString());
        }
        [Fact]
        public void Save_ReturnsViewResultIfModelStateIsInvalid()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            controller.ModelState.AddModelError("", "Error");
            var productToSave = new Product();

            //act
            var result = controller.Save(productToSave);

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Save_ReturnsRedirectToActionResultIfModelStateIsValid()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {            
                TempData = temp.Object
            };
            var productToSave = new Product();      

            //act
            var result = controller.Save(productToSave);

            //assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Save_RedirectsToActionMethodOnSuccess()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {
                TempData = temp.Object
            };
            var productToSave = new Product();
            string expected = "List";

            //act
            var result = (RedirectToActionResult)controller.Save(productToSave);

            //assert
            Assert.Equal(expected, result.ActionName);
        }

        [Fact]
        public void Delete_GET_ReturnsAViewResult()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            //act
            var result = controller.Delete(1);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_GET_ModelIsAProductObject()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            //act
            var model = controller.Delete(1).ViewData.Model as Product;

            //assert
            Assert.IsType<Product>(model);
        }

        [Fact]
        public void Delet_POST_ReturnsAsRedirectToActionResult()
        {
            //arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {
                TempData = temp.Object
            };
            var productToDelete = new Product();

            //Act
            var result = controller.Delete(productToDelete);

            //assert
            Assert.IsType<RedirectToActionResult>(result);

        }

    }
}
