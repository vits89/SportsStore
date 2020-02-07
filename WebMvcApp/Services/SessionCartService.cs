using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.AppCore.Entities;
using SportsStore.AppCore.Services;
using SportsStore.WebMvcApp.Infrastructure;

namespace SportsStore.WebMvcApp.Services
{
    public class SessionCartService : CartService
    {
        private const string SESSION_KEY_NAME = "Cart";

        [JsonIgnore]
        public ISession Session { get; set; }

        public static CartService GetCartService(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var cartService = session?.GetJson<SessionCartService>(SESSION_KEY_NAME) ?? new SessionCartService();

            cartService.Session = session;

            return cartService;
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
