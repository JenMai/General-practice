/*
 * Bulls and Cows (or Mastermind) game:
 * Player 1 types in a 4-digits number;
 * Player 2 tries to guess it under 5 attempts;
 * Clues are given as such: "right place - wrong place".
 * Usage: ./cows nnnn (4 different digits)
 */

#include<stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>
#include <unistd.h>
#include <stdbool.h>

#define DIGITS 4
#define TRIES 5

void clear(void);
void greet(void);
bool check_input(const char* input);
int compare_p1p2(char* p1, char* p2);

int main(int argc, char *argv[])
{
	/*
	 * Ensures proper usage from Player 1
	 */
	if (argc != 2)
	{
		printf("Usage: ./cows nnnn\n");
		return 1;
	}
	
	char *p1Input = argv[1];
	char p2Input[4];
	int result;

	if (check_input(p1Input) == false)
	{
		return 1;
	}

	/*
	 * Prepares terminal for Player 2
	 */
	clear();
	greet();                                           

	/*
	 * Attempts to guess p1's input
	 */
	for (int i = 0; i < TRIES; i++)
	{
		printf("Guess: ");
		scanf("%s", p2Input);

		while (check_input(p2Input) == false)          // ensures proper usage for p2
		{
			printf("Guess: ");
			scanf("%s", p2Input);
		}
		result = compare_p1p2(p1Input, p2Input);

		if (result == 4)                               // if p2 guessed the number right
		{
			printf("YOU GUESSED IT, CONGRATS!\n");
				return 0;
		}
	}

	printf("GAME OVER!\n You couldn't guess %s\n", p1Input);
}

/*
 * Clears screen using ANSI escape sequences.
 * (From CS50's Game of Fifteen exercise)
 */
void clear(void)
{
	printf("\033[2J");
	printf("\033[%d;%dH", 0, 0);
}


/**
* Greets player.
* (From CS50's Game of Fifteen exercise)
*/
void greet(void)
{
	clear();
	printf("~~* WELCOME TO BULLS AND COWS *~~\n\n");
	usleep(2000000);
	printf("GUESS PLAYER 1'S NUMBER UNDER 5 TRIES.\n");
	printf("2 CLUES WILL BE GIVEN AFTER EACH TRY:\n");
	printf("RIGHT POSITION - WRONG POSITION\n\n");
	usleep(3000000);
}

/*
 * Checks if input from P1 or P2 meets the following conditions:
 * - 4 digits
 * - final input is an integer
 */
bool check_input(const char* input)
{
	if (strlen(input) != 4)
	{
		printf("Usage: type in a 4 digits integer.\n");
		return false;
	}

	for (int i = 0; i < DIGITS; i++)
	{
		if (input[i] <= '0' || input[i] >= '9')
		{
			printf("Usage: type in a 4 digits integer.\n");
			return false;
		}
	}

	for (int i = 0; i < DIGITS - 1; i++)              // DIGITS-1, no need to check last digit
	{
		for (int j = i + 1; j < DIGITS; j++)          // compare two digits
		{
			if (input[i] == input[j])
			{
				printf("Usage: digits must all be different.\n");
				return false;
			}
		}
	}
	return true;
}

/*
 * Compares players' inputs
 */
int compare_p1p2(char* p1, char* p2)
{
	int rp = 0, wp = 0;
	
	for (int i = 0; i < DIGITS; i++)
	{
		for (int j = 0; j < DIGITS; j++)
		{
			if ((p1[i] == p2[j]) && (i != j))        // digit guessed but different position
			{
				wp += 1;
			}
			else if ((p1[i] == p2[j]) && (i == j))   // digit guessed with right position
			{
				rp += 1;
			}
		}
	}
	printf("%s \t %d - %d \n", p2, rp, wp);

	return rp;                                       // return rp for if statement
}