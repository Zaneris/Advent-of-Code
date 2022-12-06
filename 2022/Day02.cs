using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day02 : AdventBase
{
    protected override void InternalPart1()
    {
        var rock = "A";
        var paper = "B";
        var scissors = "C";
        var p2Rock = "X";
        var p2Paper = "Y";
        var p2Scissors = "Z";

        var choiceRock = 1;
        var choicePaper = 2;
        var choiceScissors = 3;

        var loss = 0;
        var draw = 3;
        var win = 6;

        var total = 0;
        foreach (var line in InputLines)
        {
            var choices = line.Split(' ');
            var points = loss;
            if ((choices[0] == rock && choices[1] == p2Paper)
                || (choices[0] == paper && choices[1] == p2Scissors)
                || (choices[0] == scissors && choices[1] == p2Rock))
            {
                points = win;
            }
            
            if ((choices[0] == rock && choices[1] == p2Rock)
                || (choices[0] == paper && choices[1] == p2Paper)
                || (choices[0] == scissors && choices[1] == p2Scissors))
            {
                points = draw;
            }

            if (choices[1] == p2Rock)
                points += choiceRock;
            if (choices[1] == p2Paper)
                points += choicePaper;
            if (choices[1] == p2Scissors)
                points += choiceScissors;
            total += points;
        }
        Console.WriteLine(total);
    }

    protected override void InternalPart2()
    {
        var rock = "A";
        var paper = "B";
        var scissors = "C";
        var needLoss = "X";
        var needDraw = "Y";
        var needWin = "Z";

        var choiceRock = 1;
        var choicePaper = 2;
        var choiceScissors = 3;

        var loss = 0;
        var draw = 3;
        var win = 6;

        var total = 0;
        foreach (var line in InputLines)
        {
            var choices = line.Split(' ');
            var points = loss;
            if (choices[1] == needLoss)
            {
                if (choices[0] == rock) points += choiceScissors;
                if (choices[0] == paper) points += choiceRock;
                if (choices[0] == scissors) points += choicePaper;
            }
            else if (choices[1] == needDraw)
            {
                points += draw;
                if (choices[0] == rock) points += choiceRock;
                if (choices[0] == paper) points += choicePaper;
                if (choices[0] == scissors) points += choiceScissors;
            }
            else if (choices[1] == needWin)
            {
                points += win;
                if (choices[0] == rock) points += choicePaper;
                if (choices[0] == paper) points += choiceScissors;
                if (choices[0] == scissors) points += choiceRock;
            }
            total += points;
        }
        Console.WriteLine(total);
    }
}
