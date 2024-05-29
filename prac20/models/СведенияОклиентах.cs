using System;
using System.Collections.Generic;

namespace prac20.models;

public partial class СведенияОклиентах
{
    public string Клиент { get; set; } = null!;

    public string? НаименованиеОбъекта { get; set; }

    public string? АдресОбъекта { get; set; }

    public virtual ICollection<Заказы> Заказыs { get; set; } = new List<Заказы>();
}
