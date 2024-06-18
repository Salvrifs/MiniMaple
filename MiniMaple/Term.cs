using System.Text;

namespace MiniMaple;

/// <summary>
/// Выражение вида a*x^k (одночлен)
/// </summary>
public class Term : Atom
{
    private readonly string symbol; // Переменная/символ
    private int coefficient; // Коэффициент
    private int power; // Показатель степени

    public Term(string s, int c = 1, int p = 1)
    {
        // По умолчанию коэффициент и показатель степени равны единице
        symbol = s;
        coefficient = c;
        power = p;
    }

    /// <summary>
    /// Свойство для чтения значения symbol вне класса
    /// </summary>
    public string Symbol => symbol;
    /// <summary>
    /// Свойство для чтения значения coefficient вне класса
    /// </summary>
    public int Coefficient => coefficient;
    /// <summary>
    /// Свойство для чтения значения power вне класса
    /// </summary>
    public int Power => power;

    /// <summary>
    /// Вспомогательный метод для копирования объекта
    /// </summary>
    private Term Copy() => new(symbol, coefficient, power);

    // TODO: реализовать проверку отрицательности
    // Подсказка: сравните коэффициент с нулем
    public override bool Negative => coefficient < 0;

    public override bool Eq(Atom other)
    {
        if (other is Term term)
            return symbol == term.symbol &&
                coefficient == term.coefficient &&
                power == term.power;
        else
            return false;
    }

    public override Atom Neg()
    {
        // TODO: реализовать операцию отрицания
        // Указание: создайте новый объект класса Term
        // Подсказка: нужно сменить знак коэффициента
        throw new NotImplementedException();
    }

    public override Atom Add(Atom other)
    {
        // Так реализуется операция сложения.
        // Обратите внимание, что создается объект другого класса.
        // Это обертка над выражением вида lhs + rhs.
        return new Sum(Copy(), other);
    }

    public override Atom Sub(Atom other)
    {
        // TODO: реализуйте операцию вычитания.
        // Указание: аналогично сложению.
        // Подсказка: нужно вызвать метод Neg
        throw new NotImplementedException();
    }

    public Term Mul(int value)
    {
        // Так реализуется операция умножения на число
        Term copy = Copy(); // создаем копию объекта
        copy.coefficient *= value; // умножаем его коэффициент на значение
        return copy; // возвращаем измененную копию
        // Обратите внимание! Изменять поля объекта this нельзя!
        // Это сломает работу цепочек методов ୧༼ಠ益ಠ༽୨
    }

    public Term Pow(int value)
    {
        // TODO: реализуйте операцию возведения в степень
        // Указание: аналогично Mul
        // Подсказка: для пересчета коэффициента используйте методы Math и приведение типов.
        // Не забудьте обновить показатель степени!
        throw new NotImplementedException();
    }

    public override Atom Diff(string sym = "x")
    {
        // TODO: реализовать дифференцирование одночлена:
        // если переменная sym не равна (<>) переменной одночлена, то производная равна нулю (Number);
        // если показатель степени больше единицы, то производная равна a*k*x^(k-1) (Term);
        // если показатель степени равен единице, то производная равна a*k (Number).
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        if (coefficient != 1)
            sb.Append($"{coefficient}*");
        sb.Append(symbol);
        if (power != 1)
            sb.Append($"^{power}");
        return sb.ToString();
    }
}
