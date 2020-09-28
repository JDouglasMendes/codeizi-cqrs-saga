using System.Collections.Generic;

namespace Codeizi.CQRS.Saga.FunctionalTest.ViewModels
{
    public class InitialStateCheckoutViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public CustomerViewModel CustomerViewModel { get; set; }
    }
}