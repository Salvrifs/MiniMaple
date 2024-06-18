namespace MiniMaple;

/// <summary>
/// Обертка над свободным членом выражения (константой)
/// </summary>
public class Number : Atom
{
    private readonly int value; // Оборачиваемая константа

    public Number(int v)
    {
        value = v;
    }
    
    /// <summary>
    /// Свойство для чтения значения value вне класса
    /// </summary>
    public int Value => value;

    // TODO: реализовать проверку отрицательности
    public override bool Negative => value < 0;

    public override bool Eq(Atom other)
    {
        // Операция is предназначена для проверки того, имеет ли классовая переменная указанный динамический тип.
        // Используем её для проверки типа выражения other:
        // если тип выражения other совпадает с типом текущего выражения (Number), то сравниваем значения полей.
        if (other is Number number)
            return value == number.value;
        // Иначе возвращаем false, так как сравнение выражений разных типов будет некорректным
        else
            return false;
    }

    public override Atom Neg()
    {
        // Так реализуется операция отрицания
        // Обязательно возвращайте копию объекта для обеспечения корректной работы цепочек методов
        // https://en.wikipedia.org/wiki/Method_chaining
        return new Number(-value);
    }

    public override Atom Add(Atom other)
    {
        // Сложение осуществляется аналогично предыдущему методу
        if (other is Number number)
            return new Number(value + number.value);
        else
            return null;
    }

    public override Atom Sub(Atom other)
    {
        // TODO: реализовать операцию вычитания аналогично оператору сложения
        throw new NotImplementedException();
    }

    public override Atom Diff(string sym = "x")
    {
        // TODO: реализовать дифференцирование константы
        // Указание: возвращать нужно новый объект класса Number
        // Подсказка: производная константы по любой переменной равна нулю
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
