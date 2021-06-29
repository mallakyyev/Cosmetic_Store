using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetics.Web.Areas.Admin.Utilits
{
    public static class API
    {
        public static string GetAllUsers { get; } = "/api/AccountAPI";

        public static string GetAllCategories { get; } = "/api/CategoryAPI";

        public static string GetAllManufacturies { get; } = "/api/ManufactureAPI";

        public static string GetAllCurrencies { get; } = "/api/CurrencyAPI";

        public static string GetAllLanguages { get; } = "/api/LanguageAPI";

        public static string GetAllCountries { get; } = "/api/CountryAPI";

        public static string GetAllProducts { get; } = "/api/ProductAPI";

        public static string GetAllOrders { get; } = "/api/CartAPI";

        public static string GetOrdersDetail { get; } = "/api/CartAPI/GetOrdersDetail";

    }
}
