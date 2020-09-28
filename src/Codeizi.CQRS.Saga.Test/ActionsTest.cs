using System;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace Codeizi.CQRS.Saga.Test
{
    public class ActionsTest
    {
        [Fact]
        public void Add_actions_in_Saga()
        {
            
        }

        [Fact]
        public void Add_property_in_state()
        {


            var id = Guid.NewGuid();
            var x = new ExpandoObject();
            x.TryAdd("id", id);
            dynamic y = x;
            Assert.Equal(id, y.id);
        }
    }
    

}
