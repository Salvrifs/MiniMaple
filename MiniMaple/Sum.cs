namespace MiniMaple;
/// <summary>
/// Выражение вида lhs + rhs
/// </summary>
public class Sum : Atom
{
    // Левая часть выражения (lhs, left hand side)
    private readonly Atom lhs;
    // Правая часть выражения (rhs, right hand side)
    private readonly Atom rhs;

    public Sum(Atom lhs, Atom rhs = null)
    {
        // Обратите внимание: правая часть выражения по умолчанию задана как null.
        // Это позволяет нам удобно создавать объекты класса Sum имея только одну часть выражения.
        this.lhs = lhs;
        this.rhs = rhs;
    }

    /// <summary>
    /// Свойство для чтения значения lhs вне класса
    /// </summary>
    public Atom Lhs => lhs;
    /// <summary>
    /// Свойство для чтения значения rhs вне класса
    /// </summary>
    public Atom Rhs => rhs;

    // Проверку на отрицательность реализовывать не будем
    public override bool Negative => false;

    public override bool Eq(Atom other)
    {
        //  реализуйте операцию сравнения
        // Указание: сравните значения полей
        if (other is Sum sum)
            return lhs.Eq(sum.lhs) && rhs.Eq(sum.rhs);
        else 
            return false;
    }

    public override Atom Neg()
    {
        // Смена знака выражения. В данном случае - оператора.
        return new Sum(lhs, rhs.Neg());
    }

    public override Atom Add(Atom other)
    {
        // Если выражение инициализировано только одной частью,  Sum           Sum
        // то, при добавлении нового слагаемого, его значение   /   \    =>   /   \
        // нужно присвоить недостающей части выражения:        a    nil      a     b
        // Sum(a, nil) + b => Sum(a, b)
        if (rhs == null)
            return new Sum(lhs, other);
        else
            // Если у выражения имеются обе части, то обернем его               Sum          Sum
            // в еще в одну сумму (поставим скобки): a + b + c = (a + b) + c   /   \   =>   /   \
            // или Sum(a, b) + c => Sum(Sum(a, b), c)                         a     b     Sum    c
            return new Sum(this, other); //                                              /   \
    } //                                                                                a     b

    public override Atom Sub(Atom other)
    {
        // TODO: реализуйте операцию вычитания
        // Указание: аналогично Add
        // Подсказка: используйте метод Neg

        if (rhs == null)
            return new Sum(lhs, other.Neg());
        else
            return new Sum(this, other.Neg());
    }

    public override Atom Diff(string sym = "x")
    {
        // Magnum opus: дифференцирование суммы

        // Производная левой части выражения
        Atom lhs = this.lhs.Diff(sym);
        // TODO: вычислите производную правой части выражения
        //Atom rhs = null;
        Atom rhs = this.rhs.Diff(sym);
        var zero = new Number(0);

        // Если производные левой и правой части выражения - константы
        if (lhs is Number && rhs is Number)
            // то результат - их сумма
            return lhs.Add(rhs);
        // Если в результате дифференцирования левая часть равна нулю
        else if (lhs.Eq(zero))
            // то результат - правая часть выражения
            return rhs;
        // TODO: проверить это условия для правой части
        //  ...
        else if (rhs.Eq(zero))
            return lhs;
        else
            // Если обе части не равны нулю, то результат - их сумма
            return new Sum(lhs, rhs);
    }

    public override string ToString()
    {
        if (rhs != null)
            return rhs.Negative switch
            {
                true => $"{lhs} - {rhs.Neg()}",
                false => $"{lhs} + {rhs}"
            };
        else
            return $"{lhs}";
    }
}
