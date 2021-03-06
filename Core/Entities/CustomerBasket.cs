using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {       
        }

        public CustomerBasket(string id) 
        {
            Id = id;
        }

        //on ne derive pas la class car c coté client redis creer le panier pour ID
        public string Id { get; set; }
        // chaque nouveau panier est vide
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        //for payment
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}