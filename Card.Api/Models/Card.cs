using System;

namespace CardApi
{
    public class Card
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string FullName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int CVV { get; set; }
        public string Type { get; set; }
    }
}
