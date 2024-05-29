using System;
using System.Collections.Generic;

namespace prac20.models;

public partial class СправочникВидовРабот
{
    public int КодРаботы { get; set; }

    public string? МаркаАвтомобиля { get; set; }

    public string? НаименованиеРаботы { get; set; }

    public decimal? СтоимостьРаботы { get; set; }

    public virtual ICollection<Заказы> Заказыs { get; set; } = new List<Заказы>();
}
