using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
    public class SessionCart : Cart
    {
        private const string SESSION_KEY_NAME = "Cart";

        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var cart = session?.GetJson<SessionCart>(SESSION_KEY_NAME) ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);

            Session.SetJson(SESSION_KEY_NAME, this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);

            Session.SetJson(SESSION_KEY_NAME, this);
        }

        public override void Clear()
        {
            base.Clear();

            Session.Remove(SESSION_KEY_NAME);
        }
    }
}
