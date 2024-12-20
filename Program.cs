using Spectre.Console;

List<double> nombres = new List<double>();
double result = 0;

void entrerNombres()
{
    AnsiConsole.MarkupLine($"[green]premiere valeur :{result}[/]");
    while (true)
    {
        string input = AnsiConsole.Ask<string>("Entrez un nombre (ou appuyez sur [green]e[/] pour terminer, [red]d[/] pour supprimer le dernier) : ");
        if (input == "e") break;
        if (input == "d")
        {
            if (nombres.Count > 0)
            {
                nombres.RemoveAt(nombres.Count - 1);
                AnsiConsole.MarkupLine("[yellow]Dernier nombre supprimé.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Aucun nombre à supprimer.[/]");
            }
            continue;
        }
        try
        {
            nombres.Add(double.Parse(input));
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]Entrez un nombre valide.[/]");
        }
    }
}

int Menu()
{
    int choix = AnsiConsole.Prompt(
        new SelectionPrompt<int>()
            .Title("[green]Resultat actuel : " +
            $"{result}[/]\n\n" +
            "Choisissez une option :")
            .AddChoices(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0)
            .UseConverter(choice =>
            {
                return choice switch
                {
                    1 => "1. Addition",
                    2 => "2. Soustraction",
                    3 => "3. Multiplication",
                    4 => "4. Division",
                    5 => "5. Puissances",
                    6 => "6. Racines",
                    7 => "7. Sinus",
                    8 => "8. Cosinus",
                    9 => "9. Tangent",
                    10 => "10. clear",
                    0 => "0. Quitter",
                    _ => "Choix invalide"
                };
            }));

    return choix;
}

void EffectuerOperation(Func<double, double, double> operation)
{
    if (nombres.Count < 2)
    {
        AnsiConsole.MarkupLine("[red]Veuillez entrer au moins deux nombres pour cette opération.[/]");
        return;
    }

    result = nombres.Aggregate(operation);
    nombres.Clear();
    nombres.Add(result);
    AnsiConsole.MarkupLine($"Le résultat est : [green]{result}[/]");
}


while (true) // Tant que l'utilisateur ne quitte pas, on continue
{
    int choix = Menu();


    switch (choix)
    {
        case 1: // Addition
            entrerNombres();
            EffectuerOperation((a, b) => a + b);
            break;

        case 2: // Soustraction
            entrerNombres();
            EffectuerOperation((a, b) => a - b);
            break;

        case 3: // Multiplication
            entrerNombres();
            EffectuerOperation((a, b) => a * b);
            break;

        case 4: // Division
            entrerNombres();
            if (nombres.Contains(0))
            {
                AnsiConsole.MarkupLine("[red]Division par zéro interdite.[/]");
                break;
            }
            EffectuerOperation((a, b) => a / b);
            break;

        case 5:
            entrerNombres();
            if (nombres.Count >= 2)
            {
                result = Math.Pow(nombres[0], nombres[1]);
                nombres.Clear();
                nombres.Add(result);
                AnsiConsole.MarkupLine("Le résultat de la puissance est : [green]{0}[/]", result);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Veuillez entrer au moins deux nombres pour la puissance.[/]");
            }
            break;
        case 6:
            double numRacine = AnsiConsole.Ask<double>("Entrez un nombre : ");
            result = Math.Sqrt(numRacine);
            nombres.Clear();
            nombres.Add(result);
            AnsiConsole.MarkupLine("Le résultat de la racine est : [green]{0}[/]", result);
            break;
        case 7:
            double numSinus = AnsiConsole.Ask<double>("Entrez un nombre : ");
            result = Math.Sin(numSinus);
            nombres.Clear();
            nombres.Add(result);
            AnsiConsole.MarkupLine("Le résultat du sinus est : [green]{0}[/]", result);
            break;
        case 8:
            double numConsinus = AnsiConsole.Ask<double>("Entrez un nombre : ");
            result = Math.Cos(numConsinus);
            nombres.Clear();
            nombres.Add(result);
            AnsiConsole.MarkupLine("Le résultat du cosinus est : [green]{0}[/]", result);
            break;
        case 9:
            double numTangente = AnsiConsole.Ask<double>("Entrez un nombre : ");
            result = Math.Tan(numTangente);
            nombres.Clear();
            nombres.Add(result);
            AnsiConsole.MarkupLine("Le résultat de la tagente est : [green]{0}[/]", result);
            break;
        case 10:
            AnsiConsole.MarkupLine("[green]Le résultat à etais supprimer avec success [/]\n");
            nombres.Clear();
            result = 0;
            break;
        case 0:
            AnsiConsole.Clear();
            Environment.Exit(0);
            break;
        default:
            AnsiConsole.MarkupLine("[red]Choix invalide[/]");
            break;
    }

    string retourMenu = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Voulez-vous retourner au menu ?")
            .AddChoices("Oui", "Non"));

    AnsiConsole.Clear();
    if (retourMenu != "Oui")
    {
        Environment.Exit(0);
    }
}