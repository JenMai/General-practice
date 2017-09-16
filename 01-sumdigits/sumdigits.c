/*
* Gets 3 integers as arguments, multiply and add them,
* then sums the result's digits by separating them
* using a base 10 division instead of
* the "integer to string" trick.
*
* Usage: ./sumdigits x y z
*/

#include<stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>

int mul_add(int x, int y, int z);
int base10_sumdigits(int a);

int totalSum;

int main(int argc, char *argv[])
{
	int values[3];                                  // integers stored after checking
	int result;

	/*
	 * Ensure proper usage
	 */
	if (argc != 4)
	{
		printf("Usage: ./sumdigits x y z (integers superior or equal to 0)\n");
		return 1;
	}

	for (int i = 1; i < argc; i++)                 // for each argument
	{
		char *test = argv[i];                  // get  current argument 
						       // (argv == pointer of pointer)
		for (int j = 0; j < strlen(test); j++) // check for alpha chars
		{
			if (isalpha(test[j])) 
			{
				printf("Usage: ./sumdigits x y z (integers superior or equal to 0)\n");
				return 1;
			}
		}

		values[i-1] = atoi(test);              // if no alpha found, store integer

		if (values[i - 1] < 0)		       // no negative integers
		{
			printf("Usage: ./sumdigits x y z (integers superior or equal to 0)\n");
			return 1;
		}
	}

	/*
	 * Sums digits 
	 */

	if ((values[0] == 0 || values[1] == 0)
	     && values[2] <= 9)                        // multiplication equals 0, no need to add digits
	{
		result = values[2];

		printf("Digits to add: %d\n", result);
		printf("Result: %d\n", result);

		return 0;
	}

	int digits = mul_add(values[0], values[1], 
			     values[2]);              // add and multiply values

	printf("Digits to add: %d\n", digits);

	result = base10_sumdigits(digits);            // get the final result

	printf("Result: %d\n", result);

}

/*
 * Multiplies then adds
 */
int mul_add(int x, int y, int z)
{
	int total;
	total = x * y + z;

	return total;
}


/*
 * Separate then add digits with base 10 division
 * (recursively, because why not?).
 */
int base10_sumdigits(int a)
{
	if (a > 10)
	{
		totalSum += (a % 10);		       // modulo to get the digits
		a /= 10;			       // divide to get the digits to the left next

		return base10_sumdigits(a);
	}
	else if (a < 10)                               // base case 1: a < 10, no need to modulo
	{
		totalSum += a;                         // add 1 from the tens, we don't want 10%10=0
		return totalSum;
	} 
	else                                           // base case 2: 1 and 0 are the last digits
	{
		totalSum += 1;
		return totalSum;
	}
}
