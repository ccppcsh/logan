using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class Presenter
    {
        private readonly IView _view;
        public Presenter(IView view)
        {
            _view = view;
            this._view.Loaded += OnLoaded;
        }
        private void OnLoaded()
        {
            _view.Render("Hello World");
        }
    }

    public interface IView
    {
        event Action Loaded;
        void Render(string text);
    }
}
