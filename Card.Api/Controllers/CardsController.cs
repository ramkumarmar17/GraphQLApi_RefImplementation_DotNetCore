using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private List<Card> _cards;

        private readonly ILogger<CardsController> _logger;

        public CardsController(ILogger<CardsController> logger)
        {
            _logger = logger;
            _cards = GetCards();
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Card> GetAllCards()
        {
            Console.WriteLine($"GetAllCards: Time: {DateTime.Now.ToString("o")}");
            Thread.Sleep(1000);
            return _cards;
        }

        [HttpGet]
        [Route("{id}")]
        public Card GetCard(string id)
        {
            Console.WriteLine($"GetCard: Time: {DateTime.Now.ToString("o")}");
            Thread.Sleep(1000);
            var card = _cards.FirstOrDefault(c => c.Id.Equals(id));
            return card ?? new Card();
        }

        private List<Card> GetCards()
        {
            return new List<Card> {
                new Card {
                    Id = "111",
                    Number = "110011001100",
                    FullName = "Raj",
                    ValidFrom = DateTime.Now.AddMonths(-12),
                    ValidTo = DateTime.Now.AddYears(2),
                    CVV = 111,
                    Type = "Master"
                },
                new Card {
                    Id = "222",
                    Number = "220022002200",
                    FullName = "Vijay",
                    ValidFrom = DateTime.Now.AddMonths(-1),
                    ValidTo = DateTime.Now.AddMonths(23),
                    CVV = 222,
                    Type = "Visa"
                },
                new Card {
                    Id = "333",
                    Number = "330033003300",
                    FullName = "Charan",
                    ValidFrom = DateTime.Now.AddMonths(-24),
                    ValidTo = DateTime.Now.AddMonths(12),
                    CVV = 333,
                    Type = "Discover"
                }
            };
        }
    }
}
