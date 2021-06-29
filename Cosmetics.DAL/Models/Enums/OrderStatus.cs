using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models.Enums
{
    public enum OrderStatus
    {
        Pending, //В ожидании
        Processing, //В обработке
        Complete, //Завершен
        Cancelled //Отменен
    }
}
