using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringLSCC
{
    public class Order
    {
        public Customer Customer { get; set; }
        public int Total { get; set; }
        public int Weight { get; set; } 
    }

    public class Customer
    {
        public string Status { get; set; }
    }

    //public class ComplexConditional
    //{
    //    public double GetShippingCost(Order order)
    //    {
    //        double shipping;
    //        if (order.Customer.Status == "P" || (order.Total > 1000 && order.Weight < 500))
    //        {
    //            shipping = order.Total * 0.05;
    //            if (shipping > 100) shipping = 100;
    //        }
    //        else
    //        {
    //            shipping = order.Total * .08;
    //            if (shipping > 200) shipping = 200;
    //        }
    //        return shipping;
    //    }
    //}

    //public class ComplexConditional
    //{
    //    public double GetShippingCost(Order order)
    //    {
    //        double shipping;
    //        if (LargeOrImportantOrder(order))
    //        {
    //            shipping = order.Total * 0.05;
    //            if (shipping > 100) shipping = 100;
    //        }
    //        else
    //        {
    //            shipping = order.Total * .08;
    //            if (shipping > 200) shipping = 200;
    //        }
    //        return shipping;
    //    }

    //    bool LargeOrImportantOrder(Order order)
    //    {
    //        return order.Customer.Status == "P" || (order.Total > 1000 && order.Weight < 500);
    //    }
    //}

    public class ComplexConditional
    {
        public double GetShippingCost(Order order)
        {
            double shipping;
            if (LargeOrImportantOrder(order))
                shipping = DiscountShippingRate(order);
            else
                shipping = NormalShippingRate(order);
            return shipping;
        }

        double NormalShippingRate(Order order)
        {
            var shipping = order.Total * .08;
            if (shipping > 200) shipping = 200;
            return shipping;
        }

        double DiscountShippingRate(Order order)
        {
            var shipping = order.Total * 0.05;
            if (shipping > 100) shipping = 100;
            return shipping;
        }

        bool LargeOrImportantOrder(Order order)
        {
            return order.Customer.Status == "P" || (order.Total > 1000 && order.Weight < 500);
        }
    }
}
