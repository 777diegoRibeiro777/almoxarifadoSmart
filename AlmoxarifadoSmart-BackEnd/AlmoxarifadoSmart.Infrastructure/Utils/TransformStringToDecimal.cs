using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Infrastructure.Utils;

public abstract class TransformStringToDecimal
{

    public static decimal StringToDecimal(string price)
    {
        CultureInfo cultureInfo = new CultureInfo("pt-BR");

        string cleanedPrice = price.Replace("R$", "").Trim();

        decimal result = decimal.Parse(cleanedPrice, NumberStyles.Currency, cultureInfo);

        return result;
    }
}
