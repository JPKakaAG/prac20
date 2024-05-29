using System;
using System.Collections.Generic;

namespace prac20.models;

public partial class Заказы
{
    public DateOnly? Дата { get; set; }

    public int НомерЗаказа { get; set; }

    public string? Клиент { get; set; }

    public string? МаркаАвтомобиля { get; set; }

    public int? КодРаботы { get; set; }

    public int? КодИсполнителя { get; set; }

    public virtual СведенияОклиентах? КлиентNavigation { get; set; }

    public virtual СправочникИсполнителейРабот? КодИсполнителяNavigation { get; set; }

    public virtual СправочникВидовРабот? КодРаботыNavigation { get; set; }
}
