using System;
using System.Collections.Generic;

namespace prac20.models;

public partial class СправочникИсполнителейРабот
{
    public int КодИсполнителя { get; set; }

    public string? НаименованиеОрганизации { get; set; }

    public string? Адрес { get; set; }

    public string? Телефон { get; set; }

    public virtual ICollection<Заказы> Заказыs { get; set; } = new List<Заказы>();
}
