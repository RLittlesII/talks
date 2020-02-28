using System;

namespace GoReactive.Services.Orders
{
    public class OrderDetailDto : Dto
    {
        public string Name { get; set; }

        public string DrinkName { get; set; }

        public string Details { get; set; }

        public DateTime OrderTime { get; set; }

        public DrinkSize Size { get; set; }
    }
}