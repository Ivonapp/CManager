using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Helpers;

public static class GuidHelper
{
    //Kodrad nedan har jag fått lite hjälp av chatgpt med framförallt "=>" .. Tidigare hade jag koden redan inne i CustomerService.cs vid ID.
    //Koden i CustomerService just nu som är: Id = GuidHelper.NewGuid(), HÄMTAR alltså det unika kund ID:t i GuidHelper.cs istället för CustomerService.cs.
    public static Guid NewGuid() => Guid.NewGuid(); 
}


