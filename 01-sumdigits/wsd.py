#-----------------------------
# Gets an integer as argument and multiply
# the digits by their position, using a base 10
# division to separate them.
#
# Usage: [python3] ./wsd.py x
#-----------------------------
import sys
import string

def main():
	totalSum = 0
	#-----------------------------
	# Ensure proper usage
	#-----------------------------

	if len(sys.argv) != 2:
		print("Usage: ./wsd x (integer superior or equal to 0)")
		return 1

	test = sys.argv[1]

	for char in test:                                # check for alpha or punctuation
		if (char.isalpha() == True) or (char == "-") \
		or (char in string.punctuation):
			print("Usage: ./wsd x (integer superior or equal to 0)")
			return 1

	counter = len(test)                              # check length of input to multiply by
	value = int(test)

	if value <= 9:                                   # whole process is then useless
		print("""Result: {}""".format(value))
		return 0

	result = base10_wsd(value, counter, totalSum)    # actual WSD

	print("Result: {}".format(result))


#-----------------------------
# Separates, multiplies and adds digits together 
# (from right to left)
# (...recursion strikes back)
#-----------------------------
def base10_wsd(a, counter, totalSum):
	if a > 10:
		totalSum += ((a % 10) * counter)             # multiply digit by position
		a = int(a / 10)                              # get next digits (and convert back to int)
		counter -= 1                                 # get next position
		return base10_wsd(a, counter, totalSum)

	elif a < 10:                                     # base case 1: a < 10, no need to further
		totalSum += a
		return totalSum

	else:                                            # base case 2: a = 10, we don't want to modulo it to 0
		totalSum += 1
		return totalSum


if __name__ == "__main__" :
    main()
