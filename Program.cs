using Spectre.Console;

List<double> nombres = new List<double>();

void entrerNombres()
{
    nombres.Clear(); // Vider la liste avant d'ajouter de nouveaux nombres
    while (true)
    {
        string input = AnsiConsole.Ask<string>("Entrez un nombre (ou appuyez sur [green]Entrée[/] pour terminer, [red]d[/] pour supprimer le dernier) : ");
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
            .Title("Choisissez une option :")
            .AddChoices(1, 2, 3, 4, 5, 6, 7, 8)
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
                    7 => "7. Quitter",
                    8 => "8. Commandes",
                    _ => "Choix invalide"
                };
            }));

    return choix;
}

while (true) // Tant que l'utilisateur ne quitte pas, on continue
{
    int choix = Menu();
    double result;

    switch (choix)
    {
        case 1:
            entrerNombres();
            result = nombres.Sum();
            AnsiConsole.MarkupLine("Le résultat de l'addition est : [green]{0}[/]", result);
            break;
        case 2:
            entrerNombres();
            result = nombres.Aggregate((a, b) => a - b);
            AnsiConsole.MarkupLine("Le résultat de la soustraction est : [green]{0}[/]", result);
            break;
        case 3:
            entrerNombres();
            result = nombres.Aggregate((a, b) => a * b);
            AnsiConsole.MarkupLine("Le résultat de la multiplication est : [green]{0}[/]", result);
            break;
        case 4:
            entrerNombres();
            result = nombres.Aggregate((a, b) => a / b);
            AnsiConsole.MarkupLine("Le résultat de la division est : [green]{0}[/]", result);
            break;
        case 5:
            entrerNombres();
            if (nombres.Count >= 2)
            {
                result = Math.Pow(nombres[0], nombres[1]);
                AnsiConsole.MarkupLine("Le résultat de la puissance est : [green]{0}[/]", result);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Veuillez entrer au moins deux nombres pour la puissance.[/]");
            }
            break;
        case 6:
            double num = AnsiConsole.Ask<double>("Entrez un nombre : ");
            result = Math.Sqrt(num);
            AnsiConsole.MarkupLine("Le résultat de la racine est : [green]{0}[/]", result);
            break;
        case 7:
            AnsiConsole.Clear();
            Environment.Exit(0);
            break;
        case 8:
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Commandes :[/]");
            AnsiConsole.MarkupLine("-----------------------");
            AnsiConsole.MarkupLine("Lors d'une opération, en cas d'erreur, utilisez la commande 'd' pour supprimer le dernier nombre entré");
            AnsiConsole.MarkupLine("Entrée : valider un nombre entré");
            break;
        default:
            AnsiConsole.MarkupLine("[red]Choix invalide[/]");
            break;
    }

    var retourMenu = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Voulez-vous retourner au menu ?")
            .AddChoices("Oui", "Non"));

    AnsiConsole.Clear();
    if (retourMenu != "Oui")
    {
        Environment.Exit(0);
    }
}