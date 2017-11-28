using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    class PresenterTest
    {
        [Test]
        public void ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();

            Presenter presenter = new Presenter(mockView);

            mockView.Loaded += Raise.Event<Action>();

            mockView.Received()
                .Render(Arg.Is<String>(s => s.Contains("Hello World")));
        }
    }
}
