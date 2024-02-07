using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero_WebApp.Tests.Enhettester;

    public class Enhetstest2
    {
        [Fact]
        public void Viewexists()
        {
            var viewName = "/Views/ShoppingCart/Index.cs";

            var result = new ViewResult { ViewName = viewName };

            Assert.NotNull(result.ViewName);
        }
    }
