using System;
using System.Collections.Generic;

namespace RefactoringKata
{
    public enum Color
    {
        unknown = 0,
        blue = 1,
        red,
        yellow
    }

    public enum Size
    {
        NotApplicable = -1,
        Invalid = 0,
        XS = 1,
        S,
        M,
        L,
        XL,
        XXL
    }

    public class Product
    {
        public static int SIZE_NOT_APPLICABLE = -1;

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
            if ((int)size <= 0)
            {
                return "Invalid Size";
            }
            return size.ToString();
        }

        public string GetColor()
        {
            if ((int)color <= 0)
            {
                return "no color";
            }
            return color.ToString();
        }

        public Boolean ShoudlSerializesize
        {
            get { return (int)size != SIZE_NOT_APPLICABLE; }
        }

        public Dictionary<string, object> GetContent()
        {
            var content = new Dictionary<string, object>();
            content.Add("code", code);
            content.Add("color", GetColor());

            if ((int)size != SIZE_NOT_APPLICABLE)
            {
                content.Add("size", GetSize());
            }

            content.Add("price", price);
            content.Add("currency", currency);
            return content;
        }
    }
}
