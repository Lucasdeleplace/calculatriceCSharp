Console.WriteLine("Calculatrice");
Console.WriteLine("Entrez un premier nombre :");
double.TryParse(Console.ReadLine(), out double a);
Console.WriteLine("Entrez un deuxième nombre :");
double.TryParse(Console.ReadLine(), out double b);
Console.WriteLine("Entrez un opérateur (+, -, *, /) :");
string op = Console.ReadLine();
double result = 0;
switch (op)
{
    case "+":
        result = a + b;
        break;
    case "-":
        result = a - b;
        break;
    case "*":
        result = a * b;
        break;
    case "/":
        if (b == 0)
        {
            Console.WriteLine("Division par zéro impossible");
            return;
        }
        result = a / b;
        break;
    default:
        Console.WriteLine("Opérateur invalide");
        return;
}
Console.WriteLine("Résultat : " + result);