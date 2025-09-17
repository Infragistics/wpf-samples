using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for parsing XElement to <see cref="Order"/> Order and <see cref="OrderDetail"/> objects
    /// </summary>
    public static class XElementOrderExtension
    {
        /// <summary>
        /// Returns a list of <see cref="Infragistics.Samples.Shared.Models.OrderDetail"/> objects from XElement 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static IEnumerable<OrderDetail> GetOrderDetails(this XElement parent)
        {
            return (from d in parent.Descendants("OrderDetails")
                    select new OrderDetail
                    {
                        ProductName = d.Element("ProductName").GetString(),
                        Quantity = d.Element("Quantity").GetInt(),
                        Discount = d.Element("Discount").GetDecimal()
                    }).ToList<OrderDetail>();
        }

        /// <summary>
        /// Returns a list of <see cref="Infragistics.Samples.Shared.Models.Order"/> objects from XElement 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static IEnumerable<Order> GetOrders(this XElement parent)
        {
            return (from d in parent.Descendants("Orders")
                    select new Order
                    {
                        OrderDate = d.Element("OrderDate").GetDateTime(),
                        ShippedDate = d.Element("ShippedDate").GetDateTime(),
                        Freight = d.Element("Freight").GetDecimal(),
                        ShipName = d.Element("ShipName").GetString(),
                        ShipAddress = d.Element("ShipAddress").GetString(),
                        ShipCity = d.Element("ShipCity").GetString(),
                        ShipPostalCode = d.Element("ShipPostalCode").GetString(),
                        ShipCountry = d.Element("ShipCountry").GetString(),
                        Quantity = d.Element("Quantity").GetDouble(),
                        OrderDetails = d.GetOrderDetails()
                    }).ToList<Order>();
        }
    }
}