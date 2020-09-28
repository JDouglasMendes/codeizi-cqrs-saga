using System;

namespace Codeizi.CQRS.Saga.FunctionalTest.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}