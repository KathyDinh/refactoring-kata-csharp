using System;
using System.Collections.Generic;

namespace RefactoringKata
{
    public class Product
    {
        public string code { get; set; }
        public Color color { get; set; }
        public Size size { get; set; }
        public double price { get; set; }
        public string currency { get; set; }

        public Product(string code, int color, int size, double price, string currency)
        {
            this.code = code;
            this.color = typeof(Color).IsEnumDefined(color) ? (Color) color : Color.unknown;
            this.size = typeof(Size).IsEnumDefined(size) ? (Size) size : Size.Invalid;
            this.price = price;
            this.currency = currency;
        }

        public string GetSize()
        {
            if (size <= 0)
            {
                return "Invalid Size";
            }
            return size.ToString();
        }

        public string GetColor()
        {
            if (color <= 0)
            {
                return "no color";
            }
            return color.ToString();
        }

        public bool ShouldSerializesize()
        {
            return size != Size.NotApplicable;
        }

        public Dictionary<string, object> GetContent()
        {
            var content = new Dictionary<string, object>();
            content.Add("code", code);
            content.Add("color", GetColor());

            if (ShouldSerializesize())
            {
                content.Add("size", GetSize());
            }

            content.Add("price", price);
            content.Add("currency", currency);
            return content;
        }
    }
}
